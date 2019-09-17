using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Restaurante.APP.Droid.Servicios;
using Restaurante.APP.Interfaces;

namespace Restaurante.APP.Droid.Servicios
{
    
    public class GaleriaServicio : IFotoPicker
    {
        public Task<Stream> ObtenerFotoGaleriaAsync()
        {
            
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.PutExtra(Intent.ExtraAllowMultiple, true);
            intent.SetAction(Intent.ActionGetContent);
            MainActivity.Instance.StartActivityForResult(Intent.CreateChooser(intent, "Seleccione una foto"), MainActivity.PickImageId);
            MainActivity.Instance.ImagenSeleccionada = new TaskCompletionSource<Stream>();
            return MainActivity.Instance.ImagenSeleccionada.Task;
        }
    }
}