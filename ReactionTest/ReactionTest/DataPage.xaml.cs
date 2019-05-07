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
	public partial class DataPage : ContentPage
	{
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

        private void Export_Clicked(object sender, EventArgs e)
        {

        }
    }
}