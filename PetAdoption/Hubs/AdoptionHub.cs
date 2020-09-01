using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PetAdoption.Models.Common;

namespace PetAdoption.Hubs
{
    [Authorize]
    public class AdoptionHub : Hub
    {
        private readonly ILogger<AdoptionHub> _logger;
        public AdoptionHub(ILogger<AdoptionHub> logger)
        {
            _logger = logger;
        }
    }
}
