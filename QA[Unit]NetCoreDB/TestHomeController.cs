using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ReactNetCoreDB.Models;
using ReactNetCoreDB.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ReactNetCoreDB.Data_structure;
using ReactNetCoreDB.Business_logic;

namespace ReactNetCoreDB
{
    [TestClass]
    public class TestHomeController
    {
        private AdventureWorks2014Context db = null;
        private dataBikesComparer  bikesComparer = null;
        private dataBikesDetailsComparer detailsComparer = null;
        private DataAccessLayer classDataAccessLayer = null;
        private Mock<IDbQuery> query = null;
        private HomeController testHomeController = null;

        [TestInitialize]
        public void SetupContext()
        {
            //Initialize
            //Comparer create
            bikesComparer = new dataBikesComparer();
            detailsComparer = new dataBikesDetailsComparer();

            var serviceProvider = new ServiceCollection()
                                .AddEntityFrameworkInMemoryDatabase()
                                .BuildServiceProvider();
            var options = new DbContextOptionsBuilder<AdventureWorks2014Context>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .UseInternalServiceProvider(serviceProvider)
                              .Options;

            //Create data
            db = new AdventureWorks2014Context(options);
            CreateTestDataToDB();

            //Test DataAccessLayer
            classDataAccessLayer = new DataAccessLayer(options);
            //Test Controller
            query = new Mock<IDbQuery>();
            testHomeController = new HomeController(query.Object);

        }

        //Test context and query to db
        [TestMethod]
        public void TestDataAccessLayerTopBikes()
        {
            //Arange
            var expected = ExpectedValueBikes().Take(5);
            //Act
            var actual = classDataAccessLayer.GetTopBikes();
            //Assert
            Assert.IsTrue(expected.SequenceEqual(actual, bikesComparer));
        }

        [TestMethod]
        public void TestDataAccessLayerAllBikes()
        {
            //Arange
            var expected = ExpectedValueBikes();
            //Act
            var actual = classDataAccessLayer.GetAllBikes();
            //Assert
            Assert.IsTrue(expected.SequenceEqual(actual, bikesComparer));
        }

        [TestMethod]
        public void TestDataAccessLayerAllBikesDetails()
        {
            //Arange
            var expected = ExpectedValueDetails();
            //Act
            var actual = classDataAccessLayer.GetAllBikesDetails();
            //Assert
            Assert.IsTrue(expected.SequenceEqual(actual, detailsComparer));
        }

        //Test Home Controller
        [TestMethod]
        public void TestControllerBikeDetails()
        {
            var actual = testHomeController.BikeDetails(1);
            query.Verify(Top => Top.BikeDetails(1), Times.Once());
        }

        [TestMethod]
        public void TestControllerFindBikes()
        {
            var actual = testHomeController.FindBikes(0, "name 1");
            query.Verify(Top => Top.FindBikes("name 1", 0), Times.Once());
        }

        [TestMethod]
        public void TestControllerTopBikes()
        {
            var actual = testHomeController.TopBikes();
            query.Verify(Top => Top.TopBikes(), Times.Once());
        }

        private void CreateTestDataToDB()
        {
            int n = 10;
            var productModelProductDescriptionCulture = new List<ProductModelProductDescriptionCulture>();
            var productDescription = new List<ProductDescription>();
            var product = new List<Product>();
            var subCategory = new List<ProductSubcategory>();
            var category = new List<ProductCategory>();
            var model = new List<ProductModel>();
            var transactionHistory = new List<TransactionHistory>();
            var productPhoto = new List<ProductProductPhoto>();
            var photo = new List<ProductPhoto>();
            //Data set
            for (int i = 1; i <= n; i++)
            {
                productModelProductDescriptionCulture.Add(new ProductModelProductDescriptionCulture
                {
                    ProductModelId = i,
                    CultureId = i + "",
                    ProductDescriptionId = (i % 2 != 0) ? 1 : i / 2,
                    ProductDescription = null,
                });
                product.Add(new Product
                {
                    ProductId = i,
                    Name = i + "",
                    ProductNumber = "number " + i,
                    Color = "" + i,
                    SafetyStockLevel = (short)(i * 10),
                    ListPrice = (decimal)(i * 10.10),
                    Size = "" + i,
                    Weight = (decimal)i + (i / 10),
                    Class = "class " + i % 3,
                    Style = "style " + i % 2,
                    ProductSubcategoryId = (i % 3 != 0) ? 4 : i / 3,
                    ProductModelId = i,
                });
                if (i < 6)
                {
                    productDescription.Add(new ProductDescription
                    {
                        ProductDescriptionId = i,
                        Description = "Description " + i,
                    });
                    subCategory.Add(new ProductSubcategory
                    {
                        ProductSubcategoryId = i,
                        ProductCategoryId = (i % 2 == 0) ? 1 : 2,
                    });
                    if (i < 3)
                        category.Add(new ProductCategory
                        {
                            ProductCategoryId = i,
                            Name = (i % 2 == 0) ? "Bikes" : "!Bikes",
                        });
                }
                model.Add(new ProductModel
                {
                    ProductModelId = i,
                });
                transactionHistory.Add(new TransactionHistory
                {
                    TransactionId = i,
                    ProductId = (i % 2 != 0) ? 6 : (i % 2 + 1),
                    Quantity = 5,
                    TransactionType = (i % 2 == 0) ? "S" : "B",
                });
                productPhoto.Add(new ProductProductPhoto
                {
                    ProductId = i,
                    ProductPhotoId = i,
                });
                photo.Add(new ProductPhoto
                {
                    ProductPhotoId = i,
                    LargePhoto = new byte[] { },
                });
            }
            //connect with table
            for (int i = 0; i < 10; i++)
            {
                //Add photo
                product[productPhoto[i].ProductId - 1].ProductProductPhoto.Add(productPhoto[i]);
                photo[productPhoto[i].ProductId - 1].ProductProductPhoto.Add(productPhoto[i]);
                //Add category
                if (i < 5)
                    category[subCategory[i].ProductCategoryId - 1].ProductSubcategory.Add(subCategory[i]);
                subCategory[(int)product[i].ProductSubcategoryId].Product.Add(product[i]);
                //Add model
                model[i].Product.Add(product[i]);
                //Add description
                productDescription[productModelProductDescriptionCulture[i].ProductDescriptionId - 1]
                    .ProductModelProductDescriptionCulture.Add(productModelProductDescriptionCulture[i]);
                model[i].ProductModelProductDescriptionCulture.Add(productModelProductDescriptionCulture[i]);
                //Add transaction history
                product[transactionHistory[i].ProductId - 1].TransactionHistory.Add(transactionHistory[i]);
            }
            db.AddRange(productDescription);
            db.AddRange(productModelProductDescriptionCulture);
            db.AddRange(productPhoto);
            db.AddRange(photo);
            db.AddRange(product);
            db.AddRange(subCategory);
            db.AddRange(category);
            db.AddRange(model);
            db.AddRange(transactionHistory);
            db.SaveChanges();
        }

