using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetAdoption.Models;
using PetAdoption.Models.Entities;
using PetAdoption.Repository;

namespace PetAdoption.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AdopterDetailController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AdopterDetailController> _logger;
        private readonly IAdopterDetailRepository _adopterDetailRepository;
        private readonly IAdoptionRepository _adoptionRepository;

        public AdopterDetailController(ILogger<AdopterDetailController> logger, IAdopterDetailRepository adopterDetailRepository, IMapper mapper, IAdoptionRepository adoptionRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _adopterDetailRepository = adopterDetailRepository;
            _adoptionRepository = adoptionRepository;
        }

        [HttpPost]
        public async Task<ActionResult<AdopterDetail>> CreateAdopterDetails(AdopterDetail adopterDetail)
        {
            try
            {
                if (adopterDetail == null)
                {
                    _logger.LogError("Bad request in AdopterDetailController, method: CreateAdopterDetail(). Request is null.");
                    return BadRequest();
                }

                if (string.IsNullOrWhiteSpace(adopterDetail.UserId))
                {
                    _logger.LogError("Bad request in AdopterDetailController, method: CreateAdopterDetail(). User ID is null.");
                    return BadRequest();
                }

                var currentLoggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!currentLoggedInUser.Equals(adopterDetail.UserId, StringComparison.InvariantCultureIgnoreCase))
                {
                    _logger.LogError($"AdoptionController, method: CreateAdopterDetail(). User with id: {currentLoggedInUser} attempting to create an adopter detail record with a different user ID.");
                    return Unauthorized();
                }

                if (string.IsNullOrWhiteSpace(adopterDetail.AdoptionId.ToString()))
                {
                    _logger.LogError("Bad request in AdopterDetailController, method: CreateAdopterDetail(). Adoption ID is null.");
                    return BadRequest();
                }

                var adopterDetailEntity = _mapper.Map<AdopterDetailEntity>(adopterDetail);
                var result = await _adopterDetailRepository.CreateAdopterDetail(adopterDetailEntity);

                if (result == null)
                {
                    _logger.LogError("Server Error in AdopterDetailController, method: CreateAdopterDetail(). Adopter detail creation failed.");
                    return StatusCode(500);
                }

                // Update the adopter id in the adoption record
                var adoptionEntity = await _adoptionRepository.GetAdoption(adopterDetail.AdoptionId);
                var adoption = _mapper.Map<Adoption>(adoptionEntity);
                adoption.AdopterId = adopterDetail.UserId;
                adoption.Status = AdoptionStatus.Pending;
                adoption.AdopterDetailId = adopterDetail.Id;
                await _adoptionRepository.UpdateAdoption(_mapper.Map<AdoptionEntity>(adoptionEntity));

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in AdopterDetailController, method: CreateAdopterDetail().", ex);
                return StatusCode(500);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdopterDetail>> GetAdopterDetails(int? id)
        {
            try
            {
                if (id == null)
                {
                    _logger.LogError("Bad request in AdopterDetailController, method: GetAdopterDetails(). Request is null.");
                    return BadRequest();
                }

                var adopterDetailEntity = await _adopterDetailRepository.GetAdopterDetails(id);

                if (adopterDetailEntity == null)
                {
                    _logger.LogError($"AdopterDetailController, method: GetAdopterDetails(). Adopter details with id {id} not found.");
                    return NotFound();
                }

                var adopterDetails = _mapper.Map<Adoption>(adopterDetailEntity);

                return Ok(adopterDetails);

            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in AdopterDetailController, method: GetAdopterDetails().", ex);
                return StatusCode(500);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<AdopterDetail>>> GetAllAdopterDetailsForUser(string userId)
        {
            try
            {
                var adopterDetailEntities = await _adopterDetailRepository.GetAllAdopterDetailsForUser(userId);

                if (adopterDetailEntities == null)
                {
                    _logger.LogError($"AdopterDetailController, method: GetAllAdopterDetailsForUser(). No adoptions found.");
                    return NotFound();
                }

                var adopterDetails = _mapper.Map<List<AdopterDetail>>(adopterDetailEntities);
                return Ok(adopterDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in AdopterDetailController, method: GetAllAdopterDetailsForUser().", ex);
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<ActionResult<AdopterDetail>> UpdateAdopterDetails(AdopterDetail adopterDetail)
        {
            try
            {
                if (adopterDetail == null)
                {
                    _logger.LogError("Bad request in AdopterDetailController, method: UpdateAdopterDetails(). Request is null.");
                    return BadRequest();
                }

                if (string.IsNullOrWhiteSpace(adopterDetail.UserId))
                {
                    _logger.LogError("Bad request in AdopterDetailController, method: UpdateAdopterDetails(). Adoptee ID is null.");
                    return BadRequest();
                }

                var currentLoggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!currentLoggedInUser.Equals(adopterDetail.UserId, StringComparison.InvariantCultureIgnoreCase))
                {
                    _logger.LogError($"AdopterDetailController, method: UpdateAdopterDetails(). User with id: {currentLoggedInUser} attempting to update an adopter detail record with a different user ID.");
                    return Unauthorized();
                }

                var adopterDetailEntity = _mapper.Map<AdopterDetailEntity>(adopterDetail);
                var entity = await _adopterDetailRepository.GetAdopterDetails(adopterDetailEntity.Id);
                if (entity == null)
                {
                    _logger.LogError("Server Error in AdopterDetailController, method: UpdateAdopterDetails(). Adopter detail record not found.");
                    return NotFound();
                }

                var result = await _adopterDetailRepository.UpdateAdopterDetail(adopterDetailEntity);

                if (result == null)
                {
                    _logger.LogError("Server Error in AdopterDetailController, method: UpdateAdopterDetails(). Adopter detail record update failed.");
                    return StatusCode(500);
                }

                // Update the adopter id in the adoption record
                var adoptionEntity = await _adoptionRepository.GetAdoption(adopterDetail.AdoptionId);
                var adoption = _mapper.Map<Adoption>(adoptionEntity);
                adoption.AdopterId = adopterDetail.UserId;
                adoption.Status = AdoptionStatus.Pending;
                await _adoptionRepository.UpdateAdoption(_mapper.Map<AdoptionEntity>(adoptionEntity));

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in AdopterDetailController, method: UpdateAdopterDetails().", ex);
                return StatusCode(500);
            }
        }
    }
}
