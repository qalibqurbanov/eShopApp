using AutoMapper;
using eShopApp.Entity.Entities;
using eShopApp.WebUI.Models.ViewModels;

namespace eShopApp.WebUI.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>()
                .ForMember(catVM => catVM.CategoryName, member2 => member2.MapFrom(cat => cat.CategoryName));

            CreateMap<CategoryViewModel, Category>()
                .ForMember(cat => cat.CategoryName,     member2 => member2.MapFrom(catVM => catVM.CategoryName));
        }
    }
}