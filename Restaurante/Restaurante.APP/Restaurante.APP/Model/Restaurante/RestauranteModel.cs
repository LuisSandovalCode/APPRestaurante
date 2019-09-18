using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Restaurante.APP.Model.Restaurante
{
    public class RestauranteModel
    {

        public int IdRestaurante { get; set; }

        public string Nombre { get; set; }

        public string Pais { get; set; }

        public string Ciudad { get; set; }

        public int CodigoPostal { get; set; }

        public decimal Longitud { get; set; }

        public decimal Latitud { get; set; }

        public string FotoRestaurante { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
