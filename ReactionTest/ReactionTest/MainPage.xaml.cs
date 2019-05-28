using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Timer = System.Timers.Timer;
using System.IO;
using PCLStorage;
using System.Net;

namespace ReactionTest
{
    public partial class MainPage : ContentPage
    {
        char divideSign = '\t';
        private bool testStarted;
        private bool buttonActive;
        private int testTimeSec = 60;
        int duration = 0;
        int testNumber = 0;
        private List<int> randoms;
        private int minutes;
        private string userID;
        private List<int> clicks = new List<int>(100);

        Timer testTimer = new Timer();
        Stopwatch stopwatch = new Stopwatch();
        private int pressedWhen;
        private int timeSinceActive;
        private int activeTime = 0;

        private int miss;
        private int hit;




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

            TestIntervalGenerator testInterval = new TestIntervalGenerator();

            randoms = testInterval.RandomizeIntervals(testTimeSec);

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;




        }

        void OnButtonClicked(object sender, EventArgs args)
        {
            stopwatch.Stop();
            timeSinceActive = (int)stopwatch.ElapsedMilliseconds;
            pressedWhen = duration;

            if (testStarted && buttonActive)
            {
            Console.WriteLine(timeSinceActive);
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
            stopwatch.Reset();
        }

        public void InitTest()
        {
            OnChangeButton("...");
            testStarted = true;


            testTimer.Interval = 1000;
            testTimer.Start();
            testTimer.Elapsed += TimerElapsedEvent;

        }

        private void TestClick()
        {
            buttonActive = false;

            if (timeSinceActive <= 200)
            {
                ButtonPress("Miss");
            }
            else if (timeSinceActive <= 500)
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
            if (activeTime == 1)
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

            await SaveCourse();
            await ReadCourse();
            SendToDatabase.sendObject(userID, DateTime.Now, hit, miss, minutes, clicks);

            Device.BeginInvokeOnMainThread(() =>
            {

                DisplayAlert("Test finished", "Thank you for participating", "Finish");
                Navigation.PopToRootAsync();

            });
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
            stopwatch.Start();
        }

        public void DeactivateButton()
        {
            stopwatch.Reset();
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
            //Task.Run(() =>
            //{
                Device.BeginInvokeOnMainThread(() =>
                {

                    this.Resources["changeString"] = NewText;


                });
            //}).ConfigureAwait(false);
        }

        public void OnChangeButton(string NewText)
        {
            //Task.Run(() =>
            //{
                Device.BeginInvokeOnMainThread(() =>
                {

                    this.Resources["changeButton"] = NewText;


                });
            //}).ConfigureAwait(false);
        }

        public void OnColorChangeButton(Color color)
        {
            //Task.Run(() =>
            //{
                Device.BeginInvokeOnMainThread(() =>
                {

                    this.Resources["buttonColor"] = color;


                });
            //}).ConfigureAwait(false);
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

            foreach (int s in clicks)
            {
                data += (s.ToString() + divideSign);
            }
            return data + "\n";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (testStarted)
            {
                DisplayAlert("Test was depricated", "the application was not in focus, therefor the test has been canceled", "OK");
                Device.BeginInvokeOnMainThread(() => Navigation.PopAsync());
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            testTimer.Stop();
            stopwatch.Stop();
        }
    }
}
