using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientSide_DanceFellows.Models
{
    public class TestAPI
    {
        public string Name { get; set; }

        TestAPI()
        {
            GetResponse();
        }

        static HttpClient client = new HttpClient();



        public static async Task<TestAPI> GetResponse()
        {
            TestAPI test = new TestAPI();

            string path = "http://localhost:56370/api/Competitors";

            HttpResponseMessage responseMessage = await client.GetAsync(path);
            if (responseMessage.IsSuccessStatusCode)
            {
                test.Name = await responseMessage.Content.ReadAsAsync<string>();
            }
            return test;
        }


    }
}
