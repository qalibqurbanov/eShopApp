using AutoMapper;
using eShopApp.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using eShopApp.WebUI.Identity.Entities;
using eShopApp.WebUI.Models.IdentityModels;
using eShopApp.WebUI.Extensions.Serialization;
using eShopApp.WebUI.Identity.Services.Abstract;

namespace eShopApp.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager; /* Userleri idare etmeyimize yarayan sinifdir, bawqa sozle: User yaratmaq/yenilemek/silmek, useri mueyyen bir rola elave etmek/silmek, usere bir Claim elave etmek/silmek, wifre deyiwdirmek, @mail tesdiqleme ve s. kimi user emeliyyatlarini yerine yetirir. */
        private readonly SignInManager<AppUser> _signInManager; /* Userin giriw ve cixiwini kontrol eden sinifdir, bawqa sozle: User giriw/cixiw/TwoFactorAuthentication kimi emeliyyatlari yerine yetirir. */
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IMapper mapper,
            IEmailSender emailSender)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
            this._emailSender = emailSender;
        }

        #region Identity ile elaqeli reallawdirilmiw emeliyyatlar barede usere sehifenin ust hissesinde gostereceyimiz mesaji set eden komekci mini metod.
        private void CreateInformationalMessage(string AlertMessage, AlertMessage.AlertType AlertType)
        {
            TempData.Set<AlertMessage>("InformationalMessage", new AlertMessage()
            {
                alertMessage = AlertMessage,
                alertType = AlertType
            });
        }
        #endregion Identity ile elaqeli reallawdirilmiw emeliyyatlar barede usere sehifenin ust hissesinde gostereceyimiz mesaji set eden komekci mini metod.

        [HttpGet]
        public IActionResult SignIn()
        {
            /* Query String-den keyi bu cur yaxalamaq yerine '[HttpGet] SignIn' metodunun parametrinde Model Binding (ve [FromQuery]) komeyile yaxalatdirada bilerdim: */
            if (Request.Query.ContainsKey("returnUrl"))
            {
                TempData["QueryStringKey"] = Request.Query["returnUrl"][0];
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn([FromForm] SignInModel model)
        {
            if (!ModelState.IsValid)
            {
                /* User kecersiz datalar daxil edibse, useri yeniden qaytaririq oldugu sehifeye ve qarwisinda qutular yanliw daxil etmiw oldugu melumatlarla dolu olsun (ki, daxil etmiw oldugu datalari gozden kecirib editlesin ve yeniden post etsin formu): */
                return View(model);
            }

            /* Daxil edilmiw mail-i yoxlayiriq - umumiyyetle bele bir maile sahib user var?: */
            AppUser appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser == null)
            {
                ModelState.AddModelError("", $"\"{model.Email}\" email adresine sahib user tapilmadi.");
                return View(model);
            }

            /* User email adresini tesdiqlemeyibse useri profiline giriw etdirmirik, qaytaririq 'SignIn' sehifesine: */
            if (!await _userManager.IsEmailConfirmedAsync(appUser))
            {
                ModelState.AddModelError("", $"Zehmet olmasa \"{model.Email}\" email adresinize daxil olaraq gondermiw oldugumuz mesaj vasitesile email adresinizi tesdiqleyin.");

                /* Useri daxil etmiw oldugu melumatlar ile birlikde qaytaririq 'SignIn' sehifesine: */
                return View(model);
            }

            /* Bura iwleyirse demeli user mailini tesdiqleyib ve belece hemin bu giriw melumatlarini daxil etmiw userden elde etdiyimiz melumatlar esasinda userimizi profiline giriw etdiririk: */
            var result = await _signInManager.PasswordSignInAsync(appUser, model.Password, model.RememberMe, false);
            if (result.Succeeded) /* Useri profiline ugurla giriw etdirdikse */
            {
                if (TempData["QueryStringKey"] != null) /* Query String-den 'ReturnUrl' yaxalanibsa, useri yonlendiririk yaxalanmiw hemin URL-e */
                {
                    return Redirect(TempData["QueryStringKey"].ToString());
                }
                else /* Eks halda useri yonlendiririk ana sehifeye */
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            /* Userin profiline giriwi her hansi bir sebeble ugursuz neticelense (ModelState-e yeni xeta mesaji elave edirik ve useri qaytaririq yeniden 'SignIn' sehifesine): */
            /* ModelState-e yeni bir error mesaji elave edirik: */
            ModelState.AddModelError("", "Daxil etdiyiniz Email ya da Password yanliwdir.");
            /* Userin post etdiyi form-da nese bir problem varsa, daxil etmiw oldugu hemin datalar ile birlikde qaytaririq hemin bu signin sehifesine: */
            return View(model);
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([FromForm] SignUpModel model, [FromQuery] string ReturnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                /* User kecersiz datalar daxil edibse, useri yeniden qaytaririq oldugu sehifeye ve qarwisinda qutular yanliw daxil etmiw oldugu melumatlarla dolu olsun (ki, daxil etmiw oldugu datalari gozden kecirib editlesin ve yeniden post etsin formu): */
                return View(model);
            }

            /* Userden elde etdiyimiz melumatlari 'AppUser' modeline gonderirik: */
            AppUser appUser = _mapper.Map<AppUser>(model);

            /* Ikinci parametrde gosterdiyimiz Password-a sahib yeni bir user yaradiriq: */
            IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);

            /* User ugurla yaradildisa: */
            if (result.Succeeded)
            {
                /* Email adresini gonderdiyimiz mail tesdiqleme URL-ni hazirlayaq: */
                string ConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                string ConfirmationURL = Url.Action("ConfirmEmail", "Account", new
                {
                    /* Generasiya edilmiw olan "~/Account/ConfirmEmail" linkine Query String olaraq awagidaki Key-ler (ve '=Value'-lari olaraq) yerlewdirilecek: */
                    UserID = appUser.Id,
                    Token = ConfirmationToken
                });

                /* Userin email adresini mailini tesdiqlemesi ucun mesaj gonderek: */
                string FullConfirmationURL = $"http://localhost:5018{ConfirmationURL}";
                await _emailSender.SendEmailAsync
                (
                    UserEmailAddress: model.Email,
                    MailSubject: "Zehmet olmasa email adresinizi tesdiqleyin.",
                    MessageContent:
                    $$"""
                    <!DOCTYPE html>
                    <html>
                        <head>
                            <meta charset="utf-8">
                            <meta http-equiv="x-ua-compatible" content="ie=edge">
                            <title>Email Confirmation</title>
                            <meta name="viewport" content="width=device-width, initial-scale=1">
                            <style type="text/css">
                                /**
                                * Google webfonts. Recommended to include the .woff version for cross-client compatibility.
                                */
                                @media screen {
                                @font-face {
                                font-family: 'Source Sans Pro';
                                font-style: normal;
                                font-weight: 400;
                                src: local('Source Sans Pro Regular'), local('SourceSansPro-Regular'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/ODelI1aHBYDBqgeIAH2zlBM0YzuT7MdOe03otPbuUS0.woff) format('woff');
                                }
                                @font-face {
                                font-family: 'Source Sans Pro';
                                font-style: normal;
                                font-weight: 700;
                                src: local('Source Sans Pro Bold'), local('SourceSansPro-Bold'), url(https://fonts.gstatic.com/s/sourcesanspro/v10/toadOcfmlt9b38dHJxOBGFkQc6VGVFSmCnC_l7QZG60.woff) format('woff');
                                }
                                }
                                /**
                                * Avoid browser level font resizing.
                                * 1. Windows Mobile
                                * 2. iOS / OSX
                                */
                                body,
                                table,
                                td,
                                a {
                                -ms-text-size-adjust: 100%; /* 1 */
                                -webkit-text-size-adjust: 100%; /* 2 */
                                }
                                /**
                                * Remove extra space added to tables and cells in Outlook.
                                */
                                table,
                                td {
                                mso-table-rspace: 0pt;
                                mso-table-lspace: 0pt;
                                }
                                /**
                                * Better fluid images in Internet Explorer.
                                */
                                img {
                                -ms-interpolation-mode: bicubic;
                                }
                                /**
                                * Remove blue links for iOS devices.
                                */
                                a[x-apple-data-detectors] {
                                font-family: inherit !important;
                                font-size: inherit !important;
                                font-weight: inherit !important;
                                line-height: inherit !important;
                                color: inherit !important;
                                text-decoration: none !important;
                                }
                                /**
                                * Fix centering issues in Android 4.4.
                                */
                                div[style*="margin: 16px 0;"] {
                                margin: 0 !important;
                                }
                                body {
                                width: 100% !important;
                                height: 100% !important;
                                padding: 0 !important;
                                margin: 0 !important;
                                }
                                /**
                                * Collapse table borders to avoid space between cells.
                                */
                                table {
                                border-collapse: collapse !important;
                                }
                                a {
                                color: #1a82e2;
                                }
                                img {
                                height: auto;
                                line-height: 100%;
                                text-decoration: none;
                                border: 0;
                                outline: none;
                                }
                            </style>
                        </head>
                        <body style="background-color: #e9ecef;">
                            <!-- start preheader -->
                            <div class="preheader" style="display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;">
                                A preheader is the short summary text that follows the subject line when an email is viewed in the inbox.
                            </div>
                            <!-- end preheader -->
                            <!-- start body -->
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <!-- start logo -->
                                <tr>
                                    <td align="center" bgcolor="#e9ecef">
                                        <!--[if (gte mso 9)|(IE)]>
                                        <table align="center" border="0" cellpadding="0" cellspacing="0" width="600">
                                            <tr>
                                                <td align="center" valign="top" width="600">
                                                    <![endif]-->
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="max-width: 600px;">
                                                        <tr>
                                                            <td align="center" valign="top" style="padding: 36px 24px;">
                                                                <a href="https://www.github.com/qalibqurbanov" target="_blank" style="display: inline-block;">
                                                                <img src="~\img\ProductImages\DEFAULT.png" alt="Logo" border="0" width="48" style="display: block; width: 48px; max-width: 48px; min-width: 48px;"> 
                                                                </a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <!--[if (gte mso 9)|(IE)]>
                                                </td>
                                            </tr>
                                        </table>
                                        <![endif]-->
                                    </td>
                                </tr>
                                <!-- end logo -->
                                <!-- start hero -->
                                <tr>
                                    <td align="center" bgcolor="#e9ecef">
                                        <!--[if (gte mso 9)|(IE)]>
                                        <table align="center" border="0" cellpadding="0" cellspacing="0" width="600">
                                            <tr>
                                                <td align="center" valign="top" width="600">
                                                    <![endif]-->
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="max-width: 600px;">
                                                        <tr>
                                                            <td align="left" bgcolor="#ffffff" style="padding: 36px 24px 0; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; border-top: 3px solid #d4dadf;">
                                                                <h1 style="margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;">Confirm Your Email Address</h1>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <!--[if (gte mso 9)|(IE)]>
                                                </td>
                                            </tr>
                                        </table>
                                        <![endif]-->
                                    </td>
                                </tr>
                                <!-- end hero -->
                                <!-- start copy block -->
                                <tr>
                                    <td align="center" bgcolor="#e9ecef">
                                        <!--[if (gte mso 9)|(IE)]>
                                        <table align="center" border="0" cellpadding="0" cellspacing="0" width="600">
                                            <tr>
                                                <td align="center" valign="top" width="600">
                                                    <![endif]-->
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="max-width: 600px;">
                                                        <!-- start copy -->
                                                        <tr>
                                                            <td align="left" bgcolor="#ffffff" style="padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;">
                                                                <p style="margin: 0;">Tap the button below to confirm your email address. If you didn't create an account with <a href="https://github.com/qalibqurbanov">eShopApp</a>, you can safely delete this email.</p>
                                                            </td>
                                                        </tr>
                                                        <!-- end copy -->
                                                        <!-- start button -->
                                                        <tr>
                                                            <td align="left" bgcolor="#ffffff">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td align="center" bgcolor="#ffffff" style="padding: 12px;">
                                                                            <table border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td align="center" bgcolor="#1a82e2" style="border-radius: 6px;">
                                                                                        <a href="{{FullConfirmationURL}}" target="_blank" style="display: inline-block; padding: 16px 36px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;">Verify</a>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <!-- end button -->
                                                        <!-- start copy -->
                                                        <tr>
                                                            <td align="left" bgcolor="#ffffff" style="padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;">
                                                                <p style="margin: 0;">If that doesn't work, copy and paste the following link in your browser:</p>
                                                                <p style="margin: 0;"><a href="{{FullConfirmationURL}}" target="_blank">{{FullConfirmationURL}}</a></p>
                                                            </td>
                                                        </tr>
                                                        <!-- end copy -->
                                                        <!-- start copy -->
                                                        <tr>
                                                            <td align="left" bgcolor="#ffffff" style="padding: 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px; border-bottom: 3px solid #d4dadf">
                                                                <p style="margin: 0;">Cheers,<br> eShopApp | Qalib Qurbanov</p>
                                                            </td>
                                                        </tr>
                                                        <!-- end copy -->
                                                    </table>
                                                    <!--[if (gte mso 9)|(IE)]>
                                                </td>
                                            </tr>
                                        </table>
                                        <![endif]-->
                                    </td>
                                </tr>
                                <!-- end copy block -->
                                <!-- start footer -->
                                <tr>
                                    <td align="center" bgcolor="#e9ecef" style="padding: 24px;">
                                        <!--[if (gte mso 9)|(IE)]>
                                        <table align="center" border="0" cellpadding="0" cellspacing="0" width="600">
                                            <tr>
                                                <td align="center" valign="top" width="600">
                                                    <![endif]-->
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="max-width: 600px;">
                                                        <!-- start permission -->
                                                        <tr>
                                                            <td align="center" bgcolor="#e9ecef" style="padding: 12px 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;">
                                                                <p style="margin: 0;">You received this email because we received a request for email activation for your account. If you didn't request email activation you can safely delete this email.</p>
                                                            </td>
                                                        </tr>
                                                        <!-- end permission -->
                                                        <!-- start unsubscribe -->
                                                        <!-- <tr>-->
                                                        <!--    <td align="center" bgcolor="#e9ecef" style="padding: 12px 24px; font-family: 'Source Sans Pro', Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;">-->
                                                        <!--      <p style="margin: 0;">To stop receiving these emails, you can <a href="https://www.blogdesire.com" target="_blank">unsubscribe</a> at any time.</p>-->
                                                        <!--      <p style="margin: 0;">Paste 1234 S. Broadway St. City, State 12345</p>-->
                                                        <!--    </td>-->
                                                        <!-- </tr>-->
                                                        <!-- end unsubscribe -->
                                                    </table>
                                                    <!--[if (gte mso 9)|(IE)]>
                                                </td>
                                            </tr>
                                        </table>
                                        <![endif]-->
                                    </td>
                                </tr>
                                <!-- end footer -->
                            </table>
                            <!-- end body -->
                        </body>
                    </html>
                    """
                );

                /* Useri yonlendirek lazimi sehifeye: */
                if (ReturnUrl != null) /* Query String-den 'ReturnUrl' yaxalanibsa, useri yonlendiririk yaxalanmiw hemin URL-e */
                {
                    return Redirect(ReturnUrl);
                }
                else /* Eks halda useri yonlendiririk ana sehifeye */
                {
                    CreateInformationalMessage
                    (
                        AlertMessage: "Zehmet olmasa emailinize kecid ederek gonderdiyimiz mesaj vasitesile mailinizi tesdiqleyin.",
                        AlertType: AlertMessage.AlertType.info
                    );
                    return RedirectToAction(nameof(SignIn));
                }
            }

            /* User her hansi bir sebeble yaradila bilmese, useri daxil etmiw oldugu hemin datalar ile birlikde qaytaririq 'SignUp' sehifesine: */
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery] int UserID, [FromQuery] string Token)
        /* Bu action, user mailine gelmiw linke kliklenende iwleyecek ve hemin linkdeki Query String parametrleri avtomatik bind olunacaq buradaki uygun parametrlere */
        {
            /* Burada aktivlewdirme linkinin butovluyunu/tamligini yoxlayiriq: */
            if (string.IsNullOrEmpty(UserID.ToString()) || string.IsNullOrEmpty(Token))
            {
                /* Wert true olsa demeli aktivlewdirme linkinin butovluyunde/tamliginda problem var. Bu sebeble ekrana baw vermiw xetayla bagli uygun mesaji cixaririq: */
                CreateInformationalMessage
                (
                    AlertMessage: "Aktivlewdirme linki kecersizdir!",
                    AlertType: AlertMessage.AlertType.danger
                );
                return View();
            }

            /* Maile gonderdiyimiz URL-den yaxaladigimiz UserID-ye sahib Useri elde edirik (ve ya bawqa sozle mailini tesdiqleyeceyimiz useri tapiriq burada): */
            AppUser appUser = await _userManager.FindByIdAsync(UserID.ToString());
            if (appUser != null)
            {
                /* User varsa mailini aktivlewdiririk (bunun ucun maildeki hemin URL dogru UserID-den elave hemde hemin User ucun generasiya edilmiw dogru Token-e sahib olmaldiir ki, tesdiqlenme ugurla neticelensin): */
                IdentityResult result = await _userManager.ConfirmEmailAsync(appUser, Token);
                if (result.Succeeded) /* Userin maili ugurla tesdiqlense */
                {
                    /* Mailin ugurla tesdiqlendiyi baresinde mesaj gosteririk: */
                    CreateInformationalMessage
                    (
                        AlertMessage: "Email adresiniz ugurla tesdiqlendi.",
                        AlertType: AlertMessage.AlertType.success
                    );
                    return View();
                }
            }

            /* Yuxaridaki hec bir if/return iwlemese demeli ki, mail tesdiqleme zamani nese bir xeta baw verib: */
            CreateInformationalMessage
            (
                AlertMessage: "Gozlenilmez bir xeta sebebile mail aktivlewdirile bilinmedi...",
                AlertType: AlertMessage.AlertType.danger
            );

            return View();
        }

        [HttpGet]
        public IActionResult Reset()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reset(ResetModel model)
        /* User mailini yazaraq post edecek ve bizde burada hemin maili model komeyile yaxalayiriq (sade bir string ile de yaxalaya bilerdim) */
        {
            /* ModelState valid deyilse demeli user mailini dogru daxil etmeyib, bu sebeble useri qaytaririq 'Reset'(yeni, password resetleme meqsedile mail yazma) sehifesine: */
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            /* Axtaririq ki userin teleb etdiyi mail adresine sahib user var? varsa hemin useri elde edirik DB-dan: */
            AppUser appUser = await _userManager.FindByEmailAsync(model.Email);

            if (appUser != null) /* Daxil edilen Mail DB-da movcuddursa */
            {
                /* Userden elde etdiyimiz mail DB-da varsa User ucun yeni bir Token generate edirik. Hemin bu Tokeni, userin passwordunu resetleye bilmesi ucun generate edeceyimiz linke yerlewdireceyik: */
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                /* Uesre passwordunu resetleye bilmesi ucun bir link yaradiriq. Bu linki gondereceyik userin daxil etmiw oldugu mailine: */
                string URL = Url.Action("ResetPassword", "Account", new { UserID = appUser.Id, Token = resetToken }); /* 3-cu parametrde oturduyumuz parametrler Query String-e yerlewdirilecek */

                /* Userin daxil etmiw oldugu mailine password resetleme mesaji gonderirik: */
                await _emailSender.SendEmailAsync
                (
                    UserEmailAddress: model.Email,
                    MailSubject: "Wifre yenileme telebi.",
                    MessageContent: $"Yeni sifre ucun <a target=\"_blank\" href=\"http://localhost:5018{URL}\">bura</a> tikla."
                );

                /* Ekrana mesaj cixararaq useri melumatlandiririq ki mailine kecid ederek onuncun generate etmiw oldugumuz linke kecid ederek yeni passwordunu set etsin: */
                CreateInformationalMessage
                (
                    AlertMessage: "Zehmet olmasa emailinize kecid ederek gonderdiyimiz mesaj vasitesile passwordunuzu berpa edin.",
                    AlertType: AlertMessage.AlertType.info
                );
            }

            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword([FromQuery] int UserID, [FromQuery] string Token)
        /* Bu action maile gelen password berpa linkine kliklenende iwleyecek. Burada password resetleme linki icerisindeki Query Stringden 'UserID' ve 'Token'-i yaxalayacayiq (ki, bilek hansi Useri hansi Token komeyile passwordunu yeniledek). */
        {
            /* Burada password resetleme linkinin butovluyunu/tamligini yoxlayiriq: */
            if(string.IsNullOrEmpty(UserID.ToString()) || string.IsNullOrEmpty(Token))
            {
                /* Wert true olsa demeli password berpa linkinin butovluyunde/tamliginda problem var. Bu sebeble ekrana baw vermiw xetayla bagli uygun mesaji cixaririq: */
                CreateInformationalMessage
                (
                    AlertMessage: "Password berpa linki kecersizdir!",
                    AlertType: AlertMessage.AlertType.danger
                );
                return View();
            }

            /* Password resetleme linkinin Query Stringinden lazimi parametrleri yaxalayaraq TempData-da saxlayiram 'ResetPassword'-un POST variantina gondermek ucun. Burada hidden input komeyilede dawiya bilerdim, lakin TempData ile dawiyacam hazirda: */
            TempData["passResetToken"] = Token;
            TempData["userId"] = UserID;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        /* User formda yeni passwordlarini yazib formu post eden zaman iwleyecek bu action */
        {
            /* User yeni passwordunu qutulara xetali(password qutulari uyuwmursa bir-birile, kod qisadirsa/uzundursa ve s.) daxil edibse, useri qaytaririq geriye yeni password set etme sehifesine ki passwordunu yeniden dogru bir wekilde set etsin: */
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            /* Passwordunu yenileyeceyim useri hemin bu 'uID' komeyile tapacam: */
            int uID = int.Parse(TempData["userId"].ToString());
            /* Verdiyimiz ID-ye sahib useri elde edirik: */
            AppUser appUser = await _userManager.FindByIdAsync(uID.ToString());

            /* Passwordunu yenilemek istediyimiz user DB-da tapilmasa usere bu haqqda mesaj gosterek ve yonlendirek 'Reset'(yeni, password resetleme meqsedile mail yazma) sehifesine */
            if (appUser == null)
            {
                CreateInformationalMessage
                (
                    AlertMessage: "Axtarilan user tapilmadi.",
                    AlertType: AlertMessage.AlertType.danger
                );   
                return RedirectToAction(nameof(Reset), nameof(AccountController));
            }

            /* (Bura iwleyirse demeli user tapilib, emeliyyata davam edirik) Userin passwordunu ugurla set ede bilmeyimiz ucun ehtiyacimiz olan Tokeni elde edirik: */
            string token = TempData["passResetToken"].ToString();
            /* Userin passwordunu yenileyirik: */
            IdentityResult Result = await _userManager.ResetPasswordAsync(appUser, token, model.Password);

            /* Userin passwordu ugurla yenilense bu barede usere mesaj gosteririk: */
            if(Result.Succeeded)
            {
                CreateInformationalMessage
                (
                    AlertMessage: "Passwordunuz ugurla yenilendi.",
                    AlertType: AlertMessage.AlertType.success
                );   
                return RedirectToAction(nameof(SignIn), nameof(AccountController));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            /* Autentifikasiya Cookie-sini brauzerden sildirmekle useri profilinde logout etdiririk: */
            await _signInManager.SignOutAsync();

            /* Useri profilinden cixardandan sonra useri yonlendirek ana sehifeye: */
            return RedirectToAction("Index", "Home");
        }
    }
}