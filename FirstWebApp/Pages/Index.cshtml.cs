using FirstWebApp.Data;
using FirstWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;
using TaskItem = FirstWebApp.Models.TaskItem;

namespace FirstWebApp.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        [BindProperty]
        public string NewTask { get; set; }
        [BindProperty]
        public string NewCategory { get; set; }
        [BindProperty]
        public int? SelectedCategoryId { get; set; }
        public List<TaskItem> MyTasks { get; set; }
        public List<Category> MyCategories { get; set; }
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task OnGet()
        {
            await GetTasks();
        }
        public async Task<IActionResult> OnPostTask()
        {
            ModelState.Remove("NewCategory");
            User user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("~/Login");

            int? safeCategoryId = null;

            if (SelectedCategoryId != 0)
            {
                var category = await _context.Categories.FindAsync(SelectedCategoryId);

                if (category == null || category.UserId != user.Id)
                {
                    ModelState.AddModelError(string.Empty, "Invalid category selected.");

                    await GetTasks(); 
                    return Page();
                }
                safeCategoryId = SelectedCategoryId;
            }

            List<TaskItem> serializedTasks = await _context.Tasks.ToListAsync();
            MyTasks = serializedTasks;

            if(NewTask == null || NewTask.Trim().Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Task title cannot be empty.");
                return Page();
            }

            TaskItem newTaskToAdd = new(NewTask, user.Id, safeCategoryId);
            MyTasks.Add(newTaskToAdd);

            _context.Tasks.Add(newTaskToAdd);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostCategory()
        {
            ModelState.Remove("NewTask");
            User user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("~/Login");
 
            if (NewCategory == null || NewCategory.Trim().Length == 0)
            {
                ModelState.AddModelError(string.Empty, "Category name cannot be empty.");
                return Page();
            }

            Category category = new Category { Name = NewCategory, UserId = user.Id};
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task GetTasks()
        {
            string userId = _userManager.GetUserId(User);
            List<Category> categories = await _context.Categories.Where(c => c.UserId == userId).ToListAsync();
            MyCategories = categories;
            List<TaskItem> tasks = await _context.Tasks.Where(T => T.UserId == userId).ToListAsync();
            MyTasks = tasks;
        }
    }
}
