using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Web_Service_Monitor_Tile.Logic
{
    public class Webservices
    {
        async public static Task<JsonObject> Get(string path)
        {
            var client = new HttpClient();
            client.MaxResponseContentBufferSize = 1024 * 1024; // Read up to 1 MB of data
            var response = await client.GetAsync(new Uri(path));
            var result = await response.Content.ReadAsStringAsync();

            var webServiceResults = JsonObject.Parse(result);


            return webServiceResults;

        }
    }
}
