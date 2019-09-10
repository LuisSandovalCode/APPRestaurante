using Restaurante.APP.ConfiguracionApp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.APP.ExternalServices
{
    public class SVUsuario
    {
        private static string URLWebApi { get; set; }
        private static string Controller { get; set; }

        public SVUsuario()
        {
            if (string.IsNullOrEmpty(URLWebApi))
                URLWebApi = AppSettingsManager.Settings["Service"];
            if(string.IsNullOrEmpty(Controller))
                Controller = AppSettingsManager.Settings["UsuarioController"];
        }

        #region [Inicio Sesion]
        /// <summary>
        /// Método Http Async, que permite poder realizar
        /// el inicio de sesión dentro de la aplicación
        /// </summary>
        /// <param name="pvcJsonInicioSesion"></param>
        /// <returns></returns>
        public async Task<string> LogearUsuario(string pvcJsonInicioSesion)
        {
            try
            {
                string vlcJsonRespuesta = string.Empty;
                string URLInicioSesion = URLWebApi + Controller + nameof(LogearUsuario);

                using (var vloClienteHttp = new HttpClient())
                {
                    var vloHttpRespuesta = await vloClienteHttp.PostAsync(URLInicioSesion,
                        new StringContent(pvcJsonInicioSesion,
                        Encoding.UTF8, "application/json"));

                    if (vloHttpRespuesta.StatusCode == System.Net.HttpStatusCode.OK)
                        vlcJsonRespuesta = await vloHttpRespuesta.Content.ReadAsStringAsync();
                }

                return vlcJsonRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion
    }
}
