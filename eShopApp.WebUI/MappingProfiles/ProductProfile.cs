using AutoMapper;
using eShopApp.Entity.Entities;
using eShopApp.WebUI.Models.ViewModels;

namespace eShopApp.WebUI.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(prodVM => prodVM.ProductName,        member2 => member2.MapFrom(prod => prod.ProductName))
                .ForMember(prodVM => prodVM.ProductDescription, member2 => member2.MapFrom(prod => prod.ProductDescription))
                .ForMember(prodVM => prodVM.ProductPrice,       member2 => member2.MapFrom(prod => prod.ProductPrice))
                .ForMember(prodVM => prodVM.ProductIsHome,      member2 => member2.MapFrom(prod => prod.ProductIsHome))
                .ForMember(prodVM => prodVM.ProductIsApproved,  member2 => member2.MapFrom(prod => prod.ProductIsApproved));

            CreateMap<ProductViewModel, Product>()
                .ForMember(prod => prod.ProductName,            member2 => member2.MapFrom(prodVM => prodVM.ProductName))
                .ForMember(prod => prod.ProductDescription,     member2 => member2.MapFrom(prodVM => prodVM.ProductDescription))
                .ForMember(prod => prod.ProductPrice,           member2 => member2.MapFrom(prodVM => prodVM.ProductPrice))
                .ForMember(prod => prod.ProductIsHome,          member2 => member2.MapFrom(prodVM => prodVM.ProductIsHome));
        }
    }
}