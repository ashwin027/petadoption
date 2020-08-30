using System;
using System.Collections.Generic;
using System.Text;

namespace PetInfo.Models.Entities
{
    public class DogBreedInfoEntity
    {
        public string bred_for { get; set; }

        public string breed_group { get; set; }

        public BreedAttributeEntity height { get; set; }

        public int id { get; set; }

        public string life_span { get; set; }

        public string name { get; set; }

        public string temperament { get; set; }

        public BreedAttributeEntity weight { get; set; }
    }
}
