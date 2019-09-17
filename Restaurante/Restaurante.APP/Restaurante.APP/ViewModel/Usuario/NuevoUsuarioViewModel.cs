using Newtonsoft.Json;
using Restaurante.APP.ExternalServices;
using Restaurante.APP.Interfaces;
using Restaurante.APP.Model.Usuario;
using Restaurante.APP.ViewModel.Utilidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Restaurante.APP.ViewModel.Usuario
{
    public class NuevoUsuarioViewModel : PropiedadNotificacion
    {

        #region [Propiedades]

        public static string Base64Foto { get; set; }

        public Stream _StreamFoto { get; set; }

        public Stream StreamFoto
        {
            get
            {
                return _StreamFoto;
            }

            set
            {
                _StreamFoto = value;
                OnPropertyChanged("StreamFoto");
            }
        }

        public bool _isLoading { get; set; }

        private static SVUsuario ServicioUsuario;

        public bool _EsCorreoValido { get; set; }

        public bool _EsContrasenaIgual { get; set; }

        public bool _EsTelefonoValido { get; set; }
        public DateTime FechaMaxima { get => DateTime.Now; }

        public DateTime FechaMinima { get => DateTime.Now.AddYears(-50); }
        public UsuarioModel _Usuario { get; set; }

        public string _FotoPerfilBase64 { get; set; }
        public ImageSource _FotoPerfil { get; set; }
        public ICommand RegistrarUsuarioCommand { get; set; }

        public ICommand SeleccionarFotoPerfilCommand { get; set; }

        public string FotoPerfilBase64
        {
            set
            {
                _FotoPerfilBase64 = value;
                OnPropertyChanged("FotoPerfilBase64");
            }

            get
            {
                return _FotoPerfilBase64;
            }
        }

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

        public ImageSource FotoPerfil
        {
            set
            {
                _FotoPerfil = value;
                OnPropertyChanged("FotoPerfil");
            }
            get
            {
                return _FotoPerfil;
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
            SeleccionarFotoPerfilCommand = new Command(SeleccionarFotoPerfil);
        }

        public async void RegistrarUsuario()
        {
            try
            {
                if (!EsContrasenaIgual)
                {
                    await App.Current.MainPage.DisplayAlert("Restaurante", "Las contraseñas no coinciden", "Ok");
                    isLoading = false;
                    EsCorreoValido = false;
                    EsTelefonoValido = false;
                    return;
                }
                if (!EsCorreoValido)
                {
                    await App.Current.MainPage.DisplayAlert("Restaurante", "El correo no es valido", "Ok");
                    isLoading = false;
                    return;
                }

                if (!EsTelefonoValido)
                {
                    await App.Current.MainPage.DisplayAlert("Restaurante", "El teléfono no es valido", "Ok");
                    isLoading = false;
                    EsContrasenaIgual = false;
                    EsCorreoValido = false;
                    return;
                }

                if (!string.IsNullOrEmpty(Base64Foto))
                {
                    Usuario.FotoPerfil = Base64Foto;
                }

                isLoading = true;

                string JsonUsuario = JsonConvert.SerializeObject(Usuario);

                bool RegistroUsuario = await ServicioUsuario.RegistrarUsuario(JsonUsuario);

                isLoading = false;

                if (RegistroUsuario)
                    await App.Current.MainPage.DisplayAlert("Restaurante", "Usuario Registrado con éxito", "Ok");
                else
                    await App.Current.MainPage.DisplayAlert("Restaurante", "Error al registrar usuario, por favor verifique los datos", "Ok");
            }
            catch (Exception ex)
            {
                isLoading = false;
                await App.Current.MainPage.DisplayAlert("Restaurante", $"Error {ex.Message}", "Ok");
            }

        }

        public async void SeleccionarFotoPerfil()
        {
            try
            {
                isLoading = true;

                StreamFoto = await DependencyService.Get<IFotoPicker>().ObtenerFotoGaleriaAsync();

                if(StreamFoto != null)
                {

                    using (MemoryStream memory = new MemoryStream())
                    {
                        StreamFoto.CopyTo(memory);
                        Base64Foto = Convert.ToBase64String(memory.ToArray());
                    }
                    StreamFoto = null;
                    byte[] Base64Stream = Convert.FromBase64String(Base64Foto);
                    FotoPerfil = ImageSource.FromStream(()=>new MemoryStream(Base64Stream));

                }
                isLoading = false;

            }
            catch (Exception ex)
            {
                isLoading = false;
                await App.Current.MainPage.DisplayAlert("Restaurante", $"Error {ex.Message}", "Ok");
            }
        }
        #endregion

    }
}
