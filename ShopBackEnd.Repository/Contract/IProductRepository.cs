﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBackEnd.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Product BestProduct();
    }
}
