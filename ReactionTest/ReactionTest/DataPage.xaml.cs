using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PCLStorage;
using System.IO;
using Xamarin.Essentials;
using Acr.UserDialogs;

namespace ReactionTest
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DataPage : ContentPage
	{
        char divideSign = '\t'; 

		public DataPage ()
		{
		}
        public DataPage(string password)
        {
            if(password == "1234")
            {
                InitializeComponent();
            }
            else
            {
                Navigation.PopAsync();
            }
        }



        private async void Export_Clicked(object sender, EventArgs e)
        {
            DateTime fromDatePicked = fromDate.Date;
            DateTime toDatePicked = toDate.Date;

            //Whole dataset as string format:
            string[][] dataSet = await ReadData("Data.csv");

            //Find data with wanted sets:
            string[][] wantedDates = checkDates(dataSet, fromDatePicked, toDatePicked);
            string textToWrite = convertToString(wantedDates);
            byte[] bytes = Encoding.ASCII.GetBytes(textToWrite);
            DependencyService.Get<ISendMail>().sendMail(bytes, "text/csv", textToWrite);

        }

        
        private async void SendAsMail_Clicked(object sender, EventArgs e)
        {
            DateTime fromDatePicked = fromDate.Date;
            DateTime toDatePicked = toDate.Date;

            //Whole dataset as string format:
            string[][] dataSet = await ReadData("Data.csv");

            //Find data with wanted sets:
            string[][] wantedDates = checkDates(dataSet, fromDatePicked, toDatePicked);
            string textToWrite = convertToString(wantedDates);
            await WriteFile(textToWrite);



            //IFile DatafileBetweenDates = null;

            //try
            //{
            //    IFolder rootFolder = PCLStorage.FileSystem.Current.LocalStorage;
            //    IFolder folder = await rootFolder.GetFolderAsync("ReactionTest");
            //    DatafileBetweenDates = await folder.GetFileAsync("DataBetweenDates.csv");
            //}
            //catch (PCLStorage.Exceptions.FileNotFoundException) { }
            //string pathplease = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DataBetweenDates.csv");

            //ExperimentalFeatures.Enable("EmailAttachments_Experimental");
            //List<EmailAttachment> eList = new List<EmailAttachment>() {  new EmailAttachment(pathplease) };

            //List<string> toList = new List<string>();
            
            //PromptConfig pc = new PromptConfig { InputType = InputType.Email, Title = "Recipient: ", IsCancellable = true};

            //PromptResult pResult = await UserDialogs.Instance.PromptAsync(pc);


            //if (pResult.Ok)
            //{
            //    toList.Add(pResult.Text);
            //    await SendEmail("Data From Test", $"Sendt testdata from {fromDatePicked.Date} to {toDatePicked.Date}", toList, eList);
            //}
            //else if (pResult.Ok && string.IsNullOrWhiteSpace(pResult.Text))
            //{
            //    UserDialogs.Instance.Alert("Email can't be blanc");
            //}

        }

 

        private string convertToString(string[][] data)
        {
            string final = $"UserID{divideSign}Date{divideSign}Timestamp{divideSign}Hits{divideSign}Misses{divideSign}Test_length\n";

            foreach(string[] line in data)
            {
                foreach(string value in line)
                {
                    final += value;
                    final += divideSign;

                }
                final += "\n";
            }
            return final;
        }



        //Method to check dates, also removes null rows from array
        private string[][] checkDates(string[][] dataSet, DateTime fromDatePicked, DateTime toDatePicked)
        {


            string[][] matchingSets = new string[dataSet.Length][];
            int counter = 0;

            for ( int i = 0; i < dataSet.Length; i++)
            {
                DateTime dt = DateTime.Now;
                bool dateFound = true;
                try
                {
                    dt = Convert.ToDateTime(dataSet[i][1]);
                }
                catch (Exception) { dateFound = false; }

                if (dateFound)
                {
                    if (dt >= fromDatePicked && dt <= toDatePicked)
                        matchingSets[counter++] = dataSet[i];
                    
                }
            }


            matchingSets = matchingSets.Where(x => x != null).ToArray();

            return matchingSets;

        }

        public async Task WriteFile(string textToWrite)
        {
            IFolder rootFolder = PCLStorage.FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("ReactionTime",
                CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("DataBetweenDates.csv",
                CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(textToWrite);
        }
        private async Task<string[][]> ReadData(string filename)
        {
            string[][] dataSet = null; 
            IFile data = null;
            //Get file
            try
            {
                IFolder rootFolder = PCLStorage.FileSystem.Current.LocalStorage;
                IFolder folder = await rootFolder.GetFolderAsync("ReactionTest");
                data = await folder.GetFileAsync(filename);
            }
            catch (PCLStorage.Exceptions.FileNotFoundException exception)
            {
                System.Diagnostics.Debug.Print(exception.ToString());
                await DisplayAlert("No tests recorded", "Could not retrieve data from file", "OK");
            }

            //Convert data
            try
            {
                dataSet = await prepareData(data);
            }
            catch (NullReferenceException)
            {
                await DisplayAlert("No data", "There was no data to be sent", "OK");
            }
            return dataSet;
            
        }

        private async Task<String[][]> prepareData(IFile data)
        {
            if (data != null)
            {
                string allData = await data.ReadAllTextAsync();
                string[] lines = allData.Split( //contains all lines(inc "")
                        new[] { Environment.NewLine },
                        StringSplitOptions.None);
                string[][] result = new string[lines.Length-1][];

                for(int i = 0; i < result.Length; i++)
                {
                    result[i] = lines[i].Split(divideSign);
                }

                return result;
            }

            return null;
        }

        public async Task SendEmail(string subject, string body, List<string> recipients, List<Xamarin.Essentials.EmailAttachment> attachments)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                    Attachments = attachments
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await DisplayAlert("Not supported", "Email is not supported on this device", "Ok");
            }
            catch (Exception ex)
            {
                // Some other exception occurred
                await DisplayAlert("Error", "Email is not supported on this device", "Ok");
            }
        }


    }
}