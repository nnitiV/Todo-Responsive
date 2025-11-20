using FirstWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FirstWebApp.Pages
{
    public class Profile : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [BindProperty]
        public IFormFile? ImageUpload { get; set; }
        public User LoggedUser;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _environment;
        public Profile(UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
        }
        public async Task OnGet()
        {
            LoggedUser = await _userManager.GetUserAsync(User);

            if (LoggedUser != null)
            {
                Email = LoggedUser.Email;
                UserName = LoggedUser.UserName;
            }
        }
        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Login");
            }

            LoggedUser = user;

            if(Password == null || Password.Trim().Length == 0)
            {
                ModelState.AddModelError("Password", "Password is required to save changes.");
                return Page();
            }
            var checkPassword = await _userManager.CheckPasswordAsync(user, Password);
            if (!checkPassword)
            {
                ModelState.AddModelError("Password", "Invalid password. Changes not saved.");
                return Page();
            }
            Console.WriteLine($"DEBUG: ImageUpload is null? {ImageUpload == null}");
            if (ImageUpload != null) Console.WriteLine($"DEBUG: File Name: {ImageUpload.FileName}");
            if (ImageUpload != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageUpload.FileName);
                var uploadPath = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);
                var filePath = Path.Combine(uploadPath, fileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageUpload.CopyToAsync(fileStream);
                }

                user.ProfilePictureUrl = "/uploads/" + fileName;
            }

            user.Email = Email;
            user.UserName = UserName;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                return RedirectToPage();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
