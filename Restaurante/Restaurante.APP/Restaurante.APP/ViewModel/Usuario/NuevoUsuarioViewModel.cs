using Newtonsoft.Json;
using Restaurante.APP.ExternalServices;
using Restaurante.APP.Model.Usuario;
using Restaurante.APP.ViewModel.Utilidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Restaurante.APP.ViewModel.Usuario
{
    public class NuevoUsuarioViewModel : PropiedadNotificacion
    {

        #region [Propiedades]

        public bool _isLoading { get; set; }

        private static SVUsuario ServicioUsuario;

        public bool _EsCorreoValido { get; set; }

        public bool _EsContrasenaIgual { get; set; }

        public bool _EsTelefonoValido { get; set; }
        public DateTime FechaMaxima { get => DateTime.Now; }

        public DateTime FechaMinima { get => DateTime.Now.AddYears(-50); }
        public UsuarioModel _Usuario { get; set; }

        public ICommand RegistrarUsuarioCommand { get; set; }
        public UsuarioModel Usuario
        {
            get
            {
                return _Usuario;
            }

            set
            {
                _Usuario = value;
                OnPropertyChanged("Usuario");
            }
        }

        public bool EsTelefonoValido
        {
            get
            {
                return _EsTelefonoValido;
            }
            set
            {
                _EsTelefonoValido = value;
                OnPropertyChanged("EsTelefonoValido");
            }
        }

        public bool EsContrasenaIgual
        {
            get
            {
                return _EsContrasenaIgual;
            }
            set
            {
                _EsContrasenaIgual = value;
                OnPropertyChanged("EsContrasenaIgual");
            }
        }

        public bool EsCorreoValido
        {
            get
            {
                return _EsCorreoValido;
            }
            set
            {
                _EsCorreoValido = value;
                OnPropertyChanged("EsCorreoValido");
            }
        }

        public bool isLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                _isLoading = value;
                OnPropertyChanged("isLoading");
            }
        }
        #endregion

        #region [Metodos y Constructor]
        public NuevoUsuarioViewModel()
        {
            if (ServicioUsuario == null)
                ServicioUsuario = new SVUsuario();
            _Usuario = new UsuarioModel();
            RegistrarUsuarioCommand = new Command(RegistrarUsuario);
        }

        public async void RegistrarUsuario()
        {
            if (!EsContrasenaIgual)
                await App.Current.MainPage.DisplayAlert("Restaurante", "Las contraseñas no coinciden", "Ok");
            if(!EsCorreoValido)
                await App.Current.MainPage.DisplayAlert("Restaurante", "El correo no es valido", "Ok");
            if(!EsTelefonoValido)
                await App.Current.MainPage.DisplayAlert("Restaurante", "El teléfono no es valido", "Ok");

            isLoading = true;

            string JsonUsuario = JsonConvert.SerializeObject(Usuario);

            bool RegistroUsuario = await ServicioUsuario.RegistrarUsuario(JsonUsuario);

            isLoading = false;

            if(RegistroUsuario)
                await App.Current.MainPage.DisplayAlert("Restaurante", "Usuario Registrado con éxito", "Ok");
            else
                await App.Current.MainPage.DisplayAlert("Restaurante", "Error al registrar usuario, por favor verifique los datos", "Ok");

        } 
        #endregion

    }
}
