namespace eShopApp.Entity.Enums
{
    /// <summary>
    /// Sifariwin hazirki veziyyetini temsil edir.
    /// </summary>
    public enum OrderState : byte
    {
        Waiting    = 0,
        UnPaid     = 1,
        Paid       = 2,
        Processing = 3,
        Completed  = 4
    }
}