using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReactionTest
{
    static class SendToDatabase
    {

        public static async Task sendObject(string userID, DateTime date, int hit, int miss, int testLength, List<double> reactionTimes)
        {
            Post postObj = new Post(userID, date, hit, miss, testLength);

            string url = "https://40.68.124.233:5001/api/data2";
            HttpClient _client = new HttpClient(new System.Net.Http.HttpClientHandler());
            HttpResponseMessage response = null;

            string content = JsonConvert.SerializeObject(postObj);
            try
            {
                response = await _client.PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString()); 
            }
        }

        private static void splitList()
        {

        }

        
    }
}
