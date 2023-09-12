using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace BPayPOC1.Models
{
    public class CustomWebView : WebView
    {
        public event EventHandler CloseButtonClicked;

        public void OnCloseButtonClicked()
        {
            CloseButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }

}
