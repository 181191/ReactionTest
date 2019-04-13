using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Timer = System.Timers.Timer;

namespace ReactionTest
{
    public partial class MainPage : ContentPage
    {

        private bool buttonActive;
        private bool testStarted;
        private int testTimeSec = 60;
        int duration = 0;
        int testNumber = 0;
        private List<int> randoms;
        private int minutes;
        public string s { get { return s; } set {
                s = value;
                Even
            } }
        public Color bColor { get; set;/* { bColor = value; PropertyChanged(this, new PropertyChangedEventArgs("bColor")); } */} = Color.Red;

        private string testID;
        Timer testTimer = new Timer();

        private double timeSinceActive;
        private int anticipationMiss;
        private int minorLaps;
        private int majorLaps;
        private int miss;
        private int hit;

        private DateTime created;
        private DateTime pressed; 

        public MainPage()
        {

        }
        public MainPage(int minutes, string testID)
        {
            this.testID = testID;
            InitializeComponent();
            BindingContext = this;

            if(minutes > 0)
                testTimeSec *=  minutes;
            
        }

        void OnButtonClicked(object sender, EventArgs args)
        {
            pressed = DateTime.Now;

            if (testStarted)
                TestClick();
            else
                InitTest();

            
        }

        private void TestClick()
        {
            timeSinceActive = (pressed.Subtract(created).TotalMilliseconds);
            Console.WriteLine("----------------  "+ timeSinceActive + " -------------");
            if (timeSinceActive <= 100)
            {
                infoString.Text = "Too early";
                anticipationMiss++; 
            }
            else if (timeSinceActive <= 500)
            {
                hit++;
                infoString.Text = "Hit"; 
            }
            else if(timeSinceActive <= 1000)
            {
                infoString.Text = "Hit"; 
                minorLaps++; 
            }
            else if (timeSinceActive <= 3000)
            {
                infoString.Text = "Hit";
                majorLaps++; 
            }
            else
            {
                infoString.Text = "Miss";
                miss++; 
            }

        }

        public void InitTest()
        {
            testStarted = true;
            rButton.BackgroundColor = Color.Red;
            infoString.Text = "Started";
            randoms = RandomizeIntervals();

            testTimer.Interval = 1000;
            testTimer.Start();
            testTimer.Elapsed += TimerElapsedEvent; 
        }

        public void TimerElapsedEvent(Object sender, ElapsedEventArgs a)
        {
            duration++;
            if (duration >= testTimeSec)
            {
                testTimer.Stop(); 
            }
            if (duration == randoms[testNumber] && duration <= testTimeSec)
            {
                infoString.Text = "Press";
                rButton.BackgroundColor = Color.Green;

                created = DateTime.Now;
            }
            else if (duration == randoms[testNumber] + 7)
            {
                infoString.Text = " ";
            }

        }

        private List<int> RandomizeIntervals()
        {
            List<int> list = new List<int>();
            Random generator = new Random();
            for (int i = 0; i < testTimeSec / 10; i++)
            {
                list.Add(i*10 + generator.Next(1, 10)); 
            }

            return list; 
        }
    }
}

