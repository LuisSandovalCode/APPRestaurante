using Newtonsoft.Json;
using Restaurante.APP.ExternalServices;
using Restaurante.APP.Model.Restaurante;
using Restaurante.APP.View;
using Restaurante.APP.ViewModel.Utilidades;
using Restaurante.APP.ViewModel.Utilidades.UtilidadesUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Restaurante.APP.ViewModel.Restaurante
{
    public class ReservacionesRealizadasViewModel : PropiedadNotificacion
    {

        public bool _IsLoading { get; set; }

        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                _IsLoading = false;
                OnPropertyChanged("IsLoading");
            }
        }

        private static ReservacionesRealizadasViewModel _instance;

        private ObservableCollection<Reservacion> _ListaReservaciones = new ObservableCollection<Reservacion>();

        private Reservacion _reservacion = new Reservacion();

        public Reservacion ReservacionET
        {
            get { return _reservacion; }
            set
            {
                _reservacion = value;
                OnPropertyChanged("ReservacionET");
            }
        }

        public DateTime FechaMaxima { get => DateTime.Now; }

        public DateTime FechaMinima { get => DateTime.Now.AddYears(-50); }

        private static SVRestaurante ServicioRestaurante;


        public ICommand EliminarReservacionCommand { get; set; }
        public ICommand EnterActualizarReservacionCommand { get; set; }
        public ICommand ActualizarReservacionCommand { get; set; }
        public ReservacionesRealizadasViewModel()
        {
            if (ServicioRestaurante == null)
                ServicioRestaurante = new SVRestaurante();

            CargarReservacionesUsuario();

            EliminarReservacionCommand = new Command<Reservacion>(EliminarReservacion);
            EnterActualizarReservacionCommand = new Command<Reservacion>(InicializarActualizarReservacionCommand);
            ActualizarReservacionCommand = new Command(ActualizarReservacion);

        }

        public ObservableCollection<Reservacion> ListaReservaciones
        {
            get
            {
                return _ListaReservaciones;
            }
            set
            {
                _ListaReservaciones = value;
                OnPropertyChanged("ListaReservaciones");
            }
        }

        public async void CargarReservacionesUsuario()
        {
            IsLoading = true;
            var usuario = JsonConvert.SerializeObject(RestauranteViewModel.GetInstance().UsuarioLogeado);

            string JsonReservaciones = await ServicioRestaurante.ObtenerReservacionesUsuario(usuario);

            ListaReservaciones = JsonConvert.DeserializeObject<ObservableCollection<Reservacion>>(JsonReservaciones);

            IsLoading = false;
        }

        public async void EliminarReservacion(Reservacion reservacion)
        {
            if (reservacion != null)
            {
                var Confirmacion = await App.Current.MainPage.DisplayAlert("Restaurante", "¿Desea deshacer esta reservación?", "Sí", "No");

                if (Confirmacion)
                {


                    Reservacion vloReservacion = new Reservacion
                    {
                        IdReservacion = reservacion.IdReservacion
                    };


                    var jsonReservacion = JsonConvert.SerializeObject(vloReservacion);

                    var Reservo = await ServicioRestaurante.EliminarReservacion(jsonReservacion);

                    if (Reservo)
                    {

                        var vloAux = ListaReservaciones.Where(x => x.IdReservacion != vloReservacion.IdReservacion).ToList();
                        ListaReservaciones.Clear();
                        foreach (var item in vloAux)
                        {
                            ListaReservaciones.Add(item);
                        }
                        await App.Current.MainPage.DisplayAlert("Restaurante", "Reservacion cancelada con exito", "Ok");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Restaurante", "Error en la reservación", "Ok");
                    }
                }
            }
        }

        public void InicializarActualizarReservacionCommand(Reservacion reservacion)
        {
            UtilidadNavegacionUI.IrAView(new ReservacionesActualizar());
            Reservacion vloAuxReservacion = new Reservacion();
            vloAuxReservacion.Nombre = reservacion.Nombre;
            vloAuxReservacion.FechaReservacion = reservacion.FechaReservacion;
            vloAuxReservacion.IdReservacion = reservacion.IdReservacion;

            ReservacionET = vloAuxReservacion;
        }

        public async void ActualizarReservacion()
        {
            if (ReservacionET != null)
            {
                var Confirmacion = await App.Current.MainPage.DisplayAlert("Restaurante", "¿Desea modificar realmente la fecha de su reservación?", "Sí", "No");

                if (Confirmacion)
                {


                    Reservacion vloReservacion = new Reservacion
                    {
                        IdReservacion = ReservacionET.IdReservacion,
                        Nombre = ReservacionET.Nombre,
                        FechaReservacion = ReservacionET.FechaReservacion
                    };


                    var jsonReservacion = JsonConvert.SerializeObject(vloReservacion);

                    var Reservo = await ServicioRestaurante.ActualizarReservacion(jsonReservacion);

                    if (Reservo)
                    {
                        var vloAux = ListaReservaciones.ToList();
                        ListaReservaciones.Clear();
                        foreach (var item in vloAux)
                        {
                            if (vloReservacion.IdReservacion == item.IdReservacion)
                                ListaReservaciones.Add(vloReservacion);
                            else
                                ListaReservaciones.Add(item);
                        }
                        await App.Current.MainPage.DisplayAlert("Restaurante", "Reservacion actualizada con exito", "Ok");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Restaurante", "Error en la actualización de la reservación", "Ok");
                    }
                }
            }
        }


        public static ReservacionesRealizadasViewModel GetInstance()
        {

            if (_instance == null)
                _instance = new ReservacionesRealizadasViewModel();

            return _instance;
        }

        public static ReservacionesRealizadasViewModel GetInstanceActualizar()
        {
            _instance = new ReservacionesRealizadasViewModel();

            return _instance;
        }
    }
}
