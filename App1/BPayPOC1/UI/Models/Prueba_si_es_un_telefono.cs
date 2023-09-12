using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BPayPOC1.Models
{
    internal class Prueba_si_es_un_telefono
    {
        public static bool RunningOnPhone()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
