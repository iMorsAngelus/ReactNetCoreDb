using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReactNetCoreDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace ReactNetCoreDB.Business_logic
{
    public class DataAccessLayer:IDataAccessLayer
    {
        protected readonly AdventureWorks2014Context context;

        public DataAccessLayer(DbContextOptions<AdventureWorks2014Context> option)
        {
            context = new AdventureWorks2014Context(option);
        }

        public IQueryable<Product> getProduct()
        {
            return context.Product;
        }

        public IQueryable<ProductCategory> getProductCategory()
        {
            return context.ProductCategory;
        }

        public IQueryable<ProductDescription> getProductDescription()
        {
            return context.ProductDescription;
        }

        public IQueryable<ProductModel> getProductModel()
        {
            return context.ProductModel;
        }

        public IQueryable<ProductModelProductDescriptionCulture> getProductModerPDC()
        {
            return context.ProductModelProductDescriptionCulture;
        }

        public IQueryable<ProductPhoto> getProductPhoto()
        {
            return context.ProductPhoto;
        }

        public IQueryable<ProductProductPhoto> getProductProductPhoto()
        {
            return context.ProductProductPhoto;
        }

        public IQueryable<ProductSubcategory> getProductSubcategory()
        {
            return context.ProductSubcategory;
        }

        public IQueryable<TransactionHistory> getTransactionHistory()
        {
            return context.TransactionHistory;
        }
    }
}
