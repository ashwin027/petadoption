using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PetAdoption.Models.Common;
using PetAdoption.Models.Entities;

namespace PetAdoption.Repository
{
    public interface IAdoptionRepository
    {
        Task<PaginatedList<AdoptionEntity>> GetAllOpenAdoptions(int? pageNumber, int? pageSize);
        Task<List<AdoptionEntity>> GetAllAdoptionsForUser(string userId);
        Task<PaginatedList<AdoptionEntity>> SearchAdoptions(string searchStr, int? pageNumber, int? pageSize);
        Task<AdoptionEntity> GetAdoption(int? id);
        Task<AdoptionEntity> CreateAdoption(AdoptionEntity adoption);
        Task<AdoptionEntity> UpdateAdoption(AdoptionEntity adoption);
        Task<bool> DeleteAdoption(int? id);
    }
}
