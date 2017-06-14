using System;
using System.Collections.Generic;

namespace ReactNetCoreDB.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductProductPhoto = new HashSet<ProductProductPhoto>();
            TransactionHistory = new HashSet<TransactionHistory>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Color { get; set; }
        public short SafetyStockLevel { get; set; }
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
        public decimal? Weight { get; set; }
        public string Class { get; set; }
        public string Style { get; set; }
        public int? ProductSubcategoryId { get; set; }
        public int? ProductModelId { get; set; }

        public virtual ICollection<ProductProductPhoto> ProductProductPhoto { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistory { get; set; }
        public virtual ProductModel ProductModel { get; set; }
        public virtual ProductSubcategory ProductSubcategory { get; set; }
    }
}
