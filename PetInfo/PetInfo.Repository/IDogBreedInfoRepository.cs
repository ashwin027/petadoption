using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PetInfo.Models;
using PetInfo.Models.Entities;

namespace PetInfo.Repository
{
    public interface IDogBreedInfoRepository
    {
        Task<List<DogBreedInfoEntity>> GetAllBreeds(CancellationToken cancellationToken);
        Task<List<DogBreedInfoEntity>> SearchBreeds(string searchString, CancellationToken cancellationToken);
    }
}
