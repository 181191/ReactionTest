using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
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
        private string userID;


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
        public MainPage(int minutes, string userID)
        {
           
            this.minutes = minutes; 
            InitializeComponent();
            onChangeValue("Lets play");
            onChangeButton("Press To Start");

            if (minutes > 0)
                testTimeSec *=  minutes;

            this.userID = userID;

        }

        void OnButtonClicked(object sender, EventArgs args)
        {
            pressed = DateTime.Now;

            if (testStarted)
            {
                TestClick();
            }
            else
            {
                onChangeButton("Wait For it");
                onChangeValue(" ");
                onColorChangeButton(Color.LightGray);
                InitTest();
            }
        }

        private void TestClick()
        {
            timeSinceActive = (pressed.Subtract(created).TotalMilliseconds);
            Console.WriteLine(timeSinceActive + " ");
            onColorChangeButton(Color.LightGray);

            if (timeSinceActive <= 100)
            { 
                onChangeValue("Too early");
                anticipationMiss++; 
            }
            else if (timeSinceActive <= 500)
            {
                hit++;
                onChangeValue("Hit");
                
            }
            else if(timeSinceActive <= 1000)
            {
                onChangeValue("Hit");
                minorLaps++; 
            }
            else if (timeSinceActive <= 3000)
            {
                onChangeValue("Hit");
                majorLaps++; 
            }
            else
            {
                onChangeValue("Miss");
                miss++; 
            }

        }

        public void InitTest()
        {

            testStarted = true;

            randoms = RandomizeIntervals();

            testTimer.Interval = 1000;
            testTimer.Start();
            testTimer.Elapsed += TimerElapsedEvent;

        }



        public void TimerElapsedEvent(Object sender, ElapsedEventArgs a)
        {
            duration++;
            Console.WriteLine(duration);
            if (duration >= testTimeSec)
            {
                testTimer.Stop(); 
            }
            if (duration == randoms[testNumber])
            {
                onChangeValue("Press Now");
                onColorChangeButton(Color.Red);
                created = DateTime.Now;

            }
            else if (duration == randoms[testNumber] + 4)
            {
                onChangeValue("  ");
                testNumber++; 
            }

        }

        private List<int> RandomizeIntervals()
        {
            List<int> list = new List<int>();
            Random generator = new Random();
            for (int i = 0; i < testTimeSec / 10; i++)
            {
                list.Add(i*10 + generator.Next(1, 10)); 
                if(i > 0)
                {
                    if (list[i] - list[i - 1] < 6)
                    {
                        list[i] += (6 - (list[i] - list[i - 1]));
                    }
                }
                
            }

            return list; 
        }


        public void onChangeValue(string NewText)
        {
            Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {

                    this.Resources["changeString"] = NewText;


                });
            });
        }

        public void onChangeButton(string NewText)
        {
            Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {

                    this.Resources["changeButton"] = NewText;


                });
            });
        }

        public void onColorChangeButton(Color color)
        {
            Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {

                    this.Resources["buttonColor"] = color;


                });
            });
        }

    }

}

