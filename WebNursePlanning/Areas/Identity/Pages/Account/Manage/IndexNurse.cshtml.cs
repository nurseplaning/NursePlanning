using DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace WebNursePlanning.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexNurseModel : PageModel
    {
        private readonly UserManager<Person> _userManager;
        private readonly SignInManager<Person> _signInManager;

        public IndexNurseModel(
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

            [Phone]
            [Required]
            [Display(Name = "N° Téléphone")]
            [RegularExpression(@"\d{10}|\+33\d{9}|\+33\s\d{1}\s\d{2}\s\d{2}\s\d{2}\s\d{2}|\d{2}\s\d{2}\s\d{2}\s\d{2}\s\d{2}", ErrorMessage = "Characters are not allowed.")]
            public string PhoneNumber { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "N° de Siret")]
            [RegularExpression(@"^[0-9]*$", ErrorMessage = "Characters are not allowed.")]
            public string SiretNumber { get; set; }
        }

        private async Task LoadAsync(Nurse user)
        {
            var nurse = await _userManager.GetUserAsync(HttpContext.User) as Nurse;
            //    var nurse = (Nurse)person;
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = nurse.FirstName,
                LastName = nurse.LastName,
                BirthDay = nurse.BirthDay,
                Adress = nurse.Adress,
                SiretNumber = nurse.SiretNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            //  var patient = (Nurse)user;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync((Nurse)user);
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
                await LoadAsync((Nurse)user);
                return Page();
            }

            var nurse = (Nurse)user;
            nurse.Adress = Input.Adress;
            nurse.PhoneNumber = Input.PhoneNumber;
            nurse.FirstName = Input.FirstName;
            nurse.LastName = Input.LastName;
            nurse.BirthDay = Input.BirthDay;
            nurse.Adress = Input.Adress;
            nurse.SiretNumber = Input.SiretNumber;

            var setPhoneResult = await _userManager.UpdateAsync(nurse);
            if (!setPhoneResult.Succeeded)
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