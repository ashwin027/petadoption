using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PetAdoption.Models.Entities;

namespace PetAdoption.Repository
{
    // To add a migrations, navigate to the repository folder in powershell and run the command,
    // dotnet ef migrations add Additional-cols --startup-project "..\PetAdoption"
    public class AdoptionContext: DbContext
    {
        public DbSet<AdoptionEntity> Adoptions { get; set; }

        public AdoptionContext(DbContextOptions<AdoptionContext> options) : base(options)
        {
        }
    }
}
