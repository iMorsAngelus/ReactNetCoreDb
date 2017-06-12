//[Route("api/[controller]")]
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactNetCoreDB.Models;

namespace NetCoreDB.Controllers
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
        public IEnumerable<object> AllBikes()
        {
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
                        join pop in populary on bike.ProductId equals pop.id into leftJoin
                        join productSubCategory in db.ProductSubcategory on bike.ProductSubcategoryId equals productSubCategory.ProductSubcategoryId
                        join productCategory in db.ProductCategory on productSubCategory.ProductCategoryId equals productCategory.ProductCategoryId
                        from pop in leftJoin.DefaultIfEmpty()
                        where productCategory.Name.Equals("Bikes")
                        select new
                        {
                            id = bike.ProductId,
                            name = bike.Name,
                            price = bike.ListPrice,
                            sell_count = (pop == null)? 0:pop.sell_count,
                            image = bike.ProductProductPhoto.Select(x => x.ProductPhoto.LargePhoto).FirstOrDefault(),
                        };
            bikes = bikes.OrderByDescending(x => x.sell_count);
            return bikes.ToList();
        }

        [HttpGet("/AllBikesDetails")]
        public IEnumerable<object> AllBikesDetails(string searchString)
        {
            var BikeDetails = from product in db.Product
                              join subCategory in db.ProductSubcategory on product.ProductSubcategoryId equals subCategory.ProductSubcategoryId
                              join category in db.ProductCategory on subCategory.ProductCategoryId equals category.ProductCategoryId
                              where category.Name.Equals("Bikes")
                              select new
                              {
                                  id = product.ProductId,
                                  description = product.ProductModel.ProductModelProductDescriptionCulture.Select(x => x.ProductDescription).FirstOrDefault(),
                                  name = product.Name,
                                  weight = product.Weight,
                                  Class = product.Class,
                                  style = product.Style,
                                  image = product.ProductProductPhoto.Select(x => x.ProductPhoto.LargePhoto).FirstOrDefault(),
                                  color = product.Color,
                                  size = product.Size,
                                  safety = product.SafetyStockLevel
                              };
            return BikeDetails.ToList();
        }
    }
}
