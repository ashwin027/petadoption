using System;
using System.Collections.Generic;
using System.Text;

namespace UserPetInfo.Models.Entities
{
    public class ImageEntity
    {
        public int Id { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }

        public int PetId { get; set; }
    }
}
