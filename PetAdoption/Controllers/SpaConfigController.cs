using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetAdoption.Models.Config;

namespace PetAdoption.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class SpaConfigController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SpaConfigController> _logger;
        private readonly ApiSettingsOptions _apiSettings;

        public SpaConfigController(ILogger<SpaConfigController> logger, IMapper mapper, IOptions<ApiSettingsOptions> options)
        {
            _logger = logger;
            _mapper = mapper;
            _apiSettings = options.Value;
        }

        [HttpGet]
        public async Task<ActionResult<SpaConfig>> GetSpaConfig()
        {
            try
            {
                var config = _apiSettings.SpaConfig;

                return Ok(config);

            }
            catch (Exception ex)
            {
                _logger.LogError("Server Error in SpaConfigController, method: GetSpaConfig().", ex);
                return StatusCode(500);
            }
        }
    }
}
