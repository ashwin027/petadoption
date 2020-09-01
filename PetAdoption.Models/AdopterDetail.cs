using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PetAdoption.Models
{
    public class AdopterDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("userPetId")]
        public int UserPetId { get; set; }
        [JsonProperty("givenName")]
        public string GivenName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("telephone")]
        public string Telephone { get; set; }
        [JsonProperty("adoptionId")]
        public int AdoptionId { get; set; }
    }
}
