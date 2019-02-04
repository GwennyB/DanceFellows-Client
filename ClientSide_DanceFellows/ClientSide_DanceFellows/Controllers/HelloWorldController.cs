
using ClientSide_DanceFellows.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Controllers
{
    public class HelloWorldController : Controller
    {

        static HttpClient client = new HttpClient();
        string path = "http://localhost:56370/api/Competitors";

        public static async Task<string> GetStringAsync(string path)
        {
            string fromAPI = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                fromAPI = await response.Content.ReadAsAsync<string>();
            }
            Console.WriteLine(fromAPI);
            return fromAPI;
        }

    }
}
