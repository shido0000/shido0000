using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Pages;
using BPayPOC1.Views.AdobeDesign;
using BPayPOC1;
using System.Net.Http;

namespace BPayPOCns.Views.AdobeDesign
{
    public partial class PaymentScreen : ContentPage
    {
        private bool isWebViewLoaded = false;

        public PaymentScreen()
        {
            InitializeComponent();

            //get payment URL
            //string url = "https://finance.yahoo.com";
            string url = "";

            string tokenUrl = "http://"+Constants.paymentDNS+"/bppayserver/api/payment?apikey=" + Constants.APIKey + "&id=2";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(tokenUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                url = json;
            }
            else
            {
                DisplayAlert("Error", "No se pudo obtener los datos de la API.", "Aceptar");
            }


            // Configurar WebView
            if (Device.RuntimePlatform == Device.Android)
            {
                
                // Cargar el sitio web externo
                webView.Source = new UrlWebViewSource { Url = url };
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                // Configuracion especifica para iOS si es necesario
                webView.Source = new UrlWebViewSource { Url = url };
            }
            ShowCustomDialog();
            SubscribeToWebViewEvents();

            // Suscribirse al evento Navigated del WebView
            /*  if (isWebViewLoaded == false) { 
                   SubscribeToWebViewEvents();
              }*/
        }

        private void SubscribeToWebViewEvents()
        {
            // Suscribirse al evento Navigated del WebView
            webView.Navigated += async (sender, e) =>
            {
                Console.WriteLine("e.Result = " + e.Result);
                Console.WriteLine("WebNavigationResult.Success = " + WebNavigationResult.Success);
                Console.WriteLine("isWebViewLoaded = " + isWebViewLoaded);

                try
                {
                    if (e.Result == WebNavigationResult.Success && isWebViewLoaded == false)
                    {
                        // El WebView ha cargado contenido correctamente
                        isWebViewLoaded = true;
                        Console.WriteLine("isWebViewLoaded = " + isWebViewLoaded);

                        // Oculta el mensaje de carga
                        await HideCustomDialogAsync();
                    }
                    else
                    {
                        // El WebView no pudo cargar contenido
                        // Puedes manejar errores aqui si es necesario
                        Console.WriteLine("El WebView no pudo cargar contenido");
                    }
                }
                catch (Exception ex)
                {
                    // Maneja la excepcion aqui, por ejemplo, muestra un mensaje de error o registra la excepcion
                    Console.WriteLine("Excepción en el evento Navigated: " + ex.Message);
                }
            };
        }



        /*  private void SubscribeToWebViewEvents()
          {
              // Suscribirse al evento Navigated del WebView
              webView.Navigated += async (sender, e) =>
              {
                  Console.WriteLine("e.Result = " + e.Result);
                  Console.WriteLine("WebNavigationResult.Success = " + WebNavigationResult.Success);
                  Console.WriteLine("isWebViewLoaded = " + isWebViewLoaded);

                  if (e.Result == WebNavigationResult.Success && isWebViewLoaded == false)
                  {
                      // El WebView ha cargado contenido correctamente
                      isWebViewLoaded = true;
                      Console.WriteLine("isWebViewLoaded = " + isWebViewLoaded);

                      // Oculta el mensaje de carga
                      await HideCustomDialogAsync();

                  }
                  else
                  {
                      // El WebView no pudo cargar contenido
                      // Puedes manejar errores aqui si es necesario
                      Console.WriteLine("El WebView cargado");

                  }
              };
          }*/

