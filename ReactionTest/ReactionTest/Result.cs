using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

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

        public Result(string person_id, SqlDateTime date, DateTime timeStamp, int hits, int misses, int test_length,
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
    }
}
