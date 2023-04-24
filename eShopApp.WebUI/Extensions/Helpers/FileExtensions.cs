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

            imageName = string.Empty;

            if (File.Exists(Path.Combine(targetPath, imageFile.FileName)))
            {
                imageName = $"{imageName}-{new Random().Next(100, 1000000)}{DateTime.Now.Ticks}";
            }
            else
            {
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