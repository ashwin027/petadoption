using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Eventing
{
    public interface IProducerWrapper
    {
        Task Produce<T>(string topic, T message);
    }
}
