using AutoMapper;
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
        }
    }
}
