using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PetAdoption.Models.Entities;

namespace PetAdoption.Models
{
    public class Mappings: Profile
    {
        public Mappings()
        {
            CreateMap<AdoptionEntity, Adoption>();
            CreateMap<Adoption, AdoptionEntity>().ForMember(src => src.AdopterDetails, opt => opt.Ignore());

            CreateMap<AdopterDetail, AdopterDetailEntity>().ForMember(src => src.Adoption, opt => opt.Ignore());
            CreateMap<AdopterDetailEntity, AdopterDetail>();
        }
    }
}
