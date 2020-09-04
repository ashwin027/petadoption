using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using PetAdoption.Eventing.Common;
using PetAdoption.Eventing.Config;

namespace PetAdoption.Eventing
{
    public class ProducerWrapper : IProducerWrapper
    {
        private readonly EventingSystemConfig _eventingSystemConfig;
        public ProducerWrapper(IOptions<EventingSystemConfig> options)
        {
            _eventingSystemConfig = options.Value;
        }
        public async Task Produce<T>(string topic, T message)
        {
            try
            {
                if (_eventingSystemConfig != null)
                {
                    var config = new ProducerConfig { BootstrapServers = _eventingSystemConfig.SystemUrlList };

                    using (var p = new ProducerBuilder<Null, T>(config)
                        .SetValueSerializer(new CustomSerializer<T>())
                        .Build())
                    {
                        try
                        {
                            var dr = await p.ProduceAsync(topic, new Message<Null, T> { Key = null, Value = message });
                        }
                        catch (ProduceException<Null, string> e)
                        {
                            Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Missing eventing system configuration.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
