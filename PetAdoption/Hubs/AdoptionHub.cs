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
        public async Task NotifyAdoptionRequest(string receiverUserName, string petName)
        {
            try
            {
                var notification = new Notification()
                {
                    NotificationId = Guid.NewGuid(),
                    Message = $"Adoption request received for {petName}"
                };
                await Clients.User(receiverUserName).SendAsync("onNotificationReceived", notification);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred when attempting to send an adoption request notification. Details: {ex.Message}");
                throw ex;
            }
            
        }
    }
}
