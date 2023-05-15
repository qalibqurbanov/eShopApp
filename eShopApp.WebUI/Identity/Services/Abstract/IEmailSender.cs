namespace eShopApp.WebUI.Identity.Services.Abstract
{
    /// <summary>
    /// Mail gonderme funksionalliginin imzasini saxlayacaq.
    /// </summary>
    public interface IEmailSender
    {
        Task SendEmailAsync(string UserEmailAddress, string MailSubject, string MessageContent);
    }
}