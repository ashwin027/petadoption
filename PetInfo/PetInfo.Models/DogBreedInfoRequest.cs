using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PetInfo.Models
{
    public class DogBreedInfoRequest
    {
        [JsonProperty("attach_breed")]
        public string AttachBreed { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }


    }
}
