using System;
using System.Collections.Generic;
using System.Linq;
using ReactNetCoreDB.Data_structure;
using ReactNetCoreDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ReactNetCoreDB.Business_logic
{
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 10000)]
    public class DbQuery : IDbQuery
    {
        private const string bikesCategory = "Bikes";
        private const string sell = "S";
        IDataAccessLayer db;

        public DbQuery(IDataAccessLayer db)
        {
            this.db = db;
        }

        public IEnumerable<dataBikes> AllBikes()
        {
            //Use join, because of decreasing speed by subquery method         
            var bikes = from bike in db.getProduct()
                        //Join populary bike
                        join pop in PopularyBikes() on bike.ProductId equals pop.Key into leftJoin
                        //join photo
                        join productPhoto in db.getProductProductPhoto() on bike.ProductId equals productPhoto.ProductId
                        join photo in db.getProductPhoto() on productPhoto.ProductPhotoId equals photo.ProductPhotoId
                        //Filter by category
                        join productSubCategory in db.getProductSubcategory() on bike.ProductSubcategoryId equals productSubCategory.ProductSubcategoryId
                        join productCategory in db.getProductCategory() on productSubCategory.ProductCategoryId equals productCategory.ProductCategoryId
                        //Left join for get all bikes (Sold and not sold)
                        from pop in leftJoin.DefaultIfEmpty()
                        where productCategory.Name.Equals(bikesCategory)
                        //Select main field
                        select new
                        {
                            id = bike.ProductId,
                            name = bike.Name,
                            price = bike.ListPrice,
                            sell_count = (pop == null) ? 0 : pop.Select(x => x.Quantity).Sum(),
                            image = photo.LargePhoto,
                        };
            bikes = bikes.OrderByDescending(x => x.sell_count);
            return bikes
                .ToList()
                .Select(bike => new dataBikes
                {
                    id = bike.id,
                    name = bike.name,
                    price = bike.price,
                    sell_count = bike.sell_count,
                    image = bike.image
                });
        }

        public IEnumerable<dataBikesDetails> AllBikesDetails()
        {
            var BikeDetails = from product in db.getProduct()
                                  //Join description
                              join model in db.getProductModerPDC() on product.ProductModelId equals model.ProductModelId
                              //Join photo
                              join productPhoto in db.getProductProductPhoto() on product.ProductId equals productPhoto.ProductId
                              join photo in db.getProductPhoto() on productPhoto.ProductPhotoId equals photo.ProductPhotoId
                              //Filter by category
                              join subCategory in db.getProductSubcategory() on product.ProductSubcategoryId equals subCategory.ProductSubcategoryId
                              join category in db.getProductCategory() on subCategory.ProductCategoryId equals category.ProductCategoryId
                              where category.Name.Equals(bikesCategory)
                              //Select main fiield
                              select new
                              {
                                  id = product.ProductId,
                                  description = model.ProductDescription.Description,
                                  name = product.Name,
                                  weight = product.Weight,
                                  Class = product.Class,
                                  style = product.Style,
                                  image = photo.LargePhoto,
                                  color = product.Color,
                                  size = product.Size,
                                  safety = product.SafetyStockLevel
                              };
            return BikeDetails
                .ToList()
                .Select(details => new dataBikesDetails
                {
                    id = details.id,
                    description = details.description,
                    name = details.name,
                    weight = details.weight,
                    Class = details.Class,
                    style = details.style,
                    image = details.image,
                    color = details.color,
                    size = details.size,
                    safety = details.safety
                });
        }

        private IQueryable<IGrouping<int, TransactionHistory>> PopularyBikes()
        {
            return from product in db.getProduct()
                   join transactionHistory in db.getTransactionHistory() on product.ProductId equals transactionHistory.ProductId
                   join productSubCategory in db.getProductSubcategory() on product.ProductSubcategoryId equals productSubCategory.ProductSubcategoryId
                   join productCategory in db.getProductCategory() on productSubCategory.ProductCategoryId equals productCategory.ProductCategoryId
                   where transactionHistory.TransactionType.Equals(sell) && productCategory.Name.Equals(bikesCategory)
                   group transactionHistory by transactionHistory.ProductId into top
                   select top;
        }
    }
}
