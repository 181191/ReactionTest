using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ReactionTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            NavigationPage np = new NavigationPage(new StartPage());
            np.Popped += (s, e) =>
            {
                Console.WriteLine("Popped");
            };
            MainPage = np;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
