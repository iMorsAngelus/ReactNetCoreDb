using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactNetCoreDB.Models;

namespace ReactNetCoreDB.Controllers
{
    public class HomeController : Controller
    {
        AdventureWorks2014Context db;
        //Конструктор
        public HomeController(DbContextOptions<AdventureWorks2014Context> option)
        {
            db = new AdventureWorks2014Context(option);
        }

        [HttpGet("/AllBikes")]
        public JsonResult AllBikes()
        {
            //Use join, because of decreasing speed by subquery method
            var populary = from product in db.Product
                           join transactionHistory in db.TransactionHistory on product.ProductId equals transactionHistory.ProductId
                           join productSubCategory in db.ProductSubcategory on product.ProductSubcategoryId equals productSubCategory.ProductSubcategoryId
                           join productCategory in db.ProductCategory on productSubCategory.ProductCategoryId equals productCategory.ProductCategoryId
                           where transactionHistory.TransactionType.Equals("S") && productCategory.Name.Equals("Bikes")
                           group transactionHistory by transactionHistory.ProductId into top
                           select new
                           {
                               id = top.Key,
                               sell_count = top.Select(x => x.Quantity).Sum()
                           };
            
            var bikes = from bike in db.Product
                        //Join populary bike
                        join pop in populary on bike.ProductId equals pop.id into leftJoin
                        //join photo
                        join productPhoto in db.ProductProductPhoto on bike.ProductId equals productPhoto.ProductId
                        join photo in db.ProductPhoto on productPhoto.ProductPhotoId equals photo.ProductPhotoId
                        //Filter by category
                        join productSubCategory in db.ProductSubcategory on bike.ProductSubcategoryId equals productSubCategory.ProductSubcategoryId
                        join productCategory in db.ProductCategory on productSubCategory.ProductCategoryId equals productCategory.ProductCategoryId
                        //Left join for get all bikes (Sold and not sold)
                        from pop in leftJoin.DefaultIfEmpty()
                        where productCategory.Name.Equals("Bikes")
                        //Select main field
                        select new
                        {
                            id = bike.ProductId,
                            name = bike.Name,
                            price = bike.ListPrice,
                            sell_count = (pop == null)? 0:pop.sell_count,
                            image = photo.LargePhoto,
                        };
            bikes = bikes.OrderByDescending(x => x.sell_count);
            return new JsonResult(bikes.ToList());
        }

        [HttpGet("/AllBikesDetails")]
        public JsonResult AllBikesDetails()
        {
            var BikeDetails = from product in db.Product
                              //Join description
                              join model in db.ProductModelProductDescriptionCulture on product.ProductModelId equals model.ProductModelId
                              //Join photo
                              join productPhoto in db.ProductProductPhoto on product.ProductId equals productPhoto.ProductId
                              join photo in db.ProductPhoto on productPhoto.ProductPhotoId equals photo.ProductPhotoId
                              //Filter by category
                              join subCategory in db.ProductSubcategory on product.ProductSubcategoryId equals subCategory.ProductSubcategoryId
                              join category in db.ProductCategory on subCategory.ProductCategoryId equals category.ProductCategoryId
                              where category.Name.Equals("Bikes")
                              //Select main fiield
                              select new
                              {
                                  id = product.ProductId,
                                  description = model.ProductDescription,
                                  name = product.Name,
                                  weight = product.Weight,
                                  Class = product.Class,
                                  style = product.Style,
                                  image = photo.LargePhoto,
                                  color = product.Color,
                                  size = product.Size,
                                  safety = product.SafetyStockLevel
                              };
            return new JsonResult(BikeDetails.ToList());
        }
    }
}
