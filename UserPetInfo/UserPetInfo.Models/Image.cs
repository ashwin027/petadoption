﻿using System;
using System.Collections.Generic;
using System.Text;

namespace UserPetInfo.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }

        public int PetId { get; set; }
    }
}
