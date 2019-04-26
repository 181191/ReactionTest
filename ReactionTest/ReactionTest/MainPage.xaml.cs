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
        private bool testStarted;
        private int testTimeSec = 60;
        int duration = 0;
        int testNumber = 0;
        private List<int> randoms;
        private int minutes;
        private string userID;
        

        Timer testTimer = new Timer();

        private int pressedWhen;
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
            OnChangeValue("Gray = Wait \nRed = Press");
            OnChangeButton("Press To Start");

            if (minutes > 0)
                testTimeSec *= minutes;

            this.userID = userID;

        }

        void OnButtonClicked(object sender, EventArgs args)
        {


            pressed = DateTime.Now;
            pressedWhen = duration;

            if (testStarted)
            {
                TestClick();
            }
            else
            {
                OnChangeButton("");
                OnChangeValue("");
                OnColorChangeButton(Color.LightGray);
                InitTest();
            }
        }

        private void TestClick()
        {
            timeSinceActive = (pressed.Subtract(created).TotalMilliseconds);
            Console.WriteLine(timeSinceActive + " ");
            OnColorChangeButton(Color.LightGray);
            OnChangeButton("...");

            if (timeSinceActive <= 100)
            {
                OnChangeValue("Miss");
                anticipationMiss++;
            }
            else if (timeSinceActive <= 500)
            {
                hit++;
                OnChangeValue("Hit");

            }
            else if (timeSinceActive <= 1000)
            {
                OnChangeValue("Hit");
                minorLaps++;
            }
            else if (timeSinceActive <= 3000)
            {
                OnChangeValue("Hit");
                majorLaps++;
            }
            else
            {
                OnChangeValue("Miss");
                miss++;
            }

        }

        public void InitTest()
        {
            OnChangeButton("...");
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
                TestFinished();
            }

            if (duration == randoms[testNumber])
            {
                testNumber++;
                OnChangeValue("");
                OnColorChangeButton(Color.Red);
                OnChangeButton("");
                created = DateTime.Now;
            }

            else if (pressedWhen + 2 == duration)
            {
                OnChangeValue("");
            }
        }

        public void TestFinished()
        {
            //TODO: DATA MANAGEMENT TYP ASYNC METODE

            Device.BeginInvokeOnMainThread(() =>
            {

                DisplayAlert("Test finished", "Thank you for participating", "Finish");
                Navigation.PopToRootAsync();

            });
        }

        private List<int> RandomizeIntervals()
        {
            List<int> list = new List<int>();
            Random generator = new Random();
            for (int i = 0; i < testTimeSec / 8; i++)
            {
                list.Add(i * 10 + generator.Next(1, 10));
                if (i > 0)
                {
                    if (list[i] - list[i - 1] < 5)
                    {
                        list[i] += (5 - (list[i] - list[i - 1]));
                    }
                    if (list[i] - list[i - 1] > 10)
                    {
                        list[i] -= generator.Next(list[i] - list[i - 1] - 10, list[i] - list[i - 1] - 10 + 3);
                    }
                }

            }

            return list;
        }


        public void OnChangeValue(string NewText)
        {
            Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {

                    this.Resources["changeString"] = NewText;


                });
            });
        }

        public void OnChangeButton(string NewText)
        {
            Task.Run(() =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {

                    this.Resources["changeButton"] = NewText;

                });
            });
        }

        public void OnColorChangeButton(Color color)
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

