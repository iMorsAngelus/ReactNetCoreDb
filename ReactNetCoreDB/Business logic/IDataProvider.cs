using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactNetCoreDB.Models;

namespace ReactNetCoreDB.Business_logic
{
    public interface IDataProvider
    {
        IQueryable<Product> Product { get; }
        IQueryable<ProductCategory> ProductCategory { get; }
        IQueryable<ProductDescription> ProductDescription { get; }
        IQueryable<ProductModel> ProductModel { get; }
        IQueryable<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture { get; }
        IQueryable<ProductPhoto> ProductPhoto { get; }
        IQueryable<ProductProductPhoto> ProductProductPhoto { get; }
        IQueryable<ProductSubcategory> ProductSubcategory { get; }
        IQueryable<TransactionHistory> TransactionHistory { get; }
    }
}
