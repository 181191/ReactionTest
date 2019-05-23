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
            if(reactionTimes.Count < 75)
            {
                for (int i = reactionTimes.Count-1; i < 75; i++) 
                {
                    reactionTimes.Add(0.0);
                }
            }

            Post postObj = new Post(userID, date, hit, miss, testLength, reactionTimes[0], reactionTimes[1], reactionTimes[2], reactionTimes[3], reactionTimes[4], reactionTimes[5], reactionTimes[6], reactionTimes[7], reactionTimes[8], reactionTimes[9], reactionTimes[10], reactionTimes[11], reactionTimes[12], reactionTimes[13], reactionTimes[14], reactionTimes[15], reactionTimes[16], reactionTimes[17], reactionTimes[18], reactionTimes[19], reactionTimes[20], reactionTimes[21], reactionTimes[22], reactionTimes[23], reactionTimes[24], reactionTimes[25], reactionTimes[26], reactionTimes[27], reactionTimes[28], reactionTimes[29], reactionTimes[30], reactionTimes[31], reactionTimes[32], reactionTimes[33], reactionTimes[34], reactionTimes[35], reactionTimes[36], reactionTimes[37], reactionTimes[38], reactionTimes[39], reactionTimes[40], reactionTimes[41], reactionTimes[42], reactionTimes[43], reactionTimes[44], reactionTimes[45], reactionTimes[46], reactionTimes[47], reactionTimes[48], reactionTimes[49], reactionTimes[50], reactionTimes[51], reactionTimes[52], reactionTimes[53], reactionTimes[54], reactionTimes[55], reactionTimes[56], reactionTimes[57], reactionTimes[58], reactionTimes[59], reactionTimes[60], reactionTimes[61], reactionTimes[62], reactionTimes[63], reactionTimes[64], reactionTimes[65], reactionTimes[66], reactionTimes[67], reactionTimes[68], reactionTimes[69], reactionTimes[70], reactionTimes[71], reactionTimes[72], reactionTimes[73], reactionTimes[74]);

            string url = "https://40.68.124.233:5001/api/data";
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
