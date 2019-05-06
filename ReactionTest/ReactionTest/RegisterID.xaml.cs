using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReactionTest
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterID : ContentPage
	{
        private string testID;
        private int testLenght;
		public RegisterID ()
		{
			InitializeComponent();
		}

        public RegisterID(int testLength)
        {
            InitializeComponent();
            this.testLenght = testLength;

        }

        private void RegisterTestID(object sender, EventArgs e)
        {
            if (ID.Text != null)
            {
                testID = ID.Text;
                Page mPage = new MainPage(testLenght, testID);
                mPage.Title = testID;
                Navigation.PushAsync(mPage);
            }
            else
                DisplayAlert("No input", "UserID can't be empty", "OK");

        }
    }
}