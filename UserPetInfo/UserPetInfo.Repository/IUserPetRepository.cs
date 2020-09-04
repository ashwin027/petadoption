using System.Threading.Tasks;
using PetAdoption.Common;
using UserPetInfo.Models.Entities;

namespace UserPetInfo.Repository
{
    public interface IUserPetRepository
    {
        Task<PaginatedList<UserPetEntity>> GetUserPets(string userId, int? pageNumber, int? pageSize);
        Task<PaginatedList<UserPetEntity>> SearchUserPets(string userId, string petName, int? pageNumber, int? pageSize);
        Task<UserPetEntity> GetUserPet(int? id);
        Task<UserPetEntity> CreateUserPet(UserPetEntity userPet);
        Task<UserPetEntity> UpdateUserPet(UserPetEntity userPet);
        Task<bool> DeleteUserPet(int? id);
    }
}
