namespace eShopApp.Business.Validation.Abstract
{
    /// <summary>
    /// Biznes qatinda metodlarda validasiyani yoxlamaqdan mukellef olan metodlari saxlayan interfeys.
    /// </summary>
    public interface IValidator<TEntity>
    {
        /// <summary>
        /// Problem entity-nin neyinde(hansi propertysinde ve s.) oldugu ile elaqeli mesaji/mesajlari ozunde saxlayan property.
        /// </summary>
        string ErrorMessage { get; set; } /* Ehtiyac olsa - xeta barede daha detalli melumat saxlamaq ucun 'Dictionary' iwletmek olar */

        /// <summary>
        /// Parametr olaraq verilmiw entity-nin valid olub-olmadigini yoxlayir.
        /// </summary>
        /// <param name="entity">Dogrulugu yoxlanmali olan entity.</param>
        /// <returns>Geriye validasiya ugurludursa 'true', ugursuzdursa 'false' dondurulecek.</returns>
        bool Validate(TEntity entity);
    }
}