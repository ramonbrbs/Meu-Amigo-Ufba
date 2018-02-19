using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeuAmigo.Model;
using MeuAmigo.Views;
using Xamarin.Forms;

namespace MeuAmigo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Session.Navigation = new NavigationPage(new Login());
            MainPage = new Principal();
            
            ((Principal)MainPage).Detail = Session.Navigation;
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
