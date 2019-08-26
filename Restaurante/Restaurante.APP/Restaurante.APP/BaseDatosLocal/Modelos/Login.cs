using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.APP.BaseDatosLocal.Modelos
{
    public class Login : RealmObject
    {
        public string Correo { get; set; }

        public string Contrasena { get; set; }
    }
}
