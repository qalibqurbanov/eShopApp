namespace eShopApp.WebUI.Models
{
    /// <summary>
    /// Hazikirki sehife ile elaqeli melumatlari saxlayir.
    /// </summary>
    public class PageInfo
    {
        public int TotalProducts { get; set; }
        public int ProductsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int? CurrentCategoryID { get; set; }

        /// <summary>
        /// Bu property geriye olmali olan sehife sayini qaytarir.
        /// </summary>
        public int TotalPages =>
            (int)Math.Ceiling((decimal)TotalProducts / ProductsPerPage);
    }
}
