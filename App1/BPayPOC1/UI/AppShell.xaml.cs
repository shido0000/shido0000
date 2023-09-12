using BPayPOC1.Views.AdobeDesign;
using BPayPOCns.ViewModels;
using BPayPOCns.Views;
using BPayPOCns.Views.AdobeDesign;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace BPayPOCns
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // MainPage = new NavigationPage(new LoadingPage());
            //Routing.RegisterRoute(nameof(LoadingPage), typeof(LoadingPage));
            Routing.RegisterRoute(nameof(Main), typeof(Main));
            Routing.RegisterRoute(nameof(Client), typeof(Client));

        }

      
    }
}
