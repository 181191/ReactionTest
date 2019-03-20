using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;


namespace ReactionTest
{
    public interface IBaseUrl { string Get(); }

    class LocalHtml : MainPage
    {
        public LocalHtml()
        {
            var browser = new WebView();

            var htmlSource = new HtmlWebViewSource();

            

            htmlSource.Html =
                            @"<html>
            <head>
            <link rel=""stylesheet"" href=""default.css"">
            </head>
            <body>
            <h1>Xamarin.Forms</h1>
            <p>The CSS and image are loaded from local files!</p>
            <img src='bild.jpg'/>
            
            </body>
            </html>";

            //"CodeFile1.html";


            //important for seperate code files
            //var source = new HtmlWebViewSource();
            htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();


            browser.Source = htmlSource;
            Content = browser;
        }
    }
}
