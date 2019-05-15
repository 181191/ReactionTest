using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ReactionTest
{
    class Result
    {
        private string person_id;
        private SqlDateTime date;
        private DateTime timeStamp;
        private int hits;
        private int misses;
        private int test_length;
        private List<double> reactionTimes;

        public Result()
        {

        }

        public Result(string person_id, int hits, int misses, int test_length,
            List<double> reactionTimes)
        {
            this.person_id = person_id;
            this.date = date; 
            this.timeStamp = timeStamp;
            this.hits = hits;
            this.misses = misses;
            this.test_length = test_length;
            this.reactionTimes = reactionTimes; 


        }

        public string getPerson_id()
        {
            return person_id; 
        }
        public int getHits()
        {
            return hits;
        }
        public int getMisses()
        {
            return misses;
        }
        public int getTestLength()
        {
            return test_length;
        }

        public List<double> GetDoubles()
        {
            return reactionTimes; 
        }
       
        // HttpClient client,
        public async void toDatabase()
        {
            string sContentType = "application/json";
            JArray arr = new JArray();

            var person = getPerson_id();
            var hits = getHits();
            var misses = getMisses();
            var test_length = getTestLength();
            var currentDate = DateTime.Now.ToShortDateString();
            var currentTime = DateTime.Now.ToShortTimeString(); 

            Object json = new JObject()
            {
                {"User-Id", person},
                {"Hits", hits},
                {"Misses", misses },
                {"Test Length", test_length},
                {"Date", currentDate },
                {"Timestamp", currentTime }
            };

            arr.Add(json);
            string url = "http://localhost:50362/api/Values/";
            HttpClient oHttpClient = new HttpClient();
            var oTaskPostAsync = oHttpClient.PutAsync(url, new StringContent(arr.ToString(), Encoding.UTF8, sContentType));
//            oTaskPostAsync.ContinueWith((oHttpResponseMessage) =>
//            {
//                // response of post here
//            };
        }
    }
}
