﻿using System;
using System.Collections.Generic;

namespace ReactNetCoreDB.Models
{
    public partial class ProductSubcategory
    {
        public ProductSubcategory()
        {
            Product = new HashSet<Product>();
        }

        public int ProductSubcategoryId { get; set; }
        public int ProductCategoryId { get; set; }

        public virtual ICollection<Product> Product { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
