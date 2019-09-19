using Restaurante.APP.Model.Configuracion;
using Restaurante.APP.Model.Home;
using Restaurante.APP.View.Home;
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
        public HomeMenuViewModel()
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

            ListaOpcionesMenu.Add(new HomeMenu
            {
                Id = (int)EnumHomeMenu.Reservar,
                Nombre = "Reservar",
                Detalle = "Reserve su restaurante y disfrute",
                IconoAndroid = ConfigAndroid.MenuIconReservar,
                IconoIOS = ConfigIOS.MenuIconReservar
            });

            ListaOpcionesMenu.Add(new HomeMenu
            {
                Id = (int)EnumHomeMenu.VerReservaciones,
                Nombre = "Ver reservaciones",
                Detalle = "Observer las reservaciones realizadas",
                IconoAndroid = ConfigAndroid.MenuIconVerReservaciones,
                IconoIOS = ConfigIOS.MenuIconVerReservaciones
            });



            ListaOpcionesMenu.Add(new HomeMenu
            {
                Id = (int)EnumHomeMenu.CerrarSesion,
                Nombre = "Cerrar sesión",
                IconoAndroid = ConfigAndroid.MenuIconLogout,
                IconoIOS = ConfigIOS.MenuIconLogout
            });
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
                    UtilidadNavegacionUI.IrAView(new ReservacionesRealizadas());
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
