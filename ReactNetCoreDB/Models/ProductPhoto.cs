using System;
using System.Collections.Generic;

namespace ReactNetCoreDB.Models
{
    public partial class ProductPhoto
    {
        public ProductPhoto()
        {
            ProductProductPhoto = new HashSet<ProductProductPhoto>();
        }

        public int ProductPhotoId { get; set; }
        public byte[] LargePhoto { get; set; }

        public virtual ICollection<ProductProductPhoto> ProductProductPhoto { get; set; }
    }
}
