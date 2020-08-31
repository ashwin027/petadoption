using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UserPetInfo.Models
{
    public class Image
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("imageTitle")]
        public string ImageTitle { get; set; }
        [JsonProperty("imageData")]
        public byte[] ImageData { get; set; }
        [JsonProperty("isAvatar")]
        public bool IsAvatar { get; set; }
        [JsonProperty("petId")]
        public int PetId { get; set; }
    }
}
