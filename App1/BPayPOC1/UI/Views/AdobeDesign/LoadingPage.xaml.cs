using BPayPOCns;
using BPayPOCns.Views;
using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BPayPOC1.Views.AdobeDesign
{
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();

            // Inicializa la barra de progreso
            loadingProgressBar.Progress = 0;

            // Inicia la animacion para llenar la barra de progreso gradualmente
            AnimateProgressBar();
        }

        private async void AnimateProgressBar()
        {
            // Duracion total de la animacion en milisegundos
            int animationDurationMs = 3000; // 3 segundos

            // Intervalo de actualizacion de la barra de progreso (en milisegundos)
            int updateIntervalMs = 100;

            // Calcula el incremento de progreso por intervalo
            double progressIncrement = 1.0 / (animationDurationMs / updateIntervalMs);

            while (loadingProgressBar.Progress < 1.0)
            {
                // Incrementa gradualmente el progreso
                loadingProgressBar.Progress += progressIncrement;

                // Espera antes de la proxima actualizacion
                await Task.Delay(updateIntervalMs);
            }

            // Una vez que la barra de progreso esta llena, puedes realizar otras acciones o navegar a la siguiente pagina.
            // Por ejemplo, puedes usar MainPage = new AppShell(); para navegar a la página principal.
            Application.Current.MainPage = new AppShell();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Aqui debes realizar cualquier tarea de carga que necesites antes de mostrar la pagina principal.
            // Esto podria incluir cargar datos, configuraciones, recursos, etc.

            // Simulacion de espera durante unos segundos (reemplaza esto con tu logica real)
            await System.Threading.Tasks.Task.Delay(2000); // Espera 3 segundos

            // Luego, navega a la pagina principal (reemplaza MainPage con tu pagina principal real)
            //Application.Current.MainPage = new MainPage();
        }
    }
}
