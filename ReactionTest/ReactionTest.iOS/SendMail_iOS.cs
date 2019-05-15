using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using ReactionTest.iOS;
using Xamarin.Forms;
using MessageUI;

[assembly: Dependency(typeof(SendMail_iOS))]
namespace ReactionTest.iOS
{
    class SendMail_iOS : ISendMail
    {
        public void sendMail(byte[]bytes, string mimeType, string content)
        {
            NSData data = NSData.FromArray(bytes);
            
            MFMailComposeViewController mailController;
            if (MFMailComposeViewController.CanSendMail)
            {
                mailController = new MFMailComposeViewController();
                mailController.SetToRecipients(new string[] { "john@doe.com" });
                mailController.SetSubject("mail test");
                mailController.SetMessageBody("content", false);
                mailController.AddAttachmentData(data, mimeType, "Data.csv");
                mailController.Finished += (object s, MFComposeResultEventArgs args) =>
                {
                    Console.WriteLine(args.Result.ToString());
                    args.Controller.DismissViewController(true, null);

                };
            }
        }
    }
}