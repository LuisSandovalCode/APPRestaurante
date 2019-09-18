using Newtonsoft.Json;
using Restaurante.APP.ExternalServices;
using Restaurante.APP.Model.Restaurante;
using Restaurante.APP.ViewModel.Utilidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Restaurante.APP.ViewModel.Restaurante
{
    public class AgregarRestauranteViewModel : PropiedadNotificacion
    {

        public DateTime FechaMaxima { get => DateTime.Now; }

        public DateTime FechaMinima { get => DateTime.Now.AddYears(-10); }
        public RestauranteModel _Restaurante { get; set; }

        public static SVRestaurante ServicioRestaurante { get; set; }

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

        public ICommand RegistrarRestauranteCommand { get; set; }

        public AgregarRestauranteViewModel()
        {
            if (ServicioRestaurante == null)
                ServicioRestaurante = new SVRestaurante();

            RegistrarRestauranteCommand = new Command(RegistrarRestaurante);
        }

        public async void RegistrarRestaurante()
        {
            if(Restaurante!=null)
            {
                string JsonRestaurante = JsonConvert.SerializeObject(Restaurante);

                bool RegistroRestaurante = await ServicioRestaurante.RegistrarRestaurante(JsonRestaurante);

                
                if (RegistroRestaurante)
                    await App.Current.MainPage.DisplayAlert("Restaurante", "Restaurante Registrado con éxito", "Ok");
                else
                    await App.Current.MainPage.DisplayAlert("Restaurante", "Error al registrar Restaurante, por favor verifique los datos", "Ok");
            }
        }
    }
}
