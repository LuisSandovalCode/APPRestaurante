using Xamarin.Forms;
namespace Restaurante.APP.ViewModel.Utilidades.UtilidadesUI
{
    public class UtilidadNavegacionUI
    {

        public static void CrearMasterDetailPage(Page paginaMaster, Page paginaDetail)
        {
            //Pone al aplicativo en modo navegacion
            NavigationPage navigation = new NavigationPage(paginaDetail);
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
            ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
        }
        /// <summary>
        /// Aplica un pop sobre la pagina actual
        /// </summary>
        public static void VolverDeView()
        {
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PopAsync();//Se devuelve a la anterior
        }

        public static void CerrarSession(Page vloPage)
        {
            App.Current.MainPage = vloPage;
        }
    }
}
