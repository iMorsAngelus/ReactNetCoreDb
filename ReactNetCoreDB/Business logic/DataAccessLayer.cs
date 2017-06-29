using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReactNetCoreDB.Models;
using ReactNetCoreDB.Data_structure;
using System.Threading;
using System.Threading.Tasks;

namespace ReactNetCoreDB.Business_logic
{
    public class DataAccessLayer:IDataAccessLayer
    {
        private const string bikesCategory = "Bikes";
        private const int CashDelay = 10; //Minutes
        private const string sell = "S";
        protected readonly IDataProvider providerBikes;
        protected readonly IDataProvider providerDetails;
        protected List<dataBikes> AllBikes = null;
        protected List<dataBikesDetails> AllBikesDetails = null;

        public DataAccessLayer(IDataProvider providerBikes, IDataProvider providerDetails)
        {
            this.providerBikes = providerBikes;
            this.providerDetails = providerDetails;
            Cashe();
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
            return from product in providerBikes.Product
                   join transactionHistory in providerBikes.TransactionHistory on product.ProductId equals transactionHistory.ProductId
                   join productSubCategory in providerBikes.ProductSubcategory on product.ProductSubcategoryId equals productSubCategory.ProductSubcategoryId
                   join productCategory in providerBikes.ProductCategory on productSubCategory.ProductCategoryId equals productCategory.ProductCategoryId
                   where transactionHistory.TransactionType.Equals(sell) && productCategory.Name.Equals(bikesCategory)
                   group transactionHistory by transactionHistory.ProductId into top
                   select top;
        }

        private void Cashe()
        {
            if (AllBikes == null && AllBikesDetails == null)
            {
                InitializeAllBikes();
                InitializeAllBikesDetails();
            }

            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(CashDelay * 60000);
                InitializeAllBikes();
                InitializeAllBikesDetails();
                Cashe();
            });
        }

        private void InitializeAllBikes()
        {
            //Use join, because of decreasing speed by subquery method         
            var bikes = from bike in providerBikes.Product
                        //Join populary bike
                        join pop in PopularyBikes() on bike.ProductId equals pop.Key into leftJoin
                        //join photo
                        join productPhoto in providerBikes.ProductProductPhoto on bike.ProductId equals productPhoto.ProductId
                        join photo in providerBikes.ProductPhoto on productPhoto.ProductPhotoId equals photo.ProductPhotoId
                        //Filter by category
                        join productSubCategory in providerBikes.ProductSubcategory on bike.ProductSubcategoryId equals productSubCategory.ProductSubcategoryId
                        join productCategory in providerBikes.ProductCategory on productSubCategory.ProductCategoryId equals productCategory.ProductCategoryId
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
            AllBikes = bikes
                        .AsNoTracking()
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
            var BikeDetails = from product in providerDetails.Product
                                  //Join description
                              join model in providerDetails.ProductModelProductDescriptionCulture on product.ProductModelId equals model.ProductModelId
                              //Join photo
                              join productPhoto in providerDetails.ProductProductPhoto on product.ProductId equals productPhoto.ProductId
                              join photo in providerDetails.ProductPhoto on productPhoto.ProductPhotoId equals photo.ProductPhotoId
                              //Filter by category
                              join subCategory in providerDetails.ProductSubcategory on product.ProductSubcategoryId equals subCategory.ProductSubcategoryId
                              join category in providerDetails.ProductCategory on subCategory.ProductCategoryId equals category.ProductCategoryId
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
                .AsNoTracking()
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
