using Microsoft.Extensions.Options;
using PetInfo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PetInfo.Models.Entities;

namespace PetInfo.Repository
{
    public class DogBreedInfoRepository : IDogBreedInfoRepository
    {
        private readonly HttpClient _client;
        private const string ApiVersion = "v1";
        private const string BreedEndpointSuffix = "breeds";
        private readonly ILogger<DogBreedInfoRepository> _logger;
        public DogBreedInfoRepository(ILogger<DogBreedInfoRepository> logger,  HttpClient client)
        {
            _client = client;
            _logger = logger;
        }
        public async Task<List<DogBreedInfoEntity>> GetAllBreeds(CancellationToken cancellationToken)
        {
            var lstDogBreedInfo = new List<DogBreedInfoEntity>();
            try
            {
                var response = await _client.GetAsync($"/{ApiVersion}/{BreedEndpointSuffix}", cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    lstDogBreedInfo = JsonConvert.DeserializeObject<List<DogBreedInfoEntity>>(responseContent);
                }

                return lstDogBreedInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        
        public async Task<List<DogBreedInfoEntity>> SearchBreeds(string searchString, CancellationToken cancellationToken)
        {
            var lstDogBreedInfo = new List<DogBreedInfoEntity>();
            try
            {
                var response = await _client.GetAsync($"/{ApiVersion}/{BreedEndpointSuffix}/search?q={searchString}", cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    lstDogBreedInfo = JsonConvert.DeserializeObject<List<DogBreedInfoEntity>>(responseContent);
                }

                return lstDogBreedInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
