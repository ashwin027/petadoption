using System;
using System.Collections.Generic;
using System.Text;

namespace PetAdoption.Models.Entities
{
    public class AdoptionEntity
    {
        public int Id { get; set; }
        public string AdopteeId { get; set; }
        public string AdopterId { get; set; }
        public int UserPetId { get; set; }
        public string PetName { get; set; }
        public AdoptionStatus Status { get; set; }
        public double Fees { get; set; }
        public string BreedName { get; set; }
        public string AdditionalRequirements { get; set; }

        // Foreign key
        public int AdopterDetailId { get; set; }
        public AdopterDetailEntity AdopterDetails { get; set; }
    }
}
