using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PetAdoption.Models.Common
{
    public class CustomDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            try
            {
                var array = data.ToArray();
                using (var stream = new MemoryStream(array))
                using (var sr = new StreamReader(stream, Encoding.UTF8))
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
