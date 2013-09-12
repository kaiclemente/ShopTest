using AutoMapper;
using ShopBackEnd.Repository;

namespace ShopBackEnd.Business
{
    public interface ICompositionService
    {
        IUnitOfWork UnitOfWork { get; }
        IMappingEngine Mapper { get; }
    }
}
