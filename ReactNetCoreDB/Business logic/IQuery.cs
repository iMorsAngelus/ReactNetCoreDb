using System.Collections.Generic;
using ReactNetCoreDB.Data_structure;
using System.Threading.Tasks;

namespace ReactNetCoreDB.Business_logic
{
    public interface IQuery
    {
        IEnumerable<dataBikesDetails> BikeDetails(int id);
        IEnumerable<dataBikes> FindBikes(string searchString, int ind);
        IEnumerable<dataBikes> TopBikes();
    }
}
