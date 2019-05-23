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
            string DocumentPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
            string documentPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath;
            string filePath;
            try
            {
                filePath = Path.Combine(DocumentPath, fileName);
                File.WriteAllText(filePath, text);
            }
            catch (Exception)
            {
                try
                {
                    filePath = Path.Combine(documentPath, fileName);
                    File.WriteAllText(filePath, text);
                }
                catch (Exception) { }
            }
        }
    }
}