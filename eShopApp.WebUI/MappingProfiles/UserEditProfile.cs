using AutoMapper;
using eShopApp.WebUI.Identity.Entities;
using eShopApp.WebUI.Models.IdentityModels.User;

namespace eShopApp.WebUI.MappingProfiles
{
    public class UserEditProfile : Profile
    {
        public UserEditProfile()
        {
            CreateMap<AppUser, UserEditModel>()
                .ForMember(userEdit => userEdit.FirstName,      member2 => member2.MapFrom(appUser => appUser.FirstName))
                .ForMember(userEdit => userEdit.LastName,       member2 => member2.MapFrom(appUser => appUser.LastName))
                .ForMember(userEdit => userEdit.UserName,       member2 => member2.MapFrom(appUser => appUser.UserName))
                .ForMember(userEdit => userEdit.Email,          member2 => member2.MapFrom(appUser => appUser.Email))
                .ForMember(userEdit => userEdit.EmailConfirmed, member2 => member2.MapFrom(appUser => appUser.EmailConfirmed));

            CreateMap<UserEditModel, AppUser>()
                .ForMember(appUser => appUser.FirstName,      member2 => member2.MapFrom(userEdit => userEdit.FirstName))
                .ForMember(appUser => appUser.LastName,       member2 => member2.MapFrom(userEdit => userEdit.LastName))
                .ForMember(appUser => appUser.UserName,       member2 => member2.MapFrom(userEdit => userEdit.UserName))
                .ForMember(appUser => appUser.Email,          member2 => member2.MapFrom(userEdit => userEdit.Email))
                .ForMember(appUser => appUser.EmailConfirmed, member2 => member2.MapFrom(userEdit => userEdit.EmailConfirmed));
        }
    }
}