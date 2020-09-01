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
        [JsonProperty("userPetId")]
        public int UserPetId { get; set; }
        [JsonProperty("petName")]
        public string PetName { get; set; }
        [JsonProperty("status")]
        public AdoptionStatus Status { get; set; }
        [JsonProperty("fees")]
        public double Fees { get; set; }
        [JsonProperty("breedName")]
        public string BreedName { get; set; }
        [JsonProperty("additionalRequirements")]
        public string AdditionalRequirements { get; set; }
        [JsonProperty("adopterDetailId")]
        public int AdopterDetailId { get; set; }
    }
}
