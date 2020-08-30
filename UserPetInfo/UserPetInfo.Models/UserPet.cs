using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace UserPetInfo.Models
{
    public class UserPet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BreedId{ get; set; }
        public string UserId { get; set; }
        public List<Image> Images { get; set; }
    }
}
