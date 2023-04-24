namespace eShopApp.WebUI.Models
{
    /// <summary>
    /// Sehifede alert sinifli teq icerisinde gostereceyim mesaj melumatlarini temsil edir.
    /// </summary>
    public class AlertMessage
    {
        public string    alertMessage { get; set; }
        public AlertType alertType    { get; set; }

        public enum AlertType : byte
        {
            /* https://getbootstrap.com/docs/5.2/components/alerts/ */
            success,
            danger,
            info
        }
    }
}
