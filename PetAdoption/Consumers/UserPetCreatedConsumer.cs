using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetAdoption.Eventing.Messages;
using PetAdoption.Hubs;
using PetAdoption.Models;
using PetAdoption.Models.Common;
using PetAdoption.Models.Config;

namespace PetAdoption.Consumers
{
    public class UserPetCreatedConsumer : BackgroundService
    {
        private readonly ILogger<UserPetCreatedConsumer> _logger;
        private readonly ApiSettingsOptions _apiSettings;

        // TODO: Move all strings to constants in the models project
        private const string Method = "userPetCreated";
        private readonly IHubContext<AdoptionHub> _hubContext;
        public UserPetCreatedConsumer(IOptions<ApiSettingsOptions> options, ILogger<UserPetCreatedConsumer> logger, IHubContext<AdoptionHub> hubContext)
        {
            _apiSettings = options.Value;
            _logger = logger;
            _hubContext = hubContext;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var groupId = $"{UserPetCreatedMessage.Topic}-{Constants.TopicGroupSuffix}";
            var conf = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = _apiSettings.EventingConfig.SystemUrlList,
                AllowAutoCreateTopics = true
            };

            using (var c = new ConsumerBuilder<Ignore, UserPetCreatedMessage>(conf)
                .SetValueDeserializer(new CustomDeserializer<UserPetCreatedMessage>())
                .Build())
            {
                c.Subscribe(UserPetCreatedMessage.Topic);

                try
                {
                    await Task.Run(async () =>
                    {
                        while (!stoppingToken.IsCancellationRequested)
                        {
                            try
                            {
                                var cr = c.Consume(stoppingToken);
                                if (cr.Message.Value != null)
                                {
                                    var userPetCreatedMsg = cr.Message.Value;

                                    _logger.LogInformation($"Consumed user created message with user pet id  '{userPetCreatedMsg.UserPetId}' at: '{cr.TopicPartitionOffset}'.");

                                    // Send the user pet created message to the user
                                    await _hubContext.Clients.User(userPetCreatedMsg.UserId)
                                        .SendAsync(Method, userPetCreatedMsg.UserPetId, stoppingToken);
                                }
                            }
                            catch (ConsumeException e)
                            {
                                _logger.LogError($"Error occurred: {e.Error.Reason}");
                            }
                        }
                    });

                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    c.Close();
                }
            }
        }
    }
}
