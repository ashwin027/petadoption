using System;
using System.Collections.Generic;
using System.Text;

namespace PetAdoption.Models.Entities
{
    public class AdopterDetailEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string GivenName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }

        // Foreign key
        public int AdoptionId { get; set; }
        public AdoptionEntity Adoption { get; set; }
    }
}
