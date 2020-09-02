using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UserPetInfo.Models.Common;
using UserPetInfo.Models.Config;

namespace UserPetInfo.Api.Producers
{
    public class ProducerWrapper
    {
        private readonly ILogger<ProducerWrapper> _logger;
        private readonly ApiSettingsOptions _apiSettings;
        public ProducerWrapper(ILogger<ProducerWrapper> logger, IOptions<ApiSettingsOptions> options)
        {
            _logger = logger;
            _apiSettings = options.Value;
        }

        public async Task Produce<T>(string topic, T message) where T : class
        {
            try
            {
                var config = new ProducerConfig { BootstrapServers = _apiSettings.KafkaSettings.BrokerList };

                using (var p = new ProducerBuilder<Null, T>(config)
                    .SetValueSerializer(new CustomSerializer<T>())
                    .Build())
                {
                    try
                    {
                        var dr = await p.ProduceAsync(topic, new Message<Null, T> { Key = null, Value = message });
                        Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                    }
                    catch (ProduceException<Null, string> e)
                    {
                        Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Production of message on topic '{topic}' failed with error reason: '{ex.Message}'.");
                throw;
            }
        }
    }
}
