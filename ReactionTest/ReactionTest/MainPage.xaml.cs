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
        private bool buttonActive;
        private int testTimeSec = 6;
        int duration = 0;
        int testNumber = 0;
        private List<int> randoms;
        private int minutes;
        private string userID;
        private List<double> clicks = new List<double>();

        Timer testTimer = new Timer();

        private int pressedWhen; 
        private double timeSinceActive;
        private int activeTime = 0;

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
                testTimeSec *=  minutes;

            this.userID = userID;

        }

        void OnButtonClicked(object sender, EventArgs args)
        {
           

            pressed = DateTime.Now;
            pressedWhen = duration; 

            if (testStarted && buttonActive)
            {
                TestClick();
            }
            else if (testStarted && !buttonActive)
            {
                buttonPress("Miss");
            }
            else
            {
                OnChangeButton("");
                OnChangeValue("");
                OnColorChangeButton(Color.LightGray);
                InitTest();
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

        private void TestClick()
        {
            timeSinceActive = (pressed.Subtract(created).TotalMilliseconds);
            Console.WriteLine(timeSinceActive + " ");
            
            buttonActive = false;

            if (timeSinceActive <= 100)
            {
                buttonPress("Miss");
            }
            else if (timeSinceActive <= 2000)
            {
                buttonPress("Hit");
                clicks.Add(timeSinceActive);
            }
            else
            {
                buttonPress("Miss");
            }

        }

        public void TimerElapsedEvent(Object sender, ElapsedEventArgs a)
        {
            duration++;
            if (duration >= testTimeSec)
            {
                testTimer.Stop();
                TestFinished(); 
            }

            if (buttonActive)
            {
                activeTime++;
            }
            if (activeTime == 3)
            {
                DeactivateButton();
            }
            if (duration == randoms[testNumber])
            {
                activateButton();
            }
            
            else if (pressedWhen + 2 == duration)
            {
                OnChangeValue("");
            }
        }

        public void TestFinished()
        {
            //TODO: DATA MANAGEMENT TYP ASYNC METODE
            Console.WriteLine("");
            foreach(double d in clicks)
            {
                Console.Write(d + " ");
            }
            Console.WriteLine("");
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
                list.Add(i*10 + generator.Next(1, 10)); 
                if(i > 0)
                {
                    if (list[i] - list[i - 1] < 5)
                    {
                        list[i] += (5 - (list[i] - list[i - 1]));
                    }
                    if(list[i] - list[i-1] > 10)
                    {
                        list[i] -=  generator.Next(list[i] - list[i - 1] - 10, list[i] - list[i - 1] - 10 + 3); 
                    }
                }
                
            }

            return list; 
        }

        public void buttonPress(string val)
        {
            OnChangeButton("...");
            OnChangeValue(val);
            OnColorChangeButton(Color.LightGray);
            if (val.Equals("Hit"))
            {
                hit++;
            }
            else if (val.Equals("Miss"))
            {
                miss++;
            }
        }

        public void activateButton()
        {
            buttonActive = true;
            testNumber++;
            OnChangeValue("");
            OnColorChangeButton(Color.Red);
            OnChangeButton("");
            created = DateTime.Now;
        }

        public void DeactivateButton()
        {
            pressedWhen = duration;
            buttonActive = false;
            activeTime = 0;
            OnChangeButton("...");
            OnChangeValue("Miss");
            OnColorChangeButton(Color.LightGray);
            miss++;
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

