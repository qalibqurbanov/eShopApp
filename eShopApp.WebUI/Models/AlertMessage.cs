namespace eShopApp.WebUI.Models
{
    /// <summary>
    /// Sehifede 'alert' class-na sahib HTML teqleri icerisinde gostereceyim mesaji temsil edir (ve ya bawqa sozle gostereceyim mesaj haqqinda melumatlari temsil edir).
    /// </summary>
    public class AlertMessage
    {
        /// <summary>
        /// Gostereceyim mesajin kontenti.
        /// </summary>
        public string alertMessage { get; set; }

        /// <summary>
        /// Gostereceyim mesajin arxa plan rengi.
        /// </summary>
        public AlertType alertType { get; set; }

        /// <summary>
        /// Icerisinde mesaj gostereceyim 'alert' class-na sahib HTML elementinin arxa plan rengini temsil edir.
        /// </summary>
        public enum AlertType : byte
        {
            /* https://getbootstrap.com/docs/5.2/components/alerts/ */
            success,
            danger,
            info
        }
    }
}