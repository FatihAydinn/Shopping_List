// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Shopping_List.Data;
using Shopping_List.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Shopping_List.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly IdentityDbContext _identityDbContext;

        //public List<CustomUser> Users { get; set; }
        public IndexModel(
            UserManager<CustomUser> userManager,
            SignInManager<CustomUser> signInManager,
            IdentityDbContext identityDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityDbContext = identityDbContext;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            /// 

            //[Phone]
            [Display(Name = "Telefon No")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Şehir")]
            public string City { get; set; }

            [Display(Name = "İlçe")]
            public string District { get; set; }

            [Display(Name = "Adres")]
            public string Adress { get; set; }
        }

        private async Task LoadAsync(CustomUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //var city = user.City;
            //var district = user.District;
            //var adress = user.Adress;

            Username = userName;
            //Input = new InputModel
            //{
            //    PhoneNumber = phoneNumber,
            //    City = city,
            //    District = district,
            //    Adress = adress
            //};
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            //user.City = Input.City;
            //user.District = Input.District;
            //user.PhoneNumber = Input.PhoneNumber;
            //user.Adress = Input.Adress;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                //return RedirectToPage("./Account/Manage/Index");
                StatusMessage = "Güncelleme Başarılı";
                return Page();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
