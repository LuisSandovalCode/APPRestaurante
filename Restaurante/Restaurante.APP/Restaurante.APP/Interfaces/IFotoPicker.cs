using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Restaurante.APP.Interfaces
{
    public interface IFotoPicker
    {
        Task<Stream> ObtenerFotoGaleriaAsync();

    }
}
