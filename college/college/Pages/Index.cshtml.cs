using college.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using test_practica.Model;

namespace college.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Specialisation> Specialisations = new List<Specialisation>();

        [BindProperty]
        public Request request { get; set; } = new();
        [BindProperty]
        public Review review { get; set; } = new();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public async Task OnGet()
        {

            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    Specialisations = await db.Specialisations.ToListAsync();
                }
                catch
                {
                    await Response.WriteAsync("Ошибка подключения к бд. Обратитесь к администратору");
                }

            }
        }
        public async Task<IActionResult> OnPostAddRequest()
        {

            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    db.Requests.Add(request);
                    await db.SaveChangesAsync();
                    return RedirectToPage("Index");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<IActionResult> OnPostAddReview()
        {

            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    db.Reviews.Add(review);
                    await db.SaveChangesAsync();
                    return RedirectToPage("Index");
                }
                catch (Exception)
                {
                    throw;
                }
            }    
        }
    }
}