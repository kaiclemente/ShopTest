using AutoMapper;
using ShopBackEnd.Repository;
using ShopBackEnd.Model;


namespace ShopBackEnd.Business.Mapping
{
    public class ProductMapping : Profile
    {
        protected override void Configure()
        {
            base.Configure();

            Mapper.CreateMap<Product, Model.DTO.Product>();
            Mapper.CreateMap<Model.DTO.Product, Product>();
        }
    }
}
