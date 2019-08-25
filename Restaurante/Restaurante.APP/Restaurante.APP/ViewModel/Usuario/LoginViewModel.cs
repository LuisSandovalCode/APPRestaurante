using Newtonsoft.Json;
using Restaurante.APP.ExternalServices;
using Restaurante.APP.Model.Usuario;
using Restaurante.APP.View.Home;
using Restaurante.APP.View.Usuario;
using Restaurante.APP.ViewModel.Utilidades;
using Restaurante.APP.ViewModel.Utilidades.UtilidadesUI;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Restaurante.APP.ViewModel.Usuario
{
    public class LoginViewModel : PropiedadNotificacion
    {
        #region [Propiedades]

        private static SVUsuario ServicioUsuario;

        public LoginModel _UsuarioLogin { get; set; }

        public bool _EsCorreoValido { get; set; }

        public bool _EsContrasenaValida { get; set; }

        public LoginModel UsuarioLogin
        {
            set
            {
                _UsuarioLogin = value;
                OnPropertyChanged("UsuarioLogin");
            }

            get
            {
                return _UsuarioLogin;
            }
        }

        public bool EsContrasenaValida
        {
            set
            {
                _EsContrasenaValida = value;
                OnPropertyChanged("EsContrasenaValida");
            }

            get
            {
                return _EsContrasenaValida;
            }
        }

        public bool EsCorreoValido
        {
            set
            {
                _EsCorreoValido = value;
                OnPropertyChanged("EsCorreoValido");
            }

            get
            {
                return _EsCorreoValido;
            }
        }

        public ICommand IniciarSesionCommand { get; set; }

        public ICommand EnterRegistrarUsuarioCommand { get; set; }
        #endregion

        #region [Metodos]
        public async void IniciarSesion()
        {
            if (EsContrasenaValida && EsCorreoValido)
            {
                string JsonLogin = JsonConvert.SerializeObject(UsuarioLogin);
                string JsonRespuesta = await ServicioUsuario.IniciarSesion(JsonLogin);
                if (!string.IsNullOrEmpty(JsonRespuesta))
                {
                    await App.Current.MainPage.DisplayAlert("Restaurante", "Bienvenido", "Ok");
                    UtilidadNavegacionUI utilidadNavegacionUI = new UtilidadNavegacionUI();
                    utilidadNavegacionUI.CrearMasterDetailPage(new HomeMenuView(), new HomeView());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Restaurante", "Error en las credenciales", "Ok");
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Restaurante", "Datos No Correctos", "Ok");
            }
        }

        public void EnterRegistrarUsuario()
        {
            UtilidadNavegacionUI.IrAView(new NuevoUsuarioView());
        }

        #endregion

        #region [Constructor]
        public LoginViewModel()
        {
            if (ServicioUsuario == null)
                ServicioUsuario = new SVUsuario();
            UsuarioLogin = new LoginModel();
            IniciarSesionCommand = new Command(IniciarSesion);
            EnterRegistrarUsuarioCommand = new Command(EnterRegistrarUsuario);
        } 
        #endregion
    }
}
