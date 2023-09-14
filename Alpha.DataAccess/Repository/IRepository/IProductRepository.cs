﻿using Alpha.Models;
using Alpha.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {

        void Update(Product obj);

        //IEnumerable<Product> GetNewProducts(int days);
        //IEnumerable<Product> GetOldProducts(int days);


    }
}
