namespace eShopApp.WebUI.Models.IdentityModels.Role
{
    /// <summary>
    /// Editlenmesi istenen role haqqinda melumatlari temsil edir.
    /// </summary>
    public class RoleEditModel
    {
        /// <summary>
        /// Editlenmiw rolun ID-sini temsil edir.
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// Editlenmiw rolun Name-ni temsil edir.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Hazirda editlenen rola elave edilecek User ID-lerini temsil edir.
        /// </summary>
        public int[] IDsToAddRole { get; set; }

        /// <summary>
        /// Hazirda editlenen roldan xaric edilecek User ID-lerini temsil edir.
        /// </summary>
        public int[] IDsToRemoveRole { get; set; }
    }
}