using Restaurante.APP.View.Home;
using Restaurante.APP.ViewModel.Utilidades;
using Restaurante.APP.ViewModel.Utilidades.UtilidadesUI;
using System.Windows.Input;
using Xamarin.Forms;

namespace Restaurante.APP.ViewModel.Usuario
{
    public class LoginViewModel : PropiedadNotificacion
    {
        #region [Propiedades]
        public string _Correo { get; set; }

        public string _Contrasena { get; set; }

        public string Correo
        {
            set
            {
                _Correo = value;
                OnPropertyChanged("Correo");
            }

            get
            {
                return _Correo;
            }
        }

        public string Contrasena
        {
            set
            {
                _Contrasena = value;
                OnPropertyChanged("Contrasena");
            }

            get
            {
                return _Contrasena;
            }
        }

        public ICommand IniciarSesionCommand { get; set; }
        #endregion

        #region [Metodos]
        public async void IniciarSesion()
        {
            UtilidadNavegacionUI utilidadNavegacionUI = new UtilidadNavegacionUI();
            utilidadNavegacionUI.CrearMasterDetailPage(new HomeMenuView(), new HomeView());
        }
        #endregion

        #region [Constructor]
        public LoginViewModel()
        {
            IniciarSesionCommand = new Command(IniciarSesion);
        } 
        #endregion
    }
}
