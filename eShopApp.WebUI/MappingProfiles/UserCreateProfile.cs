using AutoMapper;
using eShopApp.WebUI.Identity.Entities;
using eShopApp.WebUI.Models.IdentityModels.User;

namespace eShopApp.WebUI.MappingProfiles
{
    public class UserCreateProfile : Profile
    {
        public UserCreateProfile()
        {
            CreateMap<AppUser, UserCreateModel>()
                .ForMember(userCreate => userCreate.FirstName, member2 => member2.MapFrom(appUser => appUser.FirstName))
                .ForMember(userCreate => userCreate.LastName,  member2 => member2.MapFrom(appUser => appUser.LastName))
                .ForMember(userCreate => userCreate.UserName,  member2 => member2.MapFrom(appUser => appUser.UserName))
                .ForMember(userCreate => userCreate.Email,     member2 => member2.MapFrom(appUser => appUser.Email));

            CreateMap<UserCreateModel, AppUser>()
                .ForMember(appUser => appUser.FirstName, member2 => member2.MapFrom(userCreate => userCreate.FirstName))
                .ForMember(appUser => appUser.LastName,  member2 => member2.MapFrom(userCreate => userCreate.LastName))
                .ForMember(appUser => appUser.UserName,  member2 => member2.MapFrom(userCreate => userCreate.UserName))
                .ForMember(appUser => appUser.Email,     member2 => member2.MapFrom(userCreate => userCreate.Email));
        }
    }
}