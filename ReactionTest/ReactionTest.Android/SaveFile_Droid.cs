using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ReactionTest.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveFile_Droid))]
namespace ReactionTest.Droid
{
    public class SaveFile_Droid : ISaveFile
    {
        public void saveFile(string fileName, string text)
        {
            string filePath;
            try
            {
                string DocumentPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
                filePath = Path.Combine(DocumentPath, fileName);
                File.WriteAllText(filePath, text);
                ShowToast("File \"DataBetweenDates.csv\" saved in InternalStorage/Download...");

            }
            catch (Exception exception)

            {
                System.Diagnostics.Debug.Print(exception.ToString());
                try
                {
                    string documentPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath;
                    filePath = Path.Combine(documentPath, fileName);
                    File.WriteAllText(filePath, text);
                    ShowToast("File \"DataBetweenDates.csv\" saved in InternalStorage/Documents...");

                }
                catch (Exception exception2) {
                    System.Diagnostics.Debug.Print(exception2.ToString());
                }
            }
        }

        public void ShowToast(string text, bool IsLengthShort = false)
        {
            Handler mainHandler = new Handler(Looper.MainLooper);
            Java.Lang.Runnable runnableToast = new Java.Lang.Runnable(() =>
            {
                var duration = IsLengthShort ? ToastLength.Short : ToastLength.Long;
                Toast.MakeText(Android.App.Application.Context, text, duration).Show();
            });

            mainHandler.Post(runnableToast);
        }
    }
}