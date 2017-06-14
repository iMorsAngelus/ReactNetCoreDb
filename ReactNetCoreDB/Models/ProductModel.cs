using System;
using System.Collections.Generic;

namespace ReactNetCoreDB.Models
{
    public partial class ProductModel
    {
        public ProductModel()
        {
            Product = new HashSet<Product>();
            ProductModelProductDescriptionCulture = new HashSet<ProductModelProductDescriptionCulture>();
        }

        public int ProductModelId { get; set; }

        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture { get; set; }
    }
}
