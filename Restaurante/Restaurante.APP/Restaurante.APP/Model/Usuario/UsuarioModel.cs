using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.APP.Model.Usuario
{
    public class UsuarioModel
    {
        public int ID { get; set; }

        public string Cedula { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Apellido2 { get; set; }

        public string Correo { get; set; }

        public int Telefono { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Contrasena { get; set; }

        public string FotoPerfil { get; set; }
    }
}
