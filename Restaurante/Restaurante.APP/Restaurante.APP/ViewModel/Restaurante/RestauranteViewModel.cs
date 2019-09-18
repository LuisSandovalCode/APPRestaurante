using Newtonsoft.Json;
using Restaurante.APP.ExternalServices;
using Restaurante.APP.Model.Restaurante;
using Restaurante.APP.Model.Usuario;
using Restaurante.APP.ViewModel.Utilidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Restaurante.APP.ViewModel.Restaurante
{
    public class RestauranteViewModel : PropiedadNotificacion
    {
        public RestauranteModel _Restaurante { get; set; }

        private static RestauranteViewModel _instance { get; set; }

        public  UsuarioModel UsuarioLogeado  { get; set; }

        private static SVRestaurante ServicioRestaurante;

        public ObservableCollection<RestauranteModel> _ListaRestaurante { get; set; }


        public ICommand ReservarCommand { get; set; }

        public ObservableCollection<RestauranteModel> ListaRestaurantes
        {
            set
            {
                _ListaRestaurante = value;
                OnPropertyChanged("ListaRestaurantes");
            }

            get
            {
                return ListaRestaurantes;
            }
        }

        public RestauranteModel Restaurante
        {
            get
            {
                return _Restaurante;
            }
            set
            {
                _Restaurante = value;
                OnPropertyChanged("Restaurante");
            }
        }

        public  RestauranteViewModel()
        {
            if (ServicioRestaurante == null)
                ServicioRestaurante = new SVRestaurante();

            CargarRestaurantes();

            ReservarCommand = new Command<RestauranteModel>(Reservar);
        }

        public async void CargarRestaurantes()
        {

            string JsonRestaurantes = await ServicioRestaurante.ObtenerRestaurantes();

            if (!string.IsNullOrEmpty(JsonRestaurantes))
            {
                ListaRestaurantes = JsonConvert.DeserializeObject<ObservableCollection<RestauranteModel>>(JsonRestaurantes);
            }
        }


        public static RestauranteViewModel GetInstance()
        {
            if (_instance == null)
                _instance = new RestauranteViewModel();

            return _instance;
        }

        public async void Reservar(RestauranteModel restaurante)
        {
            if(restaurante!=null)
            {
                var Confirmacion = await App.Current.MainPage.DisplayAlert("Restaurante", "¿Desea realizar ésta reservación?", "Sí", "No");

                if(Confirmacion)
                {
                    restaurante.CedulaUsuario = UsuarioLogeado.Cedula;

                    var jsonReservacion = JsonConvert.SerializeObject(restaurante);

                    var Reservo = await ServicioRestaurante.RealizarReseracion(jsonReservacion);

                    if (Reservo)
                    {
                        await App.Current.MainPage.DisplayAlert("Restaurante","Reservacion Realizada con exito","Ok");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Restaurante", "Error en la reservación", "Ok");
                    }
                }
            }
        }
    }
}
