using FirstWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FirstWebApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public bool RememberMe { get; set; }
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public LoginModel(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Email);
                if(user != null)
                {
                    Console.WriteLine(Email, RememberMe);
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, Password, RememberMe, lockoutOnFailure: false);
                    if(result.Succeeded)
                    {
                        return RedirectToPage("/index");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid login atempt");
            return Page();
        }
    }
}
