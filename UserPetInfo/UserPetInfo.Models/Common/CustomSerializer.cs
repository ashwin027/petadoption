using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Confluent.Kafka;

namespace UserPetInfo.Models.Common
{
    public class CustomSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            if (data == null) { return null; }

            var serializedString = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(System.Text.Encoding.UTF8.GetBytes(serializedString));
                return stream.ToArray();
            }
        }
    }
}
