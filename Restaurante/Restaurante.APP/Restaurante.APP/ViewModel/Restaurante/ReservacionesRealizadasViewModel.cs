using Newtonsoft.Json;
using Restaurante.APP.ExternalServices;
using Restaurante.APP.Model.Restaurante;
using Restaurante.APP.ViewModel.Utilidades;
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

        private static ReservacionesRealizadasViewModel _instance;

        private ObservableCollection<Reservacion> _ListaReservaciones = new ObservableCollection<Reservacion>();

        private static SVRestaurante ServicioRestaurante;


        public ICommand EliminarReservacionCommand { get; set; }
        public ReservacionesRealizadasViewModel()
        {
            if (ServicioRestaurante == null)
                ServicioRestaurante = new SVRestaurante();

            CargarReservacionesUsuario();

            EliminarReservacionCommand = new Command<Reservacion>(EliminarReservacion);

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
            var usuario = JsonConvert.SerializeObject(RestauranteViewModel.GetInstance().UsuarioLogeado);

            string JsonReservaciones = await ServicioRestaurante.ObtenerReservacionesUsuario(usuario);

            ListaReservaciones = JsonConvert.DeserializeObject<ObservableCollection<Reservacion>>(JsonReservaciones);
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
        public static ReservacionesRealizadasViewModel GetInstance()
        {
            if (_instance == null)
                _instance = new ReservacionesRealizadasViewModel();

            return _instance;
        }
    }
}
