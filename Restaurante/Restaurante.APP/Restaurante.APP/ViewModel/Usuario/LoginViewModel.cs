using Newtonsoft.Json;
using Restaurante.APP.BaseDatosLocal;
using Restaurante.APP.ExternalServices;
using Restaurante.APP.Model.Usuario;
using Restaurante.APP.View.Home;
using Restaurante.APP.View.Usuario;
using Restaurante.APP.ViewModel.Home;
using Restaurante.APP.ViewModel.Utilidades;
using Restaurante.APP.ViewModel.Utilidades.UtilidadesUI;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.IO;
using System;
namespace Restaurante.APP.ViewModel.Usuario
{
    public class LoginViewModel : PropiedadNotificacion
    {
        #region [Propiedades]

        public static HomeMenuView _InstanceHomeView { get; set; }

        private static RealmService ServicioReal;

        private static SVUsuario ServicioUsuario;

        private bool _IsLoading { get; set; }
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

        public bool IsLoading
        {
            set
            {
                _IsLoading = value;
                OnPropertyChanged("IsLoading");
            }

            get
            {
                return _IsLoading;
            }
        }
        public ICommand IniciarSesionCommand { get; set; }

        public ICommand EnterRegistrarUsuarioCommand { get; set; }
        #endregion

        #region [Metodos]
        public async void IniciarSesion()
        {
            IsLoading = true;
            string JsonLogin = JsonConvert.SerializeObject(UsuarioLogin);
            string JsonRespuesta = await ServicioUsuario.IniciarSesion(JsonLogin);
            if (!string.IsNullOrEmpty(JsonRespuesta))
            {
                IsLoading = false;
                await App.Current.MainPage.DisplayAlert("Restaurante", "Bienvenido", "Ok");
                bool ExistenCredenciales = ServicioReal.ValidarCredenciales(UsuarioLogin);
                if (!ExistenCredenciales)
                {
                    IsLoading = true;
                    await RegistrarCredenciales();
                    IsLoading = false;
                }
                UsuarioModel vloUsuario = JsonConvert.DeserializeObject<UsuarioModel>(JsonRespuesta);
                byte[] vloFotoPerfil = Convert.FromBase64String(vloUsuario.FotoPerfil);
                HomeMenuViewModel.ObtenerInstancia().FotoPerfil = ImageSource.FromStream(()=>new MemoryStream(vloFotoPerfil));
                HomeMenuViewModel.ObtenerInstancia().NombreUsuario = vloUsuario.Nombre + "  "+vloUsuario.Apellido;
                UtilidadNavegacionUI.CrearMasterDetailPage(new HomeMenuView(), new HomeView());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Restaurante", "Error en las credenciales", "Ok");
                IsLoading = false;
            }

        }

        public void EnterRegistrarUsuario()
        {
            App.Current.MainPage = new NuevoUsuarioView();
        }

        public async Task RegistrarCredenciales()
        {
            var Confirmacion = await App.Current.MainPage.DisplayAlert("Restaurante", "¿Desea registrar sus credenciales la próxima vez?", "Sí", "No");

            if (Confirmacion)
            {
                bool RegistroCrendenciales = ServicioReal.GuardarCredenciales(UsuarioLogin);

                if (!RegistroCrendenciales)
                    await App.Current.MainPage.DisplayAlert("Restaurante", "Error al registrar credenciales", "Ok");
            }
        }

        #endregion

        #region [Constructor]
        public LoginViewModel()
        {
            if (ServicioUsuario == null)
                ServicioUsuario = new SVUsuario();
            if (ServicioReal == null)
                ServicioReal = new RealmService();
            _UsuarioLogin = ServicioReal.RecordarCredenciales();
            IniciarSesionCommand = new Command(IniciarSesion);
            EnterRegistrarUsuarioCommand = new Command(EnterRegistrarUsuario);
        }
        #endregion
    }
}
