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

        public SVRestaurante()
        {
            if (string.IsNullOrEmpty(URLWebApi))
                URLWebApi = AppSettingsManager.Settings["Service"];
            if (string.IsNullOrEmpty(Controller))
                Controller = AppSettingsManager.Settings["RestauranteController"];
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

        public async Task<bool> RealizarReseracion(string JsonReservacion)
        {
            try
            {
                bool reservo = false;
                string URLObtenerRestaurantes = URLWebApi + Controller + nameof(RealizarReseracion);

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
    }
}
