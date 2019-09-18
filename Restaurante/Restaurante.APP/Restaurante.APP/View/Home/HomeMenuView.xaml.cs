﻿
using Restaurante.APP.ViewModel.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurante.APP.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeMenuView : ContentPage
    {
        private static HomeMenuView Instancia = null;

        public HomeMenuView()
        {
            InitializeComponent();
            //BindingContext = new HomeMenuViewModel();
            BindingContext = HomeMenuViewModel.ObtenerInstancia();
        }
    }
}