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
        private int testTime;
        private bool buttonActive;
        private bool testStarted;

        private double timeSinceActice; 

        private int anticipationMiss; 
        private int minorLaps;
        private int majorLaps;
        private int miss;
        private int hit; 

        public MainPage()
        {
            InitializeComponent();

            
        }
        public MainPage(int testTime)
        {
            InitializeComponent();
            this.testTime = testTime;
        }

        void OnButtonClicked(object sender, EventArgs args)
        {
            if (!testStarted)
            {
             //   initTest(); 
            }
            else
            {
                testClick(); 
            }


        }

        private void testClick()
        {
    
        }
    }
}
