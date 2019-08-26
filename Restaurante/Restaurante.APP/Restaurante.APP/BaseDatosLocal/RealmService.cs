using Realms;
using Restaurante.APP.BaseDatosLocal.Modelos;
using Restaurante.APP.Model.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.APP.BaseDatosLocal
{
    public class RealmService
    {
        public Realm _Realm { get; set; }

        public RealmService()
        {
            _Realm = Realm.GetInstance();
        }


        #region [Recordar Credenciales]
        /// <summary>
        /// Método que permite recordar las credenciales desde Realm
        /// </summary>
        /// <returns></returns>
        public LoginModel RecordarCredenciales()
        {
            try
            {
                LoginModel Login = new LoginModel();

                var CredencialesUsuario = _Realm.All<Login>().ToList();

                if (CredencialesUsuario.Any())
                {
                    var Crendeciales = CredencialesUsuario[0];

                    Login = null;
                    Login = new LoginModel()
                    {
                        Correo = Crendeciales.Correo,
                        Contrasena = Crendeciales.Contrasena
                    };
                }

                return Login;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion

        #region [Guardar Credenciales]
        /// <summary>
        /// Método que permite almacenar la credenciales con Realm
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool GuardarCredenciales(LoginModel usuario)
        {
            try
            {
                bool RegistroUsuario = false;
                var CredencialesUsuario = _Realm.All<Login>().ToList();
                var vlbEsExistenCrendenciales = CredencialesUsuario
                    .FirstOrDefault(x => x.Correo.Equals(usuario.Correo) && x.Contrasena.Equals(usuario.Contrasena)) != null;

                if (!vlbEsExistenCrendenciales)
                {
                    using (var trans = _Realm.BeginWrite())
                    {
                        Login NuevoLogin = new Login()
                        {
                            Correo = usuario.Correo,
                            Contrasena = usuario.Contrasena
                        };

                        _Realm.Add<Login>(NuevoLogin);
                        trans.Commit();
                    }

                    RegistroUsuario = true;
                }
                else
                {
                    using (var trans = _Realm.BeginWrite())
                    {
                        _Realm.RemoveAll<Login>();
                        trans.Commit();
                    }

                    using (var trans = _Realm.BeginWrite())
                    {
                        Login NuevoLogin = new Login()
                        {
                            Correo = usuario.Correo,
                            Contrasena = usuario.Contrasena
                        };

                        _Realm.Add<Login>(NuevoLogin);
                        trans.Commit();
                    }
                }
                return RegistroUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion
    }
}
