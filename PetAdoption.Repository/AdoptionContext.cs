using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Models.Entities;

namespace PetAdoption.Repository
{
    public class AdoptionContext: DbContext
    {
        public DbSet<AdoptionEntity> Adoptions { get; set; }

        public AdoptionContext(DbContextOptions<AdoptionContext> options) : base(options)
        {
        }
    }
}
