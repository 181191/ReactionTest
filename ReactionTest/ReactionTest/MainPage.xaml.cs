using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReactionTest
{
    public partial class MainPage : ContentPage
    {
        private bool buttonActive; 

        public MainPage()
        {
            InitializeComponent();

            
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            if(isActive())
            {
                infoString.Text = "Miss";
            }
            else
            {
                infoString.Text = "Hit"; 
                
            }


        }

        private bool isActive()
        {
            return false; 
        }
    }
}
