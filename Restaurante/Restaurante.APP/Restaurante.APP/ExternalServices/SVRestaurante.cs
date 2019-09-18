using Restaurante.APP.ConfiguracionApp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.APP.ExternalServices
{
    public class SVRestaurante
    {
        private static string URLWebApi { get; set; }
        private static string Controller { get; set; }
        private static string ControllerReservacion { get; set; }

        public SVRestaurante()
        {
            if (string.IsNullOrEmpty(URLWebApi))
                URLWebApi = AppSettingsManager.Settings["Service"];
            if (string.IsNullOrEmpty(Controller))
                Controller = AppSettingsManager.Settings["RestauranteController"];
            if (string.IsNullOrEmpty(ControllerReservacion))
                ControllerReservacion = AppSettingsManager.Settings["ReservacionController"];
        }

        public async Task<string> ObtenerRestaurantes()
        {
            string vlcJsonRespuesta = string.Empty;
            string URLObtenerRestaurantes = URLWebApi + Controller + nameof(ObtenerRestaurantes);

            using (var HttpCliente = new HttpClient())
            {
                var vloHttpRespuesta = await HttpCliente.PostAsync(URLObtenerRestaurantes,
                            new StringContent("",
                            Encoding.UTF8, "application/json"));

                if (vloHttpRespuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    vlcJsonRespuesta = await vloHttpRespuesta.Content.ReadAsStringAsync();
                }
            }

            return vlcJsonRespuesta;
        }

        public async Task<bool> RegistrarReservacion(string JsonReservacion)
        {
            try
            {
                bool reservo = false;
                string URLObtenerRestaurantes = URLWebApi + ControllerReservacion + nameof(RegistrarReservacion);

                using (var HttpCliente = new HttpClient())
                {
                    var vloHttpRespuesta = await HttpCliente.PostAsync(URLObtenerRestaurantes,
                                new StringContent(JsonReservacion,
                                Encoding.UTF8, "application/json"));

                    if (vloHttpRespuesta.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        reservo = true;
                    }
                }

                return reservo;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> EliminarReservacion(string JsonReservacion)
        {
            try
            {
                bool reservo = false;
                string URLObtenerRestaurantes = URLWebApi + ControllerReservacion + nameof(EliminarReservacion);

                using (var HttpCliente = new HttpClient())
                {
                    var vloHttpRespuesta = await HttpCliente.PostAsync(URLObtenerRestaurantes,
                                new StringContent(JsonReservacion,
                                Encoding.UTF8, "application/json"));

                    if (vloHttpRespuesta.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        reservo = true;
                    }
                }

                return reservo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> ObtenerReservacionesUsuario(string JsonUsaurio)
        {
            string vlcJsonRespuesta = string.Empty;
            string URLObtenerRestaurantes = URLWebApi + ControllerReservacion + nameof(ObtenerReservacionesUsuario);

            using (var HttpCliente = new HttpClient())
            {
                var vloHttpRespuesta = await HttpCliente.PostAsync(URLObtenerRestaurantes,
                            new StringContent(JsonUsaurio,
                            Encoding.UTF8, "application/json"));

                if (vloHttpRespuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    vlcJsonRespuesta = await vloHttpRespuesta.Content.ReadAsStringAsync();
                }
            }

            return vlcJsonRespuesta;
        }

        public async Task<bool> ActualizarReservacion(string JsonUsaurio)
        {
            string vlcJsonRespuesta = string.Empty;
            string URLObtenerRestaurantes = URLWebApi + ControllerReservacion + nameof(ActualizarReservacion);

            using (var HttpCliente = new HttpClient())
            {
                var vloHttpRespuesta = await HttpCliente.PostAsync(URLObtenerRestaurantes,
                            new StringContent(JsonUsaurio,
                            Encoding.UTF8, "application/json"));

                if (vloHttpRespuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
            }

            return false;
        }


        public async Task<bool> RegistrarRestaurante(string JsonRestaurante)
        {
            try
            {
                bool reservo = false;
                string URLObtenerRestaurantes = URLWebApi + Controller + nameof(RegistrarRestaurante);

                using (var HttpCliente = new HttpClient())
                {
                    var vloHttpRespuesta = await HttpCliente.PostAsync(URLObtenerRestaurantes,
                                new StringContent(JsonRestaurante,
                                Encoding.UTF8, "application/json"));

                    if (vloHttpRespuesta.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        reservo = true;
                    }
                }

                return reservo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
