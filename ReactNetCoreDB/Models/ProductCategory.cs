using System;
using System.Collections.Generic;

namespace ReactNetCoreDB.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            ProductSubcategory = new HashSet<ProductSubcategory>();
        }

        public int ProductCategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductSubcategory> ProductSubcategory { get; set; }
    }
}
