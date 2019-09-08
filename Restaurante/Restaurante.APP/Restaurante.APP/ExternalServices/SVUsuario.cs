﻿using Restaurante.APP.ConfiguracionApp;
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
        public async Task<string> IniciarSesion(string pvcJsonInicioSesion)
        {
            try
            {
                string vlcJsonRespuesta = string.Empty;
                string URLInicioSesion = URLWebApi + Controller + nameof(IniciarSesion);

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

        #region [Registrar Usuario]
        /// <summary>
        /// Método Http Async, que permita registrar un usuario 
        /// dentro de la aplicación
        /// </summary>
        /// <param name="JsonUsuario"></param>
        /// <returns></returns>
        public async Task<bool> RegistrarUsuario(string JsonUsuario)
        {
            string URLRegistrarUsuario = URLWebApi + Controller + nameof(RegistrarUsuario);
            bool UsuarioRegistrado = false;
            try
            {
                using (var ClienteHttp = new HttpClient())
                {
                    var vloRespuesta = await ClienteHttp.PostAsync(URLRegistrarUsuario,
                        new StringContent(JsonUsuario,Encoding.UTF8,"application/json"));

                    if(vloRespuesta.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string JsonRespuesta = await vloRespuesta.Content.ReadAsStringAsync();
                        UsuarioRegistrado = Convert.ToBoolean(JsonRespuesta);
                    }

                    if (vloRespuesta.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        throw new Exception("Servicio no disponible, intentelo más tarde");
                }

                return UsuarioRegistrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion
    }
}
