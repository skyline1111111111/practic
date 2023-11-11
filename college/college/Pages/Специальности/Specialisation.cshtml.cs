using college.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using test_practica.Model;

namespace college.Pages.Специальности
{
    public class SpecialisationModel : PageModel
    {
        public int Id;
        public List<Specialisation> Specialisations = new List<Specialisation>();
        public async Task OnGetAsync(int id)
        {
            Id = id;
            using (ApplicationContext db = new ApplicationContext())
            {
                Specialisations = await db.Specialisations.ToListAsync();
            }
        }
    }
}
