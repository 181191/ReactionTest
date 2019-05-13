using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;

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

        private async void Settings_Clicked(object sender, EventArgs e)
        {

            PromptConfig pc = new PromptConfig{ InputType=InputType.Password, Title = "Password:", IsCancellable = true, MaxLength = 4};

            PromptResult pResult = await UserDialogs.Instance.PromptAsync(pc);

            if (pResult.Ok && pResult.Text == "1234")
            {
                await Navigation.PushAsync(new DataPage(pResult.Text));
            }
            else if(pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text))
            {
                UserDialogs.Instance.Alert("Wrong password");
            }
        }
    }
}