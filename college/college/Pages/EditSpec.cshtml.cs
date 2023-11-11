using college.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using test_practica.Model;

namespace college.Pages
{
    public class EditSpecModel : PageModel
    {
        public int Id;
        public int Num;
        public IWebHostEnvironment env;
        [BindProperty]
        public Specialisation? Specialisation { get; set; } = new()!;

        public List<Specialisation> Specialisations = new List<Specialisation>();

        public EditSpecModel(IWebHostEnvironment webHost)
        {
            env = webHost;
        }
        public async Task<IActionResult> OnGetAsync(int id, int num)
        {
            Id = id;
            Num = num;
            using (ApplicationContext db = new ApplicationContext())
            {
                Specialisation = await db.Specialisations.FindAsync(id);
             
                //Specialisations = db.Specialisations.AsNoTracking().ToList();

                if (Specialisation == null) return NotFound();

                return Page();
            }

        }
        public async Task<IActionResult> OnPostAsync(IFormFile pdfLink, IFormFile excelLink, IFormFile photo)
        {
            if (pdfLink != null && excelLink != null && photo != null)
            {
                string pathPdf = "/pdf/" + pdfLink.FileName;
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
                Specialisation.PdfLink = pdfLink.FileName;
                Specialisation.ExcelLink = excelLink.FileName;
                Specialisation.Photo = photo.FileName;
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                Specialisations = db.Specialisations.AsNoTracking().ToList();
                Id = Convert.ToInt32(RouteData.Values["id"]);
                Num = Convert.ToInt32(RouteData.Values["num"]);

                string pdf = Specialisations[Num].PdfLink;
                string excel = Specialisations[Num].ExcelLink;
                string photoo = Specialisations[Num].Photo;

                if (Specialisation.PdfLink == null && Specialisation.ExcelLink == null & Specialisation.Photo == null)
                {
                    Specialisation.PdfLink = pdf;
                    Specialisation.ExcelLink = excel;
                    Specialisation.Photo = photoo;
                }
                db.Specialisations.Update(Specialisation);
                await db.SaveChangesAsync();
                return RedirectToPage("AdminPanel");
            }
        }
    }
}
