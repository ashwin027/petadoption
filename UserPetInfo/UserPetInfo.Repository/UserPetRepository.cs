using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserPetInfo.Models;
using UserPetInfo.Models.Common;
using UserPetInfo.Models.Entities;

namespace UserPetInfo.Repository
{
    public class UserPetRepository: IUserPetRepository
    {
        private readonly ILogger<UserPetRepository> _logger;
        private readonly UserPetContext _userPetContext;
        private const int DefaultPageIndex = 1;
        private const int DefaultPageSize = 5;
        public UserPetRepository(UserPetContext userPetContext, ILogger<UserPetRepository> logger)
        {
            _userPetContext = userPetContext;
            _logger = logger;
        }

        public async Task<PaginatedList<UserPetEntity>> GetUserPets(string userId, int? pageNumber, int? pageSize)
        {
            try
            {
                IQueryable<UserPetEntity> userPets = from u in _userPetContext.UserPets
                                                     where u.UserId==userId
                                                     select u;
                return await PaginatedList<UserPetEntity>.CreateAsync(userPets.AsNoTracking(), pageNumber ?? DefaultPageIndex, pageSize ?? DefaultPageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in GetUserPets().", ex);
                throw ex;
            }
        }

        public async Task<PaginatedList<UserPetEntity>> SearchUserPets(string userId, string petName, int? pageNumber, int? pageSize)
        {
            try
            {
                var userPets = _userPetContext.UserPets.Where(p => p.Name.Contains(petName) && p.UserId.Equals(userId, StringComparison.InvariantCultureIgnoreCase));
                return await PaginatedList<UserPetEntity>.CreateAsync(userPets.AsNoTracking(), pageNumber ?? DefaultPageIndex, pageSize ?? DefaultPageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in SearchUserPets().", ex);
                throw ex;
            }
        }

        public async Task<UserPetEntity> GetUserPet(int? id)
        {
            try
            {
                if (id == null)
                {
                    return null;
                }

                return await _userPetContext.UserPets.FirstOrDefaultAsync(query => query.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in GetUserPet().", ex);
                throw ex;
            }
        }

        public async Task<UserPetEntity> CreateUserPet(UserPetEntity userPet)
        {
            try
            {
                await _userPetContext.UserPets.AddAsync(userPet);
                await _userPetContext.SaveChangesAsync();

                return userPet;
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in CreateUserPet().", ex);
                throw ex;
            }
        }

        public async Task<UserPetEntity> UpdateUserPet(UserPetEntity userPet)
        {
            try
            {
                if (userPet?.Id == null)
                {
                    return null;
                }

                var existingUserPet = await _userPetContext.UserPets.FindAsync(userPet.Id);
                if (existingUserPet == null)
                {
                    return null;
                }

                existingUserPet.Id = userPet.Id;
                existingUserPet.Name = userPet.Name;
                existingUserPet.Description = userPet.Description;
                existingUserPet.BreedId = userPet.BreedId;
                existingUserPet.UserId = userPet.UserId;
                await _userPetContext.SaveChangesAsync();

                return existingUserPet;
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in UpdateUserPet().", ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteUserPet(int? id)
        {
            try
            {
                if (id == null)
                {
                    return false;
                }
                var userPet = await _userPetContext.UserPets.FindAsync(id);
                _userPetContext.UserPets.Remove(userPet);
                await _userPetContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in DeleteUserPet().", ex);
                throw ex;
            }
        }
    }
}
