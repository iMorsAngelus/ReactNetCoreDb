using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactNetCoreDB.Models;

namespace ReactNetCoreDB.Business_logic
{
    public class DataProvider:IDataProvider
    {
        private AdventureWorks2014Context context;

        public DataProvider(AdventureWorks2014Context context)
        {
            this.context = context;
        }

        public IQueryable<Product> Product => context.Product;
        public IQueryable<ProductCategory> ProductCategory => context.ProductCategory;
        public IQueryable<ProductDescription> ProductDescription => context.ProductDescription;
        public IQueryable<ProductModel> ProductModel => context.ProductModel;
        public IQueryable<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture => context.ProductModelProductDescriptionCulture;
        public IQueryable<ProductPhoto> ProductPhoto => context.ProductPhoto;
        public IQueryable<ProductProductPhoto> ProductProductPhoto => context.ProductProductPhoto;
        public IQueryable<ProductSubcategory> ProductSubcategory => context.ProductSubcategory;
        public IQueryable<TransactionHistory> TransactionHistory => context.TransactionHistory;
    }
}
