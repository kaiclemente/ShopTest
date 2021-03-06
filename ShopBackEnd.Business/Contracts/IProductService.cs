﻿using ShopBackEnd.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBackEnd.Business
{
    public interface IProductService: IService<Product>
    {
        Product BestProduct();
    }
}
