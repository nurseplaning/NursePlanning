using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebNursePlanning.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexPatientModel : PageModel
    {
        private readonly UserManager<Person> _userManager;
        private readonly SignInManager<Person> _signInManager;

        public IndexPatientModel(
            UserManager<Person> userManager,
            SignInManager<Person> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Prénom")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Nom de Famille")]
            [RegularExpression(@"^[a-zA-Z''-'\s]{3,30}$", ErrorMessage = "Characters are not allowed.")]
            public string LastName { get; set; }

            [Required]
            [DataType(DataType.Date)]
            [Display(Name = "Date de naissance")]
            [RegularExpression(@"^[0-9\s]{4}-[0-9\s]{1,2}-[0-9\s]{1,2}$", ErrorMessage = "Characters are not really allowed.")]
            public DateTime BirthDay { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Adresse")]
            public string Adress { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Ville")]
            public string City { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Code Postal")]
            [RegularExpression(@"\d{5,6}", ErrorMessage = "Entrez uniquement jusqu'à 6 chiffres.")]
            public string PostalCode { get; set; }

            [DataType(DataType.Text)]
            [Display(Name = "Informations complémentaires Adresse")]
            public string ComplementaryAdressInformation { get; set; }

            [Phone]
            [Required]
            [Display(Name = "N° Téléphone")]
            [RegularExpression(@"\d{10}|\+33\d{9}|\+33\s\d{1}\s\d{2}\s\d{2}\s\d{2}\s\d{2}|\d{2}\s\d{2}\s\d{2}\s\d{2}\s\d{2}", ErrorMessage = "Characters are not allowed.")]
            public string PhoneNumber { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Régime Social")]
            public string SocialRegime { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Stationnement à proximité ?")]
            public bool IsParkingAvailable { get; internal set; }
        }

        private async Task LoadAsync(Patient user)
        {
            var patient = await _userManager.GetUserAsync(HttpContext.User) as Patient;
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                BirthDay = patient.BirthDay,
                Adress = patient.Adress,
                PostalCode = patient.PostalCode,
                City = patient.City,
                ComplementaryAdressInformation = patient.ComplementaryAdressInformation,
                SocialRegime = patient.SocialRegime
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var patient = (Patient)user;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync((Patient)user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync((Patient)user);
                return Page();
            }

            var patient = (Patient)user;
            patient.Adress = Input.Adress;
            patient.PhoneNumber = Input.PhoneNumber;
            patient.FirstName = Input.FirstName;
            patient.LastName = Input.LastName;
            patient.BirthDay = Input.BirthDay;
            patient.Adress = Input.Adress;
            patient.PostalCode = Input.PostalCode;
            patient.City = Input.City;
            patient.IsParkingAvailable = Input.IsParkingAvailable;
            patient.ComplementaryAdressInformation = Input.ComplementaryAdressInformation;
            patient.SocialRegime = Input.SocialRegime;

            var setPatientResult = await _userManager.UpdateAsync(patient);
            if (!setPatientResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to set profile.";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}