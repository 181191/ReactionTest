using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Timer = System.Timers.Timer;

namespace ReactionTest
{
    public partial class MainPage : ContentPage
    {
        private bool buttonActive;
        private bool testStarted;
        private int testTimeSec = 60; 

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
            InitializeComponent();


        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            if (!testStarted)
            {
                InitTest();
            }
            else
            {
                TestClick();
            }


        }

        private void TestClick()
        {
            pressed = DateTime.Now;
            timeSinceActive = (pressed.Subtract(created).TotalMilliseconds);
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
                minorLaps++; 
            }
            else if (timeSinceActive <= 3000)
            {
                majorLaps++; 
            }

        }

        public void InitTest()
        {
            testStarted = true; 
            int duration = 0;
            int testNumber = 0;
            int closestTen = 0;
            List<int> randoms = RandomizeIntervals(); 
           
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                
                Console.WriteLine(duration);

                if ( duration == closestTen + randoms[testNumber] && duration <= testTimeSec)
                {
                    infoString.Text = "Press Now";
                    created = DateTime.Now;
                    
                }
                if (duration >= testTimeSec) 
                    return false; 
                
                duration++;
                if (duration % 10 == 0)
                {
                    testNumber++;
                    closestTen = duration;
                }
                return true; // True = Repeat again, False = Stop the timer
            });
        }

        private List<int> RandomizeIntervals()
        {
            List<int> list = new List<int>();
            Random generator = new Random();
            for (int i = 0; i < testTimeSec / 10; i++)
            {
                list.Add(10 * generator.Next(1, 10)); 
            }

            return list; 
        }
    }
}

