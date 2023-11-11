using college.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using test_practica.Model;

namespace college.Pages.Специальности
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public List<Specialisation> Specialisations = new List<Specialisation>();

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            using  (ApplicationContext db = new ApplicationContext())
            {
                Specialisations = await db.Specialisations.ToListAsync();
            }
        }
    }
}