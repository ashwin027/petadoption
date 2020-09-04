using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetAdoption.Common;
using UserPetInfo.Models;
using UserPetInfo.Models.Entities;
using UserPetInfo.Repository;

namespace UserPetInfo.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserPetController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserPetController> _logger;
        private readonly IUserPetRepository _userPetRepository;
        public UserPetController(ILogger<UserPetController> logger, IUserPetRepository userPetRepository, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _userPetRepository = userPetRepository;
        }

        [HttpGet("/api/user/{userId}/userPet/{id}")]
        public async Task<ActionResult<UserPet>> GetUserPet(string userId, int? id)
        {
            try
            {
                if (id == null)
                {
                    _logger.LogError("Bad request in UserPetController, method: GetUserPet(). Request is null.");
                    return BadRequest();
                }

                var currentLoggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!currentLoggedInUser.Equals(userId, StringComparison.InvariantCultureIgnoreCase))
                {
                    _logger.LogError($"UserPetController, method: GetUserPet(). User with id: {currentLoggedInUser} attempting to get pet with id: {id} of a different user.");
                    return Unauthorized();
                }

                var userPetEntity = await _userPetRepository.GetUserPet(id);

                if (userPetEntity == null)
                {
                    _logger.LogError($"UserPetController, method: GetUserPet(). User Pet with id {id} not found.");
                    return NotFound();
                }

                var userPet = _mapper.Map<UserPet>(userPetEntity);

                return Ok(userPet);

            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in UserPetController, method: GetUserPet().", ex);
                return StatusCode(500);
            }
        }

        [HttpGet("/api/user/{userId}/userPets")]
        public async Task<ActionResult<PaginatedList<UserPet>>> GetuserPets(string userId, [FromQuery] PaginationParams paginationParams)
        {
            try
            {
                var currentLoggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!currentLoggedInUser.Equals(userId, StringComparison.InvariantCultureIgnoreCase))
                {
                    _logger.LogError($"UserPetController, method: GetuserPets(). User with id: {currentLoggedInUser} attempting to get pets of a different user.");
                    return Unauthorized();
                }

                var userPetEntities = await _userPetRepository.GetUserPets(userId, paginationParams?.PageIndex, paginationParams?.PageSize);

                if (userPetEntities == null)
                {
                    _logger.LogError($"UserPetController, method: GetuserPets(). No user pets found.");
                    return NotFound();
                }

                var userPets = _mapper.Map<PaginatedList<UserPet>>(userPetEntities);
                return Ok(userPets);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in UserPetController, method: GetuserPets().", ex);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserPet>> CreateUserPet(UserPet userPet)
        {
            try
            {
                if (userPet == null)
                {
                    _logger.LogError("Bad request in UserPetController, method: CreateUserPet(). Request is null.");
                    return BadRequest();
                }

                if (string.IsNullOrWhiteSpace(userPet.UserId))
                {
                    _logger.LogError("Bad request in UserPetController, method: CreateUserPet(). User ID is null.");
                    return BadRequest();
                }

                var currentLoggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!currentLoggedInUser.Equals(userPet.UserId, StringComparison.InvariantCultureIgnoreCase))
                {
                    _logger.LogError($"UserPetController, method: CreateUserPet(). User with id: {currentLoggedInUser} attempting to create a pet with a different user ID.");
                    return Unauthorized();
                }

                var userPetEntity = _mapper.Map<UserPetEntity>(userPet);
                var result = await _userPetRepository.CreateUserPet(userPetEntity);

                if (result == null)
                {
                    _logger.LogError("Server Error in UserPetController, method: CreateUserPet(). User pet creation failed.");
                    return StatusCode(500);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in UserPetController, method: CreateUserPet().", ex);
                return StatusCode(500);
            }

        }

        [HttpPut]
        public async Task<ActionResult<UserPet>> UpdateUserPet(UserPet userPet)
        {
            try
            {
                if (userPet == null)
                {
                    _logger.LogError("Bad request in UserPetController, method: UpdateUserPet(). Request is null.");
                    return BadRequest();
                }

                if (string.IsNullOrWhiteSpace(userPet.UserId))
                {
                    _logger.LogError("Bad request in UserPetController, method: UpdateUserPet(). User ID is null.");
                    return BadRequest();
                }

                var currentLoggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!currentLoggedInUser.Equals(userPet.UserId, StringComparison.InvariantCultureIgnoreCase))
                {
                    _logger.LogError($"UserPetController, method: UpdateUserPet(). User with id: {currentLoggedInUser} attempting to update a pet with a different user ID.");
                    return Unauthorized();
                }

                var userPetEntity = _mapper.Map<UserPetEntity>(userPet);
                var pet = await _userPetRepository.GetUserPet(userPetEntity.Id);
                if (pet == null)
                {
                    _logger.LogError("Server Error in UserPetController, method: UpdateUserPet(). User pet not found.");
                    return NotFound();
                }

                var result = await _userPetRepository.UpdateUserPet(userPetEntity);

                if (result == null)
                {
                    _logger.LogError("Server Error in UserPetController, method: UpdateUserPet(). User pet update failed.");
                    return StatusCode(500);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in UserPetController, method: UpdateUserPet().", ex);
                return StatusCode(500);
            }
        }

        [HttpDelete("/api/user/{userId}/userPet/{id}")]
        public async Task<ActionResult> DeleteUserPet(string userId, int? id)
        {
            try
            {
                if (id == null)
                {
                    _logger.LogError("Bad request in UserPetController, method: DeleteUserPet(). Id is null.");
                    return BadRequest();
                }

                if (string.IsNullOrWhiteSpace(userId))
                {
                    _logger.LogError("Bad request in UserPetController, method: DeleteUserPet(). User ID is null.");
                    return BadRequest();
                }

                var currentLoggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!currentLoggedInUser.Equals(userId, StringComparison.InvariantCultureIgnoreCase))
                {
                    _logger.LogError($"UserPetController, method: UpdateUserPet(). User with id: {currentLoggedInUser} attempting to delete a pet with a different user ID.");
                    return Unauthorized();
                }

                var result = await _userPetRepository.DeleteUserPet(id);

                if (!result)
                {
                    _logger.LogError("Server Error in UserPetController, method: DeleteUserPet(). Deletion failed.");
                    return StatusCode(500);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in UserPetController, method: DeleteUserPet().", ex);
                return StatusCode(500);
            }
        }
    }
}
