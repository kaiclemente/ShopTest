using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBackEnd.Repository
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(IDbContext context)
            :base(context)
        {
            
        }

        public Product BestProduct()
        {
            return DbSet.First();
        }
    }
}
