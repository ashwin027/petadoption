using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PetAdoption.Hubs;
using PetAdoption.Models;
using PetAdoption.Models.Common;
using PetAdoption.Models.Entities;
using PetAdoption.Models.Messages;
using PetAdoption.Producers;
using PetAdoption.Repository;

namespace PetAdoption.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AdoptionController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AdoptionController> _logger;
        private readonly IAdoptionRepository _adoptionRepository;
        private readonly IHubContext<AdoptionHub> _hubContext;
        private readonly ProducerWrapper _producerWrapper;

        public AdoptionController(ILogger<AdoptionController> logger, IAdoptionRepository adoptionRepository, IMapper mapper, IHubContext<AdoptionHub> hubContext, ProducerWrapper producer)
        {
            _mapper = mapper;
            _logger = logger;
            _adoptionRepository = adoptionRepository;
            _hubContext = hubContext;
            _producerWrapper = producer;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Adoption>> GetAdoption(int? id)
        {
            try
            {
                if (id == null)
                {
                    _logger.LogError("Bad request in AdoptionController, method: GetAdoption(). Request is null.");
                    return BadRequest();
                }

                var adoptionEntity = await _adoptionRepository.GetAdoption(id);

                if (adoptionEntity == null)
                {
                    _logger.LogError($"AdoptionController, method: GetAdoption(). Adoption with id {id} not found.");
                    return NotFound();
                }

                var adoption = _mapper.Map<Adoption>(adoptionEntity);

                return Ok(adoption);

            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in AdoptionController, method: GetAdoption().", ex);
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<Adoption>>> GetAdoptions([FromQuery] PaginationParams paginationParams)
        {
            try
            {
                var adoptionEntities = await _adoptionRepository.GetAllOpenAdoptions(paginationParams?.PageIndex, paginationParams?.PageSize);

                if (adoptionEntities == null)
                {
                    _logger.LogError($"AdoptionController, method: GetAdoptions(). No adoptions found.");
                    return NotFound();
                }

                var adoptions = _mapper.Map<PaginatedList<Adoption>>(adoptionEntities);
                return Ok(adoptions);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in AdoptionController, method: GetAdoptions().", ex);
                return StatusCode(500);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<PaginatedList<Adoption>>> GetAdoptionsForUser(string userId)
        {
            try
            {
                var adoptionEntities = await _adoptionRepository.GetAllAdoptionsForUser(userId);

                if (adoptionEntities == null)
                {
                    _logger.LogError($"AdoptionController, method: GetAdoptionsForUser(). No adoptions found.");
                    return NotFound();
                }

                var adoptions = _mapper.Map<List<Adoption>>(adoptionEntities);
                return Ok(adoptions);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in AdoptionController, method: GetAdoptionsForUser().", ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Adoption>> CreateAdoption(Adoption adoption)
        {
            try
            {
                if (adoption == null)
                {
                    _logger.LogError("Bad request in AdoptionController, method: CreateAdoption(). Request is null.");
                    return BadRequest();
                }

                if (string.IsNullOrWhiteSpace(adoption.AdopteeId))
                {
                    _logger.LogError("Bad request in AdoptionController, method: CreateAdoption(). Adoptee ID is null.");
                    return BadRequest();
                }

                var currentLoggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!currentLoggedInUser.Equals(adoption.AdopteeId, StringComparison.InvariantCultureIgnoreCase))
                {
                    _logger.LogError($"AdoptionController, method: CreateAdoption(). User with id: {currentLoggedInUser} attempting to create an adoption with a different user ID.");
                    return Unauthorized();
                }

                var adoptionEntity = _mapper.Map<AdoptionEntity>(adoption);
                var result = await _adoptionRepository.CreateAdoption(adoptionEntity);

                if (result == null)
                {
                    _logger.LogError("Server Error in AdoptionController, method: CreateAdoption(). Adoption creation failed.");
                    return StatusCode(500);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in AdoptionController, method: CreateAdoption().", ex);
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Adoption>> UpdateAdoption(Adoption adoption)
        {
            try
            {
                if (adoption == null)
                {
                    _logger.LogError("Bad request in AdoptionController, method: UpdateAdoption(). Request is null.");
                    return BadRequest();
                }

                if (string.IsNullOrWhiteSpace(adoption.AdopteeId))
                {
                    _logger.LogError("Bad request in AdoptionController, method: UpdateAdoption(). Adoptee ID is null.");
                    return BadRequest();
                }

                var currentLoggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!currentLoggedInUser.Equals(adoption.AdopteeId, StringComparison.InvariantCultureIgnoreCase))
                {
                    _logger.LogError($"AdoptionController, method: UpdateAdoption(). User with id: {currentLoggedInUser} attempting to update an adoption with a different user ID.");
                    return Unauthorized();
                }

                var adoptionEntity = _mapper.Map<AdoptionEntity>(adoption);
                var entity = await _adoptionRepository.GetAdoption(adoptionEntity.Id);
                if (entity == null)
                {
                    _logger.LogError("Server Error in AdoptionController, method: UpdateAdoption(). Adoption record not found.");
                    return NotFound();
                }

                var currentStatus = entity.Status;
                var result = await _adoptionRepository.UpdateAdoption(adoptionEntity);

                if (result == null)
                {
                    _logger.LogError("Server Error in AdoptionController, method: UpdateAdoption(). Adoption record update failed.");
                    return StatusCode(500);
                }

                if (currentStatus != result.Status && result.Status == AdoptionStatus.Closed)
                {
                    _producerWrapper.Produce("UserPetCreation", new UserPetCreationMessage(){UserId = result.AdopterId, PreviousUserPetId = result.UserPetId});
                }

                return Ok(_mapper.Map<Adoption>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in AdoptionController, method: UpdateAdoption().", ex);
                return StatusCode(500);
            }
        }

        [HttpPut("details")]
        public async Task<ActionResult<Adoption>> UpdateAdoptionWithDetails(AdopterDetail adopterDetail)
        {
            try
            {
                if (adopterDetail == null)
                {
                    _logger.LogError("Bad request in AdoptionController, method: UpdateAdoptionWithDetails(). Request is null.");
                    return BadRequest();
                }

                if (string.IsNullOrWhiteSpace(adopterDetail.AdoptionId.ToString()))
                {
                    _logger.LogError("Bad request in AdoptionController, method: UpdateAdoptionWithDetails(). Adoption ID is null.");
                    return BadRequest();
                }

                var entity = await _adoptionRepository.GetAdoption(adopterDetail.AdoptionId);
                if (entity == null)
                {
                    _logger.LogError("Server Error in AdoptionController, method: UpdateAdoptionWithDetails(). Adoption record not found.");
                    return NotFound();
                }

                var adoption = _mapper.Map<Adoption>(entity);
                var originalStatus = adoption.Status;

                adoption.AdopterDetails = adopterDetail;
                adoption.Status = AdoptionStatus.Pending;
                adoption.AdopterId = adopterDetail.UserId;

                var adoptionEntityToSave = _mapper.Map<AdoptionEntity>(adoption);

                var adoptionEntity = await _adoptionRepository.UpdateAdoption(adoptionEntityToSave);

                if (adoptionEntity == null)
                {
                    _logger.LogError("Server Error in AdoptionController, method: UpdateAdoptionWithDetails(). Adoption record update failed.");
                    return StatusCode(500);
                }

                if (originalStatus != adoption.Status)
                {
                    var notification = new Notification()
                    {
                        NotificationId = Guid.NewGuid(),
                        Message = $"Adoption request received for {adoption.PetName}"
                    };
                    await _hubContext.Clients.User(adoption.AdopteeId).SendAsync("adoptionRequestReceived", notification);
                }
                return Ok(_mapper.Map<Adoption>(adoptionEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in AdoptionController, method: UpdateAdoptionWithDetails().", ex);
                return StatusCode(500);
            }
        }

        [HttpDelete("/api/user/{userId}/adoption/{id}")]
        public async Task<ActionResult> DeleteAdoption(string userId, int? id)
        {
            try
            {
                if (id == null)
                {
                    _logger.LogError("Bad request in AdoptionController, method: DeleteAdoption(). Id is null.");
                    return BadRequest();
                }

                if (string.IsNullOrWhiteSpace(userId))
                {
                    _logger.LogError("Bad request in AdoptionController, method: DeleteAdoption(). User ID is null.");
                    return BadRequest();
                }

                var currentLoggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!currentLoggedInUser.Equals(userId, StringComparison.InvariantCultureIgnoreCase))
                {
                    _logger.LogError($"AdoptionController, method: DeleteAdoption(). User with id: {currentLoggedInUser} attempting to delete an adoption with a different user ID.");
                    return Unauthorized();
                }

                var result = await _adoptionRepository.DeleteAdoption(id);

                if (!result)
                {
                    _logger.LogError("Server Error in AdoptionController, method: DeleteAdoption(). Deletion failed.");
                    return StatusCode(500);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in AdoptionController, method: DeleteAdoption().", ex);
                return StatusCode(500);
            }
        }
    }
}
