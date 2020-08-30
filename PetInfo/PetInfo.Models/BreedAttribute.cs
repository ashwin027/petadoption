using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PetInfo.Models
{
    public class BreedAttribute
    {
        [JsonProperty("imperial")]
        public string Imperial { get; set; }

        [JsonProperty("metric")]
        public string Metric { get; set; }
    }
}
