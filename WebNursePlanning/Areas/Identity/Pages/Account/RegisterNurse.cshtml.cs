using DomainModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace WebNursePlanning.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterNurseModel : PageModel
    {
        private readonly SignInManager<Person> _signInManager;
        private readonly UserManager<Person> _userManager;
        private readonly ILogger<RegisterNurseModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterNurseModel(
            UserManager<Person> userManager,
            SignInManager<Person> signInManager,
            ILogger<RegisterNurseModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "E-mail")]
            public string Email { get; set; }

            [EmailAddress]
            [Display(Name = "Confirmation E-mail")]
            [Compare("Email", ErrorMessage = "L'e-mail et la confiramtion d'e-mail ne correspondent pas.")]
            public string ConfirmEmail { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Mot de passe")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmation password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Nom")]
            [RegularExpression(@"^[a-zA-Z''-'\s]{3,30}$", ErrorMessage = "Characters are not allowed.")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Prenom")]
            [RegularExpression(@"^[a-zA-Z''-'\s]{3,30}$", ErrorMessage = "Characters are not allowed.")]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Date naissance")]
            public DateTime BirthDay { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Adress")]
            public string Adress { get; set; }

            [Required]
            [DataType(DataType.PhoneNumber)]
            [Display(Name = "N° Téléphone")]
            [RegularExpression(@"\d{10}|\+33\d{9}|\+33\s\d{1}\s\d{2}\s\d{2}\s\d{2}\s\d{2}|\d{2}\s\d{2}\s\d{2}\s\d{2}\s\d{2}", ErrorMessage = "Characters are not allowed.")]
            public string Phonenumber { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "N° de Siret")]
            [RegularExpression(@"^[0-9]*$", ErrorMessage = "Characters are not allowed.")]
            public string SiretNumber { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new Nurse
                {
                    UserName = $"{Input.LastName}{Input.FirstName}{DateTime.Now:yyyyymmddHHmmss}",
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    BirthDay = Input.BirthDay,
                    Adress = Input.Adress,
                    SiretNumber = Input.SiretNumber,
                    PhoneNumber = Input.Phonenumber,
                    IsActive = false
                };
                var mailExiste = await _userManager.FindByEmailAsync(Input.Email);
                if (mailExiste is not null)
                {
                    ErrorMessage = "Ce mail existe deja.";
                    return Page();
                }

                var nurses = await _userManager.GetUsersInRoleAsync("ROLE_ADMIN");
                var admins = await _userManager.GetUsersInRoleAsync("ROLE_SUPER_ADMIN");

                foreach (var item in nurses)
                {
                    var nurse = item as Nurse;
                    if (nurse.SiretNumber == Input.SiretNumber)
                    {
                        StatusMessage = "Le numéro de siret est déjà enregistré en base";
                        return Page();
                    }
                }
                foreach (var item in admins)
                {
                    var nurse = item as Nurse;
                    if (nurse.SiretNumber == Input.SiretNumber)
                    {
                        StatusMessage = "Le numéro de siret est déjà enregistré en base";
                        return Page();
                    }
                }

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    // add Role Admin to Nurse
                    await _userManager.AddToRoleAsync(user, "ROLE_ADMIN");

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        StatusMessage = "Votre compte a bien été créé vous pourriez vous connecter après l'activation pour votre administrateur";
                        return Page();
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}