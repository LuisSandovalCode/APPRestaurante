using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Restaurante.APP.Model.Restaurante
{
    public class RestauranteModel
    {

        public string CedulaUsuario { get; set; }

        public int Id { get; set; }

        public ImageSource Imagen { get; set; }

        public string Nombre { get; set; }

        public string Pais { get; set; }

        public string Ciudad { get; set; }

        public DateTime FechaIngreso { get; set; }
    }
}
