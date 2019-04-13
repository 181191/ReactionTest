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
	public partial class StartPage : ContentPage
	{
		public StartPage ()
		{
			InitializeComponent ();
		}

        private void ThreeMinuteTestClicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new RegisterID(3));

        }

        private void SevenMinuteTestClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterID(7));
        }

        private void TenMinuteTestClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterID(10));
        }
    }
}