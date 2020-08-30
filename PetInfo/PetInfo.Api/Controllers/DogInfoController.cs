using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetInfo.Models;
using PetInfo.Repository;

namespace PetInfo.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DogInfoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DogInfoController> _logger;
        private readonly IDogBreedInfoRepository _dogBreedInfoRepository;
        public DogInfoController(ILogger<DogInfoController> logger, IDogBreedInfoRepository dogBreedInfoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _dogBreedInfoRepository = dogBreedInfoRepository;
        }

        [HttpGet("GetBreeds")]
        public async Task<IActionResult> GetBreeds(CancellationToken cancellationToken)
        {
            try
            {
                var breedEntities = await _dogBreedInfoRepository.GetAllBreeds(cancellationToken);
                var breeds = _mapper.Map<List<DogBreedInfo>>(breedEntities);
                return Ok(breeds);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Server Error in {nameof(DogInfoController)}, method: {MethodBase.GetCurrentMethod()?.Name}.", ex);
                return StatusCode(500);
            }
        }

        [HttpGet("SearchBreed/{searchString}")]
        public async Task<IActionResult> SearchBreed(string searchString, CancellationToken cancellationToken)
        {
            try
            {
                var breedEntities = await _dogBreedInfoRepository.SearchBreeds(searchString, cancellationToken);
                var breeds = _mapper.Map<List<DogBreedInfo>>(breedEntities);
                return Ok(breeds);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Server Error in {nameof(DogInfoController)}, method: {MethodBase.GetCurrentMethod()?.Name}.", ex);
                return StatusCode(500);
            }
        }
    }
}
