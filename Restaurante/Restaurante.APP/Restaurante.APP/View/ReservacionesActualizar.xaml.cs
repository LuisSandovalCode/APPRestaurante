
using Restaurante.APP.ViewModel.Restaurante;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurante.APP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservacionesActualizar : ContentPage
    {
        public ReservacionesActualizar()
        {
            InitializeComponent();

            //BindingContext = new ReservacionesRealizadasViewModel();
            BindingContext = ReservacionesRealizadasViewModel.GetInstance();
        }
    }
}