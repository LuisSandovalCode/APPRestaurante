using Restaurante.APP.ViewModel.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurante.APP.View.Usuario
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NuevoUsuarioView : ContentPage
    {
        public NuevoUsuarioView()
        {
            InitializeComponent();

            BindingContext = new NuevoUsuarioViewModel();
        }
    }
}