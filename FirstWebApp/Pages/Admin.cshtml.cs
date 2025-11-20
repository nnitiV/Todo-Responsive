using FirstWebApp.Data;
using FirstWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApp.Pages
{
    [Authorize(Roles="Admin")]
    public class AdminModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        public User LoggedUser;
        public List<TaskItem> AllTasks;
        public AdminModel(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task OnGet()
        {
            LoggedUser = await _userManager.GetUserAsync(User);

            AllTasks = await _context.Tasks
                .Include(t => t.User)
                .ToListAsync();
        }
        public async Task<IActionResult> OnPostDelete()
        {
            TaskItem taskToRemove = await _context.Tasks.FindAsync(Id);
            if (taskToRemove == null)
            {
                return new JsonResult(new { success = false, message = "Task not found." });
            }
            _context.Tasks.Remove(taskToRemove);
            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true, action = "delete", message = "Task deleted." });
        }
    }
}
