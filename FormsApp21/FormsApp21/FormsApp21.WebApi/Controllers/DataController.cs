using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FormsApp21.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        // GET api/data
        [HttpGet]
        public string Get()
        {
            return "Protected data";
        }
    }
}
