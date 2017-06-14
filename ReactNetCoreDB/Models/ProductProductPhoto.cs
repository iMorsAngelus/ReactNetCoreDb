using System;
using System.Collections.Generic;

namespace ReactNetCoreDB.Models
{
    public partial class ProductProductPhoto
    {
        public int ProductId { get; set; }
        public int ProductPhotoId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductPhoto ProductPhoto { get; set; }
    }
}
