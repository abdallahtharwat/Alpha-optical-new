﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.DataAccess.Repository.IRepository
{
    public interface IUnitofWork
    {
        ICategoryRepository Category { get; }

        IBrandRepository Brand { get; }

        ILenstypeRepository Lenstype { get; }

        IProductRepository Product { get; }
        IProductImageRepository ProductImage { get; }

        ICompanyRepository Company { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }

        IApplicationUserRepository ApplicationUser { get; }

        void Save();

    }
}
