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
        static HttpClient client = new HttpClient();
        string path = "https://apidancefellows20190204115607.azurewebsites.net/api/Competitors";

        public async Task<string> GetStringAsync(string path)
        {
            string fromAPI = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                fromAPI = await response.Content.ReadAsStringAsync();
            }
            
            return fromAPI;
        }

        public async Task<IActionResult> Index()
        {
            string response = await GetStringAsync(path);
            ViewBag.HelloWorld = response;
            return View();
        }
    }
}



