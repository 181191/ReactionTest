using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace ReactionTest
{
    internal class Post : INotifyPropertyChanged
    {
        public string UserID { get; set; }
        public DateTime date { get; set; }
        public int hit { get; set; }
        public int miss { get; set; }
        public int testLenght { get; set; }

        public Post(string userID, DateTime date, int hit, int miss, int testLength)
        {
            this.UserID = userID;
            this.date = date;
            this.hit = hit;
            this.miss = miss;
            this.testLenght = testLenght;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }


}
