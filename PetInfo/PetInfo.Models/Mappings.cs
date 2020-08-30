using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PetInfo.Models.Entities;

namespace PetInfo.Models
{
    public class Mappings: Profile
    {
        public Mappings()
        {
            CreateMap<BreedAttributeEntity, BreedAttribute>();
            CreateMap<DogBreedInfoEntity, DogBreedInfo>()
                .ForMember(dest => dest.BredFor, opt => opt.MapFrom(src => src.bred_for))
                .ForMember(dest => dest.BreedGroup, opt => opt.MapFrom(src => src.breed_group))
                .ForMember(dest => dest.LifeSpan, opt => opt.MapFrom(src => src.life_span));
        }
    }
}
