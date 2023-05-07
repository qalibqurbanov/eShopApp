using System.IO;
using eShopApp.DataAccess.DatabaseContext;
using Microsoft.Extensions.Hosting;

namespace eShopApp.WebUI.Extensions.Helpers
{
    /// <summary>
    /// Fayllarla iwlemek ile elaqeli extension metodlari saxlayir.
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        /// Form-a yuklenen wekli 'Web Root (wwwroot)' papkasina yerlewdirir.
        /// </summary>
        /// <param name="imageFile">Form-dan gelen wekil.</param>
        /// <param name="WebRootPath">'Web Root (wwwroot)' papkasinin yolu.</param>
        /// <param name="imageName">Metoddan kenara gonderilecek wekil adi (+ Daha sonra bu weklin adini hemin bu metoddan kenarda yaxalayaraq DB-ya qeyd edecem).</param>
        public static void MoveFormFile(this IFormFile imageFile, string WebRootPath, out string imageName)
        {
            /* Faylin ozunu yerlewdirirem 'Web Root (wwwroot)' papkasina: */
            string targetPath = Path.Combine(WebRootPath, "img\\ProductImages");

            /* Ilk once metoddan cole gondereceyimiz wekil adini temizleyirem: */
            imageName = string.Empty;

            /* Eyni adli wekil hedef pathde movcuddursa, weklin adini editleyecem: */
            if (File.Exists(Path.Combine(targetPath, imageFile.FileName)))
            {
                /* {FaylinUzantisizEslAdi}-_-image{RandomReqem}{HazirkiTick}{FaylinUzantisi} */
                imageName = $"{imageFile.FileName.Remove(imageFile.FileName.LastIndexOf('.'))}-_-image{new Random().Next(100, int.MaxValue)}{DateTime.Now.Ticks}{Path.GetExtension(imageFile.FileName)}";
            }
            else
            {
                /* {FaylinEslAdiUzantisiIleBirlikde} */
                imageName = imageFile.FileName;
            }

            targetPath = Path.Combine(targetPath, imageName);

            using (FileStream FS = new FileStream(targetPath, FileMode.Create))
            {
                imageFile.CopyTo(FS);
            }
        }
    }
}