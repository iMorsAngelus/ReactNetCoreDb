using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReactNetCoreDB.Models;
using ReactNetCoreDB.Data_structure;
using Microsoft.AspNetCore.Mvc;

namespace ReactNetCoreDB.Business_logic
{
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 2)]
    public class DataAccessLayer:IDataAccessLayer
    {
        private const string bikesCategory = "Bikes";
        private const string sell = "S";
        protected readonly AdventureWorks2014Context context;
        protected List<dataBikes> AllBikes;
        protected List<dataBikesDetails> AllBikesDetails;

        public DataAccessLayer(DbContextOptions<AdventureWorks2014Context> option)
        {
            context = new AdventureWorks2014Context(option);
            //Initialize data for client
            InitializeAllBikes();
            InitializeAllBikesDetails();
        }

        public IEnumerable<dataBikes> GetAllBikes()
        {
            return AllBikes;
        }

        public IEnumerable<dataBikesDetails> GetAllBikesDetails()
        {
            return AllBikesDetails;
        }

        public IEnumerable<dataBikes> GetTopBikes()
        {
            return AllBikes.Take(5);
        }

        private IQueryable<IGrouping<int, TransactionHistory>> PopularyBikes()
        {
            return from product in context.Product
                   join transactionHistory in context.TransactionHistory on product.ProductId equals transactionHistory.ProductId
                   join productSubCategory in context.ProductSubcategory on product.ProductSubcategoryId equals productSubCategory.ProductSubcategoryId
                   join productCategory in context.ProductCategory on productSubCategory.ProductCategoryId equals productCategory.ProductCategoryId
                   where transactionHistory.TransactionType.Equals(sell) && productCategory.Name.Equals(bikesCategory)
                   group transactionHistory by transactionHistory.ProductId into top
                   select top;
        }

        private void InitializeAllBikes()
        {
            //Use join, because of decreasing speed by subquery method         
            var bikes = from bike in context.Product
                            //Join populary bike
                        join pop in PopularyBikes() on bike.ProductId equals pop.Key into leftJoin
                        //join photo
                        join productPhoto in context.ProductProductPhoto on bike.ProductId equals productPhoto.ProductId
                        join photo in context.ProductPhoto on productPhoto.ProductPhotoId equals photo.ProductPhotoId
                        //Filter by category
                        join productSubCategory in context.ProductSubcategory on bike.ProductSubcategoryId equals productSubCategory.ProductSubcategoryId
                        join productCategory in context.ProductCategory on productSubCategory.ProductCategoryId equals productCategory.ProductCategoryId
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
            //Order by count of sells
            bikes = bikes.OrderByDescending(x => x.sell_count);

            //Set value
            AllBikes =  bikes
                        .ToList()
                        .Select(bike => new dataBikes
                        {
                            id = bike.id,
                            name = bike.name,
                            price = bike.price,
                            sell_count = bike.sell_count,
                            image = bike.image
                        }).ToList<dataBikes>();
        }

        private void InitializeAllBikesDetails()
        {
            var BikeDetails = from product in context.Product
                                  //Join description
                              join model in context.ProductModelProductDescriptionCulture on product.ProductModelId equals model.ProductModelId
                              //Join photo
                              join productPhoto in context.ProductProductPhoto on product.ProductId equals productPhoto.ProductId
                              join photo in context.ProductPhoto on productPhoto.ProductPhotoId equals photo.ProductPhotoId
                              //Filter by category
                              join subCategory in context.ProductSubcategory on product.ProductSubcategoryId equals subCategory.ProductSubcategoryId
                              join category in context.ProductCategory on subCategory.ProductCategoryId equals category.ProductCategoryId
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

            //Set value
            AllBikesDetails = BikeDetails
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
                }).ToList<dataBikesDetails>();
        }
    }
}
