using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {           
            return View();
        }
    }
}



