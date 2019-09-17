namespace Restaurante.APP.Model.Home
{
    public class HomeMenu
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public string IconoAndroid { get; set; }
        public string IconoIOS { get; set; }

        public HomeMenu()
        {
            Id = 0;
            Nombre = string.Empty;
            Detalle = string.Empty;
            IconoAndroid = string.Empty;
            IconoIOS = string.Empty;
        }
    }
}
