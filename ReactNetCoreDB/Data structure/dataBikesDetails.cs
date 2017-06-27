using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactNetCoreDB.Data_structure
{
    public struct dataBikesDetails
    {
        public int id;
        public string description;
        public string name;
        public decimal? weight;
        public string Class;
        public string style;
        public byte[] image;
        public string color;
        public string size;
        public short safety;
    }

    public class dataBikesDetailsComparer : IEqualityComparer<dataBikesDetails>
    {
        public bool Equals(dataBikesDetails x, dataBikesDetails y)
        {
            if (
                    x.id == y.id
                    && x.Class == y.Class
                    && x.style == y.style
                    && x.color == y.color
                    && x.weight == y.weight
                    && x.size == y.size
                    && x.safety == y.safety
                    && (x.image.SequenceEqual(y.image) || (x.image.Count() == 0 && y.image.Count() == 0))
                    && x.description == y.description
               )
                return true;
            return false;
        }

        public int GetHashCode(dataBikesDetails obj)
        {
            //Get hash code for the Name field if it is not null. 
            int hashProductName = obj.name == null ? 0 : obj.name.GetHashCode();

            //Get hash code for the Code field. 
            int hashProductCode = obj.id.GetHashCode();

            //Calculate the hash code for the product. 
            return hashProductName ^ hashProductCode;
        }
    }
}
