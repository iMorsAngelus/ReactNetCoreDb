using System;
using System.Collections.Generic;
using System.Linq;
using ReactNetCoreDB.Data_structure;
using ReactNetCoreDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ReactNetCoreDB.Business_logic
{
    public class Query : IQuery
    {
        private const int countBikes = 10;
        protected readonly IDataAccessLayer data;

        public Query(IDataAccessLayer data)
        {
            this.data = data;
        }

        public IEnumerable<dataBikesDetails> BikeDetails(int id)
        {
            return data.GetAllBikesDetails().Where(bike => bike.id.Equals(id));
        }

        public IEnumerable<dataBikes> FindBikes(string searchString, int ind)
        {
            //Select only text data
            var FindBikes = Find(searchString).ToList();

            if (ind < FindBikes.Count - 1)
            {
                IEnumerable<dataBikes> result = FindBikes.GetRange(ind, (FindBikes.Count - ind - countBikes> 0) ? countBikes : FindBikes.Count - ind);
                return result;
            }

            return null;
        }

        public IEnumerable<dataBikes> TopBikes()
        {
            return data.GetTopBikes();
        }

        private IEnumerable<dataBikes> Find(string searchString)
        {
            if (searchString.Length > 0)
            {
                var result = data.GetAllBikes();
                return result.Where(bike => bike.name.ToLower().Contains(searchString));
            }
            return TopBikes();
        }

    }
}
