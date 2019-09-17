using Restaurante.APP.Model.Configuracion;
using Restaurante.APP.Model.Home;
using Restaurante.APP.ViewModel.Utilidades;
using Restaurante.APP.ViewModel.Utilidades.UtilidadesUI;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Restaurante.APP.ViewModel.Home
{
    public class HomeMenuViewModel : PropiedadNotificacion
    {
        #region :::: Atributos y Objetos ::::
        private static HomeMenuViewModel Instancia = null;

        public  ImageSource FotoPerfil { get; set; }

        public  string NombreUsuario { get; set; }

        private ObservableCollection<HomeMenu> listaOpcionesMenu = new ObservableCollection<HomeMenu>();
        public ObservableCollection<HomeMenu> ListaOpcionesMenu
        {
            get { return listaOpcionesMenu; }
            set
            {
                listaOpcionesMenu = value;
                OnPropertyChanged("ListaOpcionesMenu");
            }
        }
        #endregion :::: Atributos y Objetos ::::

        #region :::: Métodos ::::

        #region [Inicialización]
        protected HomeMenuViewModel()
        {
            IntegrarInicializacionClase();
        }
        protected void IntegrarInicializacionClase()
        {
            InicializarMenu();
            InicializarCommands();

        }
        protected void InicializarMenu()
        {
            ListaOpcionesMenu = new ObservableCollection<HomeMenu>();
            ListaOpcionesMenu.Add(new HomeMenu((int)EnumHomeMenu.Reservar, "Reservar", "Reserve su restaurante y disfrute", ConfigAndroid.HomeIconoReservar, ConfigIOS.HomeIconoReservar));
            ListaOpcionesMenu.Add(new HomeMenu((int)EnumHomeMenu.VerReservaciones, "Ver reservaciones", "Observer las reservaciones realizadas", ConfigAndroid.HomeIconoVerReservaciones, ConfigIOS.HomeIconoVerReservaciones));
            ListaOpcionesMenu.Add(new HomeMenu((int)EnumHomeMenu.Contactar, "Contactar", "Conctácte al restaurante", ConfigAndroid.HomeIconoContactar, ConfigIOS.HomeIconoContactar));
            ListaOpcionesMenu.Add(new HomeMenu((int)EnumHomeMenu.Informacion, "Información", "Detalles de uso de la aplicación", ConfigAndroid.HomeIconoInformacion, ConfigIOS.HomeIconoInformacion));
            ListaOpcionesMenu.Add(new HomeMenu((int)EnumHomeMenu.CerrarSesion, "Cerrar sesión", "", ConfigAndroid.HomeIconoLogout, ConfigIOS.HomeIconoLogout));
        }
        protected void InicializarCommands()
        {
            EjecutarHomeMenuCommand = new Command<int>(ImplemetarHomeMenuCommand);
        }
        public static HomeMenuViewModel ObtenerInstancia()
        {
            if (Instancia == null)
                Instancia = new HomeMenuViewModel();
            return Instancia;
        }
        #endregion [Inicialización]

        #region [Comandos]
        public ICommand EjecutarHomeMenuCommand { get; set; }

        public void ImplemetarHomeMenuCommand(int Id)
        {
            switch (Id)
            {
                case (int)EnumHomeMenu.Reservar:

                    break;

                case (int)EnumHomeMenu.VerReservaciones:

                    break;

                case (int)EnumHomeMenu.Contactar:

                    break;

                case (int)EnumHomeMenu.Informacion:

                    break;

                case (int)EnumHomeMenu.CerrarSesion:
                    UtilidadNavegacionUI.CerrarSession(new View.Usuario.LoginView());
                    break;
            }
        }
        #endregion [Comandos]


        #endregion :::: Métodos ::::
    }
}
