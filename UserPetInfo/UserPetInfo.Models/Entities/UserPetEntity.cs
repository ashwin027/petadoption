using System;
using System.Collections.Generic;
using System.Text;

namespace UserPetInfo.Models.Entities
{
    public class UserPetEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BreedId { get; set; }
        public string UserId { get; set; }
        public List<ImageEntity> Images { get; set; }
    }
}
