using Xamarin.Forms;

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
    }
}
