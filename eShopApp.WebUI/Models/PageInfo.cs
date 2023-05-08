namespace eShopApp.WebUI.Models
{
    /// <summary>
    /// Hazikirki sehife ile elaqeli melumatlari temsil edir.
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// Sehifede gostereceyimiz mehsul sayini saxlayir.
        /// </summary>
        public int TotalProducts { get; set; }

        /// <summary>
        /// Her sehifede nece eded mehsulun gosterileceyi sayini saxlayir.
        /// </summary>
        public int ProductsPerPage { get; set; }

        /// <summary>
        /// Hazirki sehifenin nomresini saxlayir.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Hazirda hansi kateqoriya icerisinde oldugumuz ile elaqeli melumati saxlayir.
        /// </summary>
        public int? CurrentCategoryID { get; set; }

        /// <summary>
        /// Bu property geriye nece dene sehife olmalidirsa hesablayaraq qaytarir.
        /// </summary>
        public int TotalPages => (int)Math.Ceiling((decimal)TotalProducts / ProductsPerPage);
        /*
            return : UmumiMehsulSayi / SehifeBawinaOlmaliOlanMehsulSayi (Men her sehifede '3' eded mehsulun gorsenmeyini isteyirem)
            Numune : 50 / 3 = 16.666 : YuxariYuvarlaqlawdir(16.666) = 17, yeni cemi '17' sehifemiz olmalidir eger '50' eded mehsulumuz varsa, bawqa sozle '50' mehsulu sehifeleye bilmeyimiz ucun cemi '17' sehifeye ehtiyacimiz var.
        */
    }
}