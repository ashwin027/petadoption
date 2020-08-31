using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetAdoption.Models
{
    public class Adoption
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("adopteeId")]
        public string AdopteeId { get; set; }
        [JsonProperty("adopterId")]
        public string AdopterId { get; set; }
        [JsonProperty("petId")]
        public int PetId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("fees")]
        public double Fees { get; set; }
    }
}
