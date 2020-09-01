using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PetAdoption.Models.Entities;

namespace PetAdoption.Repository
{
    public interface IAdopterDetailRepository
    {
        Task<AdopterDetailEntity> CreateAdopterDetail(AdopterDetailEntity adopterDetails);
        Task<AdopterDetailEntity> UpdateAdopterDetail(AdopterDetailEntity adopterDetails);
        Task<AdopterDetailEntity> GetAdopterDetails(int? id);
        Task<List<AdopterDetailEntity>> GetAllAdopterDetailsForUser(string userId);
        Task<bool> DeleteAdopterDetails(int? id);
    }
}
