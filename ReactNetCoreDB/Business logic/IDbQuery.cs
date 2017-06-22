using System.Collections.Generic;
using ReactNetCoreDB.Data_structure;
using System.Threading.Tasks;

namespace ReactNetCoreDB.Business_logic
{
    public interface IDbQuery
    {
        IEnumerable<dataBikes> AllBikes();
        IEnumerable<dataBikesDetails> AllBikesDetails();
    }
}
