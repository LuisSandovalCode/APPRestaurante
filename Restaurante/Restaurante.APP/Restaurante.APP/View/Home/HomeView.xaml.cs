
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurante.APP.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        private static HomeView Instancia = null;
        public HomeView()
        {
            InitializeComponent();
        }

        public static HomeView ObtenerInstancia()
        {
            if (Instancia == null)
                Instancia = new HomeView();
            return Instancia;
        }
    }
}