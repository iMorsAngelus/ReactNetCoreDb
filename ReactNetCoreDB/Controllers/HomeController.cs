using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactNetCoreDB.Business_logic;

namespace ReactNetCoreDB.Controllers
{
    //[ResponseCache(Location = ResponseCacheLocation.Any, Duration = 200)]
    public class HomeController : Controller
    {
        IDbQuery query;
        //Конструктор
        public HomeController(IDbQuery query)
        {
            this.query = query;    
        }

        [HttpGet("/AllBikes")]
        public async Task<JsonResult> AllBikes()
        {
            return await Task.Run(() =>
            {
                var a = query.AllBikes();
                return new JsonResult(a);
            }).ConfigureAwait(false);
        }

        [HttpGet("/AllBikesDetails")]
        public async Task<JsonResult> AllBikesDetails()
        {
            return await Task.Run(() =>
            {
                return new JsonResult(query.AllBikesDetails());
            }).ConfigureAwait(false);
        }

    }
}
