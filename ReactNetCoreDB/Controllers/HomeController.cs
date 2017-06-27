using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactNetCoreDB.Business_logic;

namespace ReactNetCoreDB.Controllers
{
    public class HomeController : Controller
    {
        IDbQuery query;
        //Конструктор
        public HomeController(IDbQuery query)
        {
            this.query = query;    
        }

        [Route("/BikeDetails/{id}")]
        [HttpGet]
        public async Task<JsonResult> BikeDetails(int id)
        {
            return await Task.Run(() =>
            {
                return new JsonResult(query.BikeDetails(id));
            });
        }

        [Route("/FindBikes/{ind}/{searchString?}")]
        [HttpGet]
        public async Task<JsonResult> FindBikes(int ind, string searchString="")
        {
            return await Task.Run(() =>
            {
                return new JsonResult(query.FindBikes(searchString, ind));
            });
        }

        [Route("/TopBikes")]
        [HttpGet]
        public async Task<JsonResult> TopBikes()
        {
            return await Task.Run(() =>
            {
                return new JsonResult(query.TopBikes());
            });
        }
    }
}
