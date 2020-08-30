using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using UserPetInfo.Models.Common;
using UserPetInfo.Models.Entities;

namespace UserPetInfo.Models
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<ImageEntity, Image>();
            CreateMap<Image, ImageEntity>();

            CreateMap<UserPetEntity, UserPet>();
            CreateMap<UserPet, UserPetEntity>();

            //CreateMap<PaginatedList<UserPetEntity>, PaginatedList<UserPet>>();
            //CreateMap<PaginatedList<UserPet>, PaginatedList<UserPetEntity>>();
        }
    }
}
