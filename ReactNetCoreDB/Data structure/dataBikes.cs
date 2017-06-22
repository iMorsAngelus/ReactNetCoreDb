using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactNetCoreDB.Models;

namespace ReactNetCoreDB.Data_structure
{
    public struct dataBikes
    {
        public int id;
        public string name;
        public decimal price;
        public int sell_count;
        public byte[] image;
    }

    public class dataBikesComparer:IEqualityComparer<dataBikes>
    {
        public bool Equals(dataBikes x, dataBikes y)
        {
            if (
                    x.id == y.id
                    && x.name == y.name
                    && x.price == y.price
                    && x.sell_count == y.sell_count
                    && (x.image.SequenceEqual(y.image) || (x.image.Count() == 0  && y.image.Count() == 0) )
               )
                return true;
            else return false;
        }

        public int GetHashCode(dataBikes obj)
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
