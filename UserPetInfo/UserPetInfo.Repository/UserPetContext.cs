using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UserPetInfo.Models;
using UserPetInfo.Models.Entities;

namespace UserPetInfo.Repository
{
    public class UserPetContext: DbContext
    {
        public DbSet<UserPetEntity> UserPets { get; set; }
        public DbSet<ImageEntity> Images { get; set; }

        public UserPetContext(DbContextOptions<UserPetContext> options) : base(options)
        {
        }
    }
}
