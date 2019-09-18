using Restaurante.APP.Model.Restaurante;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurante.APP.Model.Restaurante
{
    public class Reservacion : RestauranteModel
    {
        public int IdReservacion { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaReservacion { get; set; }
    }

}
