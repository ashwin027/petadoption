using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetInfo.Models
{
    public class DogBreedInfo
    {
        [JsonProperty("bredFor")]
        public string BredFor { get; set; }

        [JsonProperty("breedGroup")]
        public string BreedGroup { get; set; }

        [JsonProperty("height")]
        public BreedAttribute Height { get; set; }

        [JsonProperty("id")]
        public int Id { get; set;}

        [JsonProperty("lifeSpan")]
        public string LifeSpan { get; set; }

        [JsonProperty("name")] 
        public string Name { get; set; }

        [JsonProperty("temperament")]
        public string Temperament { get; set; }

        [JsonProperty("weight")]
        public BreedAttribute Weight { get; set; }
    }
}
