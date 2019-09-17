using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using System.IO;
using Android.Content;
using Restaurante.APP.Droid.Servicios;

namespace Restaurante.APP.Droid
{
    [Activity(Label = "Restaurante.APP", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static readonly int PickImageId = 1000;
        public static MainActivity Instance { get; private set; }
        public TaskCompletionSource<Stream> ImagenSeleccionada { get; set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Forms.DependencyService.Register<GaleriaServicio>();
            Instance = this;
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent IntentoResultado)
        {
            base.OnActivityResult(requestCode, resultCode, IntentoResultado);

            if(requestCode.Equals(PickImageId))
            {
                if(resultCode == Result.Ok && IntentoResultado != null)
                {
                    Android.Net.Uri vloUriImagen = IntentoResultado.Data;

                    Stream vloStreamImagen = ContentResolver.OpenInputStream(vloUriImagen);

                    ImagenSeleccionada.SetResult(vloStreamImagen);
                }
                else
                {
                    ImagenSeleccionada.SetResult(null);
                }
            }
        }
    }
}