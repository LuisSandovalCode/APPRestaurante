using Restaurante.APP.ViewModel.Restaurante;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurante.APP.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservacionesRealizadas : ContentPage
    {
        public ReservacionesRealizadas()
        {
            InitializeComponent();

            //BindingContext = ReservacionesRealizadasViewModel.GetInstance();
            BindingContext = new ReservacionesRealizadasViewModel();
        }
    }
}