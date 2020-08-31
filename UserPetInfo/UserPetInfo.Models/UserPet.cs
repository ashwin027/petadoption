using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UserPetInfo.Models
{
    public class UserPet
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("breedId")]
        public int BreedId{ get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("images")]
        public List<Image> Images { get; set; }
    }
}
