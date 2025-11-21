using FirstWebApp.Data;
using FirstWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TaskItem = FirstWebApp.Models.TaskItem;

namespace FirstWebApp.Pages
{
    [Authorize]
    public class TasksModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Title { get; set; }
        public List<TaskItem> MyTasks { get; set; }
        public List<Category> MyCategories { get; set; }
        [BindProperty]
        public int CategoryIdToDelete { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? SelectedCategoryId { get; set; }
        public TasksModel(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task OnGet()
        {                                
            await GetTasks();
        }

        public async Task<IActionResult> OnPostDeleteCategoryAsync()
        {
            var userId = _userManager.GetUserId(User);

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == CategoryIdToDelete && c.UserId == userId);

            if (category == null)
            {
                return RedirectToPage(); 
            }

            List<TaskItem> tasksToDelete = await _context.Tasks.Where(t => t.CategoryId == category.Id && t.UserId == userId).ToListAsync();

            if (tasksToDelete != null && tasksToDelete.Any())
            {
                _context.Tasks.RemoveRange(tasksToDelete);
            }

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return RedirectToPage(new { SelectedCategoryId = 0 });
        }

        public async Task<IActionResult> OnPostDelete()
        {
            TaskItem taskToRemove = await GetSingleTaskFromUserId();
            if (taskToRemove == null)
            {
                return new JsonResult(new { success = false, message = "Task not found." });
            }
            Console.WriteLine("Removing task...");
            _context.Tasks.Remove(taskToRemove);
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, removedTaskId = taskToRemove.Id });

        }

        public async Task<IActionResult> OnPostUpdate()
        {
            TaskItem taskToUpdate = await GetSingleTaskFromUserId();
            if (taskToUpdate == null)
            {
                return new JsonResult(new { success = false, message = "Task not found." });
            }
            taskToUpdate.Title = Title;
            _context.Tasks.Update(taskToUpdate);
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, task = taskToUpdate });
        }

        public async Task<IActionResult> OnPostComplete()
        {
            TaskItem taskToUpdate = await GetSingleTaskFromUserId();
            if (taskToUpdate == null)
            {
                return new JsonResult(new { success = false, message = "Task not found." });
            }
            taskToUpdate.IsCompleted = !taskToUpdate.IsCompleted;
            _context.Tasks.Update(taskToUpdate);
            await _context.SaveChangesAsync();
            return new JsonResult(new { success = true, task = taskToUpdate });
        }

        public async Task<TaskItem> GetSingleTaskFromUserId()
        {
            string userId = _userManager.GetUserId(User);
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == Id && t.UserId == userId);
        }

        public async Task GetTasks()
        {
            string userId = _userManager.GetUserId(User);

            List<Category> categories = await _context.Categories.Where(c => c.UserId == userId).ToListAsync();
            MyCategories = categories;

            var query =  _context.Tasks.Include(t => t.Category).Where(T => T.UserId == userId);

            if(SelectedCategoryId.HasValue)
            {
                query = query.Where(t => t.CategoryId == SelectedCategoryId.Value);
            }

            MyTasks = await query.ToListAsync();
        }
    }
}