        private async void ShowCustomDialog()
        {
            await PopupNavigation.Instance.PushAsync(new CustomLoading());
        }
        private async Task HideCustomDialogAsync()
        {
            // Oculta el dialogo personalizado solo si el WebView se ha cargado completamente
            if (isWebViewLoaded==true)
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }

       
        // Evento que se ejecutara despues de cargar el sitio web
        private async Task OnNavigatedAsync(object sender, WebNavigatedEventArgs e)
        {
            /*  if (e.Result == WebNavigationResult.Success)
              {
                  // La pagina web se ha cargado completamente
                  isWebViewLoaded = true;
                  Console.WriteLine("isWebViewLoaded = " + isWebViewLoaded);
              }

              else
              {
                  // La navegacion no se completo con exito
                  // Puedes manejar los errores aqui si es necesario
              }*/
            //  await HideCustomDialogAsync();
            
            if (e.Result == WebNavigationResult.Success && isWebViewLoaded == false)
            {
                // El WebView ha cargado contenido correctamente
                isWebViewLoaded = true;
                Console.WriteLine("isWebViewLoaded = " + isWebViewLoaded);
                

                // Oculta el mensaje de carga
                //  await HideCustomDialogAsync();


                // Operacion await para mantener el metodo como asincronico
                await Task.Delay(0);

                // Inyectar el boton de Regresar mediante JavaScript despues de que el contenido del WebView se haya cargado completamente
                string script = @"
                var para_boton = document.getElementById('para_boton');
                var regresarButton = document.createElement('button');
                regresarButton.innerHTML = 'Regresar';
                regresarButton.style.backgroundColor = 'green';
                regresarButton.style.color = 'white';
                regresarButton.style.padding = '10px';
                regresarButton.onclick = function() {
                    window.location.href = 'XamarinEssentials:Terminado';
                };
                para_boton.appendChild(regresarButton);

                // Crear un espacio en blanco debajo del botón Regresar
                var spaceDiv = document.createElement('div');
                spaceDiv.style.height = regresarButton.offsetHeight + 'px';
                para_boton.appendChild(spaceDiv);
            ";

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await webView.EvaluateJavaScriptAsync(script);
                    // Despues de inyectar el boton, muestra el mensaje de carga
                   // ShowCustomDialog();
                });
                // Oculta el mensaje de carga
                await HideCustomDialogAsync();

            }
            else
            {
                // La navegacion no se completo con exito
                // Puedes manejar los errores aqui si es necesario
            }

              if (isWebViewLoaded)
              {
                  // El WebView se ha cargado completamente
              }
        }

        // Evento que se ejecuta antes de que comience la navegacion en el WebView
       
        // Evento que se ejecuta cuando el usuario hace clic en el boton "Regresar"
        private async void OnRegresarClicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Confirmación", "¿Estás seguro de que quieres regresar a la aplicación?", "Sí", "No");

            if (result) // Si el usuario hace clic en "Si"
            {
                // Aqui puedes agregar la logica para regresar a la aplicacion si lo deseas.


                // Puedes usar Xamarin.Essentials o cualquier otro metodo que uses para la navegacion o finalizacion de la pagina actual.

                // Por ahora, solo mostraremos un mensaje indicando que se va a regresar a la aplicacion.
                await DisplayAlert("Mensaje", "Regresando a la aplicación...", "OK");

                // Realiza la navegacion hacia la nueva pagina
                await Navigation.PushAsync(new Client());

            }
            else
            {
                // Si el usuario hace clic en "No" o cierra el mensaje emergente, no realizas ninguna accion.
                // El usuario decidio no regresar.
            }
        }

        private void webView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            
            /*  if (e.Result == WebNavigationResult.Success)
             {
                 // La pagina web se ha cargado completamente
                 isWebViewLoaded = true;
                 Console.WriteLine("isWebViewLoaded = " + isWebViewLoaded);
             }

             else
             {
                 // La navegacion no se completo con exito
                 // Puedes manejar los errores aqui si es necesario
             }*/
            //  await HideCustomDialogAsync();

            /*  if (e.Result == WebNavigationResult.Success && isWebViewLoaded == false)
              {
                  // El WebView ha cargado contenido correctamente
                  isWebViewLoaded = true;
                  Console.WriteLine("isWebViewLoaded = " + isWebViewLoaded);


                  // Oculta el mensaje de carga
                  //  await HideCustomDialogAsync();


                  // Operacion await para mantener el metodo como asincronico
                  await Task.Delay(0);

                  // Inyectar el boton de Regresar mediante JavaScript despues de que el contenido del WebView se haya cargado completamente
                  string script = @"
                  var para_boton = document.getElementById('para_boton');
                  var regresarButton = document.createElement('button');
                  regresarButton.innerHTML = 'Regresar';
                  regresarButton.style.backgroundColor = 'green';
                  regresarButton.style.color = 'white';
                  regresarButton.style.padding = '10px';
                  regresarButton.onclick = function() {
                      window.location.href = 'XamarinEssentials:Terminado';
                  };
                  para_boton.appendChild(regresarButton);

                  // Crear un espacio en blanco debajo del botón Regresar
                  var spaceDiv = document.createElement('div');
                  spaceDiv.style.height = regresarButton.offsetHeight + 'px';
                  para_boton.appendChild(spaceDiv);
              ";

                /*  Device.BeginInvokeOnMainThread(async () =>
                  {
                      await webView.EvaluateJavaScriptAsync(script);
                      // Despues de inyectar el boton, muestra el mensaje de carga
                     // ShowCustomDialog();
                  });
                  // Oculta el mensaje de carga
                  await HideCustomDialogAsync();

              }
              else
              {
                  // La navegacion no se completo con exito
                  // Puedes manejar los errores aqui si es necesario
              }*/

            /*  if (isWebViewLoaded)
              {
                  // El WebView se ha cargado completamente
              }*/
        }

    }
}
