using System;
using System.Collections.Generic;
using System.Linq;
using ReactNetCoreDB.Models;
using ReactNetCoreDB.Data_structure;

namespace ReactNetCoreDB.Business_logic
{
    public interface IDataAccessLayer
    {
        IEnumerable<dataBikes> GetAllBikes();
        IEnumerable<dataBikesDetails> GetAllBikesDetails();
        IEnumerable<dataBikes> GetTopBikes();
    }
}
