using BPayPOC1.Views.AdobeDesign;
using BPayPOCns.Services;
using BPayPOCns.Views;
using System;
using System.Threading.Tasks; 
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BPayPOCns
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            // Establece la pagina de inicio como LoadingPage
            MainPage = new NavigationPage(new LoadingPage());
        }

        protected override async void OnStart()
        {
          
        }

        protected override void OnSleep()
        {
            // Maneja la suspension de la aplicacion si es necesario
        }

        protected override void OnResume()
        {
            // Maneja la reanudacion de la aplicacion si es necesario
        }
    }
}
