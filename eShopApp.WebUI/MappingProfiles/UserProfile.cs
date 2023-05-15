using AutoMapper;
using eShopApp.WebUI.Identity.Entities;
using eShopApp.WebUI.Models.IdentityModels;

namespace eShopApp.WebUI.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, SignUpModel>()
                .ForMember(regModel => regModel.FirstName, member2 => member2.MapFrom(appUser => appUser.FirstName))
                .ForMember(regModel => regModel.LastName, member2 => member2.MapFrom(appUser => appUser.LastName))
                .ForMember(regModel => regModel.UserName, member2 => member2.MapFrom(appUser => appUser.UserName))
                .ForMember(regModel => regModel.Email, member2 => member2.MapFrom(appUser => appUser.Email));

            CreateMap<SignUpModel, AppUser>()
                .ForMember(appUser => appUser.FirstName, member2 => member2.MapFrom(regModel => regModel.FirstName))
                .ForMember(appUser => appUser.LastName, member2 => member2.MapFrom(regModel => regModel.LastName))
                .ForMember(appUser => appUser.UserName, member2 => member2.MapFrom(regModel => regModel.UserName))
                .ForMember(appUser => appUser.Email, member2 => member2.MapFrom(regModel => regModel.Email));
        }
    }
}
