using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PetAdoption.Models.Config
{
    public class SpaConfig
    {
        [JsonProperty("petInfoApiBaseUrl")]
        public string PetInfoApiBaseUrl { get; set; }
        [JsonProperty("userPetInfoApiBaseUrl")]
        public string UserPetInfoApiBaseUrl { get; set; }
    }
}
