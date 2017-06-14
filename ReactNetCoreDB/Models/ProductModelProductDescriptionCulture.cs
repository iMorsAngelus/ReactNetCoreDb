using System;
using System.Collections.Generic;

namespace ReactNetCoreDB.Models
{
    public partial class ProductModelProductDescriptionCulture
    {
        public int ProductModelId { get; set; }
        public int ProductDescriptionId { get; set; }
        public string CultureId { get; set; }

        public virtual ProductDescription ProductDescription { get; set; }
        public virtual ProductModel ProductModel { get; set; }
    }
}
