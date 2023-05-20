using System.ComponentModel.DataAnnotations;

namespace eShopApp.WebUI.Models.IdentityModels.Login
{
    /// <summary>
    /// Giriw meqsedile userin formu doldurub post etdiyi datalari temsil edir.
    /// </summary>
    public class SignInModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 12)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// 'Remember Me' secilidirse Autentifikasiya Cookie-sinin omru olacaq 'Startup.cs'-de 'ExpireTimeSpan' property-sine gosterdiyim muddet, yox eger 'Remember Me' secilmese Autentifikasiya Cookie-sinin omru olacaq brauzer baglanana kimi (bu tip Cookie-ye 'Session Cookie' deyilir). Chrome ve s. diger brauzerlerin nastroykasinda "Continue where you left off" funksionalligi secilidirse bu zaman hemin brauzer 'Session Cookie'-ni silmeyecek ve bu sebeble user appde autentifikasiyadan kecmiw halda qalmiw olacaq, halbuki 'Session Cookie'-si session sonlananda (yeni, brauzer baglananda) silinecek ve netice olaraq user appimizden logout olmuw olacaq idi, lakin brauzerin "Continue where you left off" funksionalligi sayesinde 'Session Cookie' iwlemeli oldugu kimi iwlemir. Sozu geden problem haqqinda daha etrafli: <see href="https://stackoverflow.com/questions/10617954/chrome-doesnt-delete-session-cookies">https://stackoverflow.com/questions/10617954/chrome-doesnt-delete-session-cookies</see>
        /// </summary>
        public bool RememberMe { get; set; }
    }
}