        private List<dataBikes> ExpectedValueBikes()
        {
            //Initialize
            List<dataBikes> expected = new List<dataBikes>{
                new dataBikes {
                    id = 1,
                    name = "1",
                    price = (decimal)10.1,
                    sell_count = 25,
                    image = new byte[0],
                },
                new dataBikes {
                    id = 2,
                    name = "2",
                    price = (decimal)20.2,
                    sell_count = 0,
                    image = new byte[0],
                },
                new dataBikes {
                    id = 4,
                    name = "4",
                    price = (decimal)40.4,
                    sell_count = 0,
                    image = new byte[0],
                },
                new dataBikes {
                    id = 5,
                    name = "5",
                    price = (decimal)50.5,
                    sell_count = 0,
                    image = new byte[0],
                },
                new dataBikes {
                    id = 6,
                    name = "6",
                    price = (decimal)60.6,
                    sell_count = 0,
                    image = new byte[0],
                },
                new dataBikes {
                    id = 7,
                    name = "7",
                    price = (decimal)70.7,
                    sell_count = 0,
                    image = new byte[0],
                },
                new dataBikes {
                    id = 8,
                    name = "8",
                    price = (decimal)80.8,
                    sell_count = 0,
                    image = new byte[0],
                },
                new dataBikes {
                    id = 10,
                    name = "10",
                    price = (decimal)101,
                    sell_count = 0,
                    image = new byte[0],
                }
            };

            //Return
            return expected;
        }
        private List<dataBikesDetails> ExpectedValueDetails()
        {
            List<dataBikesDetails> expected = new List<dataBikesDetails>() {
                new dataBikesDetails {
                    id = 1,
                    size = "1",
                    description = "Description 1",
                    name = "1",
                    weight = 1,
                    Class = "class 1",
                    style = "style 1",
                    image = new byte[0],
                    safety = 10,
                    color = "1",
                },
                new dataBikesDetails {
                    id = 2,
                    size = "2",
                    description ="Description 1",
                    name = "2",
                    weight = 2,
                    Class = "class 2",
                    style = "style 0",
                    image = new byte[0],
                    safety = 20,
                    color = "2",
                },
                new dataBikesDetails {
                    id = 4,
                    size = "4",
                    description = "Description 2",
                    name = "4",
                    weight = 4,
                    Class = "class 1",
                    style = "style 0",
                    image = new byte[0],
                    safety = 40,
                    color = "4",
                },
                new dataBikesDetails {
                    id = 5,
                    size = "5",
                    description = "Description 1",
                    name = "5",
                    weight = 5,
                    Class = "class 2",
                    style = "style 1",
                    image = new byte[0],
                    safety = 50,
                    color = "5",
                },
                new dataBikesDetails {
                    id = 6,
                    size = "6",
                    description = "Description 3",
                    name = "6",
                    weight = 6,
                    Class = "class 0",
                    style = "style 0",
                    image = new byte[0],
                    safety = 60,
                    color = "6",
                },
                new dataBikesDetails {
                    id = 7,
                    size = "7",
                    description = "Description 1",
                    name = "7",
                    weight = 7,
                    Class = "class 1",
                    style = "style 1",
                    image = new byte[0],
                    safety = 70,
                    color = "7",
                },
                new dataBikesDetails {
                    id = 8,
                    size = "8",
                    description = "Description 4",
                    name = "8",
                    weight = 8,
                    Class = "class 2",
                    style = "style 0",
                    image = new byte[0],
                    safety = 80,
                    color = "8",
                },
                new dataBikesDetails {
                    id = 10,
                    size = "10",
                    description = "Description 5",
                    name = "10",
                    weight = 11,
                    Class = "class 1",
                    style = "style 0",
                    image = new byte[0],
                    safety = 100,
                    color = "10",
                },
            };

            return expected;
        }
    }
}
