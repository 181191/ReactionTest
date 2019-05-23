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
using System.IO;
using System.Reflection;
using PCLStorage;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace ReactionTest
{
    public partial class MainPage : ContentPage
    {
        char divideSign = '\t';
        private bool testStarted;
        private bool buttonActive;
        private int testTimeSec = 6;
        int duration = 0;
        int testNumber = 0;
        private List<int> randoms;
        private int minutes;
        private string userID;
        private List<double> clicks = new List<double>(75);

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
            InitializeComponent();
            this.minutes = minutes;
            this.userID = userID;

            OnChangeValue("Grey = Wait \nRed = Press");
            OnChangeButton("Press To Start");
            OnColorChangeButton(Color.Red);

            if (minutes > 0)
                testTimeSec *= minutes;

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;


            
            
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
                ButtonPress("Miss");
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

            if (timeSinceActive <= 200)
            {
                ButtonPress("Miss");
            }
            else if (timeSinceActive <= 800)
            {
                ButtonPress("Hit");
                clicks.Add(timeSinceActive);
            }
            else
            {
                ButtonPress("Miss");
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
                ActivateButton();
            }

            else if (pressedWhen + 2 == duration)
            {
                OnChangeValue("");
            }
        }

        public async void TestFinished()
        {
            //TODO: DATA MANAGEMENT TYP ASYNC METODE
            Console.WriteLine(clicks);
            Console.WriteLine();

            await SaveCourse();
            await ReadCourse();
            await SendToDatabase.sendObject(userID, DateTime.Now, hit, miss, minutes, clicks);

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

        public void ButtonPress(string val)
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

        public void ActivateButton()
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

        protected override void OnDisappearing()
        {
            testTimer.Stop();
        }

        private async Task SaveCourse()
        {
            string dataText;
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("Documents",
                CreationCollisionOption.OpenIfExists);
            IFile data;
            try
            {
                data = await folder.GetFileAsync("Data.csv");
                //data = await folder.CreateFileAsync("Data.csv", CreationCollisionOption.ReplaceExisting);
                dataText = writeData();
            }
            catch (PCLStorage.Exceptions.FileNotFoundException)
            {
                data = await folder.CreateFileAsync("Data.csv", 
                    CreationCollisionOption.ReplaceExisting);
                dataText = $"UserID{divideSign}Date{divideSign}Timestamp{divideSign}Hits{divideSign}Misses{divideSign}Test_length\n" + writeData();

            }
            

            var bufferArray = Encoding.UTF8.GetBytes(dataText);
            using (Stream streamToWrite = await data.OpenAsync(PCLStorage.FileAccess.ReadAndWrite).ConfigureAwait(false))
            {
                streamToWrite.Position = streamToWrite.Length;
                if (streamToWrite.CanWrite)
                {
                    await streamToWrite.WriteAsync(bufferArray, 0, bufferArray.Length).ConfigureAwait(false);
                }
            }
        }

        private async Task ReadCourse()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.GetFolderAsync("Documents");
            IFile data = await folder.GetFileAsync("Data.csv");
            Console.WriteLine(await data.ReadAllTextAsync());
        }

        private string writeData()
        {
            string data = userID + divideSign + 
                System.DateTime.Now.Date.ToString("dd/MM/yyyy") + divideSign +
                System.DateTime.Now.ToString("HH:mm:ss") + divideSign + 
                hit + divideSign + 
                miss + divideSign + 
                minutes + divideSign;

            foreach (double s in clicks)
            {
                data += (s.ToString() + divideSign);
            }
            return data + "\n";
        }
    }
}

