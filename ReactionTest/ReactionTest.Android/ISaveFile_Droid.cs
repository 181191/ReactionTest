﻿using System;
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

[assembly: Dependency(typeof(ISaveFile_Droid))]
namespace ReactionTest.Droid
{
    public class ISaveFile_Droid : ISaveFile
    {
        public void saveFile(string fileName, string text)
        {
            string DocumentPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filePath = Path.Combine(DocumentPath, fileName);
            File.WriteAllText(filePath, text);
        }
    }
}