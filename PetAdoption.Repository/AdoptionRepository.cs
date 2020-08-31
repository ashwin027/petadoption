using PetAdoption.Models.Common;
using PetAdoption.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PetAdoption.Repository
{
    public class AdoptionRepository : IAdoptionRepository
    {
        private readonly ILogger<AdoptionRepository> _logger;
        private readonly AdoptionContext _adoptionContext;
        private const int DefaultPageIndex = 1;
        private const int DefaultPageSize = 5;
        public AdoptionRepository(AdoptionContext adoptionContext, ILogger<AdoptionRepository> logger)
        {
            _adoptionContext = adoptionContext;
            _logger = logger;
        }
        public async Task<AdoptionEntity> CreateAdoption(AdoptionEntity adoption)
        {
            try
            {
                await _adoptionContext.Adoptions.AddAsync(adoption);
                await _adoptionContext.SaveChangesAsync();

                return adoption;
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in CreateAdoption().", ex);
                throw ex;
            }
        }

        public async Task<bool> DeleteAdoption(int? id)
        {
            try
            {
                if (id == null)
                {
                    return false;
                }
                var adoption = await _adoptionContext.Adoptions.FindAsync(id);
                _adoptionContext.Adoptions.Remove(adoption);
                await _adoptionContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in DeleteAdoption().", ex);
                throw ex;
            }
        }

        public async Task<AdoptionEntity> GetAdoption(int? id)
        {
            try
            {
                if (id == null)
                {
                    return null;
                }

                return await _adoptionContext.Adoptions.FirstOrDefaultAsync(query => query.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in GetAdoption().", ex);
                throw ex;
            }
        }

        public async Task<PaginatedList<AdoptionEntity>> GetAllAdoptions(int? pageNumber, int? pageSize)
        {
            try
            {
                IQueryable<AdoptionEntity> userPets = from u in _adoptionContext.Adoptions
                    select u;
                return await PaginatedList<AdoptionEntity>.CreateAsync(userPets.AsNoTracking(), pageNumber ?? DefaultPageIndex, pageSize ?? DefaultPageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in GetAllAdoptions().", ex);
                throw ex;
            }
        }

        public async Task<PaginatedList<AdoptionEntity>> SearchAdoptions(string searchStr, int? pageNumber, int? pageSize)
        {
            try
            {
                var userPets = _adoptionContext.Adoptions.Where(p => p.BreedName.Contains(searchStr) || p.PetName.Contains(searchStr));
                return await PaginatedList<AdoptionEntity>.CreateAsync(userPets.AsNoTracking(), pageNumber ?? DefaultPageIndex, pageSize ?? DefaultPageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in SearchAdoptions().", ex);
                throw ex;
            }
        }

        public async Task<AdoptionEntity> UpdateAdoption(AdoptionEntity adoption)
        {
            try
            {
                if (adoption?.Id == null)
                {
                    return null;
                }

                var existingAdoption = await _adoptionContext.Adoptions.FindAsync(adoption.Id);
                if (existingAdoption == null)
                {
                    return null;
                }

                existingAdoption.Id = adoption.Id;
                existingAdoption.BreedName = adoption.BreedName;
                existingAdoption.PetId = adoption.PetId;
                existingAdoption.PetName = adoption.PetName;
                existingAdoption.Fees = adoption.Fees;
                existingAdoption.AdopteeId = adoption.AdopteeId;
                existingAdoption.AdopterId = adoption.AdopterId;
                existingAdoption.Status = adoption.Status;
                
                await _adoptionContext.SaveChangesAsync();

                return existingAdoption;
            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in UpdateAdoption().", ex);
                throw ex;
            }
        }
    }
}
