using System;
using System.Collections.Generic;
using System.Linq;
using ReactNetCoreDB.Models;

namespace ReactNetCoreDB.Business_logic
{
    public interface IDataAccessLayer
    {
        IQueryable<Product> getProduct();
        IQueryable<ProductCategory> getProductCategory();
        IQueryable<ProductDescription> getProductDescription();
        IQueryable<ProductModel> getProductModel();
        IQueryable<ProductModelProductDescriptionCulture> getProductModerPDC();
        IQueryable<ProductPhoto> getProductPhoto();
        IQueryable<ProductProductPhoto> getProductProductPhoto();
        IQueryable<ProductSubcategory> getProductSubcategory();
        IQueryable<TransactionHistory> getTransactionHistory();
    }
}
