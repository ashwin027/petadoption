using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetAdoption.Models.Common;
using PetAdoption.Models.Config;
using PetAdoption.Models.Messages;

namespace PetAdoption.Producers
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

        public void Produce<T>(string topic, T message) where T:class
        {
            try
            {
                var producerConfig = new ProducerConfig { BootstrapServers = _apiSettings.KafkaSettings.BrokerList };
                using (var p = new ProducerBuilder<Null, T>(producerConfig)
                    .SetValueSerializer(new CustomSerializer<T>())
                    .Build())
                {
                    p.Produce(topic, new Message<Null, T> { Value = message }, (report) =>
                    {
                        if (report.Error.IsError)
                        {
                            _logger.LogError($"Production of message on topic '{topic}' failed with error reason: '{report.Error.Reason}'.");
                        }
                    });
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
