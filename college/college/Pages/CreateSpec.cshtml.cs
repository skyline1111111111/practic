
using college.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_practica.Model;

namespace college.Pages
{
    public class CreateSpecModel : PageModel
    {
       public IWebHostEnvironment env;
        [BindProperty]
        public Specialisation Specialisation { get; set; } = new()!;
        
        public CreateSpecModel(IWebHostEnvironment webHost)
        {
            env = webHost;
        }
        public void OnGet()
        {
           
        }
        public async Task<IActionResult> OnPost(IFormFile pdfLink, IFormFile excelLink, IFormFile photo)
        {
            if (pdfLink != null && excelLink != null && photo != null) 
            {
                string pathPdf ="/pdf/"+ pdfLink.FileName;
                string pathExcel = "/excel/" + excelLink.FileName;
                string pathImg = "/images/imgs/" + photo.FileName;

                using (var fileStreamPdf = new FileStream(env.WebRootPath + pathPdf, FileMode.Create))
                {
                    await pdfLink.CopyToAsync(fileStreamPdf);
                }

                using (var fileStreamExcel = new FileStream(env.WebRootPath + pathExcel, FileMode.Create))
                {
                    await excelLink.CopyToAsync(fileStreamExcel);
                }

                using (var fileStreamImg = new FileStream(env.WebRootPath + pathImg, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStreamImg);
                }

                using (ApplicationContext db = new ApplicationContext())
                {
                    Specialisation.PdfLink = pdfLink.FileName;
                    Specialisation.ExcelLink = excelLink.FileName;
                    Specialisation.Photo = photo.FileName;

                    db.Specialisations.Add(Specialisation);
                    await db.SaveChangesAsync();
                    return RedirectToPage("AdminPanel");
                }
            }
            else
            {
                return RedirectToPage();
            }
            
        }
    }
}
