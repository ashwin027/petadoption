using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using UserPetInfo.Api.Producers;
using UserPetInfo.Models;
using UserPetInfo.Models.Common;
using UserPetInfo.Models.Config;
using UserPetInfo.Models.Entities;
using UserPetInfo.Models.Messages;
using UserPetInfo.Repository;

namespace UserPetInfo.Api.Consumers
{
    public class UserPetCreationConsumer : BackgroundService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserPetCreationConsumer> _logger;
        private readonly ApiSettingsOptions _apiSettings;
        private readonly IServiceProvider _serviceProvider;

        // TODO: Move all strings to constants in the models project
        private const string GroupId = "userpetinfo-userpetcreation-group";
        private const string Topic = "UserPetCreation";
        private const string ProductionTopic = "UserPetCreated";
        public UserPetCreationConsumer(IOptions<ApiSettingsOptions> options, ILogger<UserPetCreationConsumer> logger, IServiceProvider serviceProvider, IMapper mapper)
        {
            _apiSettings = options.Value;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerConfig = new ConsumerConfig
            {
                GroupId = GroupId,
                BootstrapServers = _apiSettings.KafkaSettings.BrokerList,
                AllowAutoCreateTopics = true
            };

            var producerConfig = new ProducerConfig { BootstrapServers = _apiSettings.KafkaSettings.BrokerList };

            // Expecting the user pet id from the previous owner to create a record for the new owner using the previous pet details
            using (var c = new ConsumerBuilder<Ignore, UserPetCreationMessage>(consumerConfig)
                .SetValueDeserializer(new CustomDeserializer<UserPetCreationMessage>())
                .Build())
            {
                c.Subscribe(Topic);

                try
                {
                    await Task.Run(async () =>
                    {
                        while (!stoppingToken.IsCancellationRequested)
                        {
                            try
                            {
                                var consumeResult = c.Consume(stoppingToken);
                                if (consumeResult.Message.Value != null)
                                {
                                    using (var scope = _serviceProvider.CreateScope())
                                    {
                                        var userPetRepository = scope.ServiceProvider.GetRequiredService<IUserPetRepository>();
                                        var userPetCreationMessage = consumeResult.Message.Value;

                                        _logger.LogInformation($"Consumed user creation message with user id  '{userPetCreationMessage.UserId}' at: '{consumeResult.TopicPartitionOffset}'.");

                                        var previousPetDetails = await userPetRepository.GetUserPet(userPetCreationMessage.PreviousUserPetId);

                                        var newUserPet = new UserPet()
                                        {
                                            UserId = userPetCreationMessage.UserId,
                                            BreedId = previousPetDetails.BreedId,
                                            Name = previousPetDetails.Name,
                                            Description = previousPetDetails.Description,
                                            Gender = previousPetDetails.Gender,
                                        };

                                        var newUserPetEntity = _mapper.Map<UserPetEntity>(newUserPet);

                                        var result = await userPetRepository.CreateUserPet(newUserPetEntity);

                                        if (result == null)
                                        {
                                            _logger.LogError($"Creation of pet for user  '{userPetCreationMessage.UserId}' failed.");
                                            _logger.LogError(
                                                $"original message: {JsonConvert.SerializeObject(userPetCreationMessage)}");
                                            continue;
                                        }

                                        var createdUserPet = _mapper.Map<UserPet>(result);
                                        var userPetCreatedMessage = new UserPetCreatedMessage()
                                        {
                                            UserId = createdUserPet.UserId,
                                            UserPetId = createdUserPet.Id
                                        };

                                        var producerWrapper = scope.ServiceProvider.GetRequiredService<ProducerWrapper>();
                                        await producerWrapper.Produce(ProductionTopic, userPetCreatedMessage);
                                    }
                                }
                            }
                            catch (ConsumeException e)
                            {
                                Console.WriteLine($"Error occurred: {e.Error.Reason}");
                            }
                        }
                    }, stoppingToken);
                    
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
