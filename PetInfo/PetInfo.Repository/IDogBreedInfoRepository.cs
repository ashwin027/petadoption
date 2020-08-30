using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PetInfo.Models;

namespace PetInfo.Repository
{
    public interface IDogBreedInfoRepository
    {
        Task<List<DogBreedInfo>> GetAllBreeds(CancellationToken cancellationToken);
        Task<List<DogBreedInfo>> SearchBreeds(string searchString, CancellationToken cancellationToken);
    }
}
