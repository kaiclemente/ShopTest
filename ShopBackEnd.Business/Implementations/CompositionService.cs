using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShopBackEnd.Repository;

namespace ShopBackEnd.Business
{
    public class CompositionService : ICompositionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingEngine _mapper;

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }

        public IMappingEngine Mapper
        {
            get { return _mapper; }
        }


        public CompositionService(
            IUnitOfWork unitOfWork,
            IMappingEngine mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
