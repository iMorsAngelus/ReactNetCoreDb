using System;
using System.Collections.Generic;
using System.Linq;
using ReactNetCoreDB.Data_structure;
using ReactNetCoreDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ReactNetCoreDB.Business_logic
{
    public class DbQuery : IDbQuery
    {
        protected readonly IDataAccessLayer data;
        protected Stack<IEnumerable<dataBikes>> Searching;

        public DbQuery(IDataAccessLayer data)
        {
            this.data = data;
            Searching = new Stack<IEnumerable<dataBikes>>();
            Searching.Push(data.GetAllBikes());
        }

        public IEnumerable<dataBikesDetails> BikeDetails(int id)
        {
            return data.GetAllBikesDetails().Where(bike => bike.id.Equals(id));
        }

        public IEnumerable<dataBikes> FindBikes(string searchString, int ind)
        {
            var FindBikes = Find(searchString).ToList();
            if (ind < FindBikes.Count - 1)
            {
                IEnumerable<dataBikes> result = FindBikes.GetRange(ind, (FindBikes.Count - ind - 10> 0) ? 10 : FindBikes.Count - ind);
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
                if (searchString.Length + 1 > Searching.Count)
                {
                    Searching.Push(Searching
                                        .Peek()
                                        .Where(bike => bike.name.ToLower().Contains(searchString))
                                  );
                }
                else if (searchString.Length + 1 < Searching.Count)
                {
                    Searching.Pop();
                }
                return Searching.Peek();
            }
            Searching.Pop();
            return TopBikes();
        }

    }
}
