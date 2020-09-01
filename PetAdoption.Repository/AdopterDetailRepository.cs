using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetAdoption.Models;
using PetAdoption.Models.Common;
using PetAdoption.Models.Entities;

namespace PetAdoption.Repository
{
    public class AdopterDetailRepository: IAdopterDetailRepository
    {
        private readonly ILogger<AdopterDetailRepository> _logger;
        private readonly AdoptionContext _adoptionContext;
        private const int DefaultPageIndex = 1;
        private const int DefaultPageSize = 5;
        public AdopterDetailRepository(AdoptionContext adoptionContext, ILogger<AdopterDetailRepository> logger)
        {
            _adoptionContext = adoptionContext;
            _logger = logger;
        }
        public async Task<AdopterDetailEntity> CreateAdopterDetail(AdopterDetailEntity adopterDetails)
        {
            try
            {
                await _adoptionContext.AdopterDetails.AddAsync(adopterDetails);
                await _adoptionContext.SaveChangesAsync();

                return adopterDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in CreateAdopterDetail().", ex);
                throw ex;
            }
        }

        public async Task<AdopterDetailEntity> UpdateAdopterDetail(AdopterDetailEntity adopterDetails)
        {
            try
            {
                if (adopterDetails?.Id == null)
                {
                    return null;
                }

                var existingAdopterDetails = await _adoptionContext.AdopterDetails.FindAsync(adopterDetails.Id);
                if (existingAdopterDetails == null)
                {
                    return null;
                }

                existingAdopterDetails.Id = adopterDetails.Id;
                existingAdopterDetails.Address = adopterDetails.Address;
                existingAdopterDetails.GivenName = adopterDetails.GivenName;
                existingAdopterDetails.LastName = adopterDetails.LastName;
                existingAdopterDetails.Telephone = adopterDetails.Telephone;
                existingAdopterDetails.UserId = adopterDetails.UserId;

                await _adoptionContext.SaveChangesAsync();

                return existingAdopterDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in UpdateAdopterDetail().", ex);
                throw ex;
            }
        }

        public async Task<AdopterDetailEntity> GetAdopterDetails(int? id)
        {
            try
            {
                if (id == null)
                {
                    return null;
                }

                return await _adoptionContext.AdopterDetails.FirstOrDefaultAsync(query => query.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in GetAdopterDetails().", ex);
                throw ex;
            }
        }

        public async Task<List<AdopterDetailEntity>> GetAllAdopterDetailsForUser(string userId)
        {
            try
            {
                return await _adoptionContext.AdopterDetails.Where(ad => ad.Adoption.AdopteeId==userId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in GetAllAdopterDetailsForUser().", ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteAdopterDetails(int? id)
        {
            try
            {
                if (id == null)
                {
                    return false;
                }
                var adopterDetailEntity = await _adoptionContext.AdopterDetails.FindAsync(id);
                _adoptionContext.AdopterDetails.Remove(adopterDetailEntity);
                await _adoptionContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in DeleteAdopterDetails().", ex);
                throw ex;
            }
        }
    }
}
