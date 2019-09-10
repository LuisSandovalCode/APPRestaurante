﻿using Xamarin.Forms;

namespace Restaurante.APP.ViewModel.Utilidades.UtilidadesUI
{
    public class UtilidadNavegacionUI
    {

        public void CrearMasterDetailPage(Page paginaMaster, Page paginaDetail)
        {
            //Pone al aplicativo en modo navegacion
            NavigationPage navigation = new NavigationPage(paginaDetail);
            //NavigationPage navigation = new NavigationPage(new MainPersonView());
            //Se direcciona
            App.Current.MainPage = new MasterDetailPage
            {
                Master = paginaMaster,
                Detail = navigation
            };
        }

        
        /// <summary>
        /// Aplica un push sobre el MasterDetailPage
        /// </summary>
        /// <param name="page">Object view redirect</param>
        public static void IrAView(Page page)
        {
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(page);
        }
        /// <summary>
        /// Aplica un pop sobre la pagina actual
        /// </summary>
        public static void VolverDeView()
        {
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PopAsync();//Se devuelve a la anterior
        }
    }
}
