using college.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using test_practica.Model;

namespace college.Pages.Администратор
{
    public class AdminPanelModel : PageModel
    {
        public List<Review> Reviews = new List<Review>();
        public List<Specialisation> Specialisations = new List<Specialisation>();
        public List<Request> Requests = new List<Request>();
        public double avg;
        private IWebHostEnvironment _env;
        public AdminPanelModel(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task OnGetAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Reviews = await db.Reviews.ToListAsync ();
                Specialisations = await db.Specialisations.ToListAsync();
                Requests = await db.Requests.ToListAsync();
                avg = Math.Round(Reviews.Average(rev => rev.Rate),2);
               
            }

        }

        public async Task<IActionResult> OnPostExcelAsync()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Specialisations = await db.Specialisations.ToListAsync();
                Requests = await db.Requests.ToListAsync();
            }
            int[] counts = new int[Specialisations.Count];
            for (int i = 0; i < counts.Length; i++)
            {
                counts[i] = Requests.Count(req => req.Special1 == Specialisations[i].Name || req.Special2 == Specialisations[i].Name || req.Special3 == Specialisations[i].Name);
            }
            Application excel = new Application();
            excel.Visible = true;
            Workbook workbook;
            Worksheet worksheet;
            workbook = excel.Workbooks.Add();
            worksheet = (Worksheet)workbook.Sheets[1];
            worksheet.Name = "Количество заявок";
            for (int i = 1; i < counts.Length; i++)
            {

                worksheet.Cells[i, 1].Value = Specialisations[i - 1].Name;
                worksheet.Cells[i, 2].Value = counts[i - 1];
            }
            worksheet.Cells.Font.Name = "Times New Roman";
            worksheet.Cells.VerticalAlignment = 1;
            worksheet.Rows.AutoFit();
            worksheet.Columns.AutoFit();

            object misValue = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Excel.Range chartRange;

            ChartObjects xlCharts = (ChartObjects)worksheet.ChartObjects(Type.Missing);
            ChartObject myChart = xlCharts.Add(10, 10, 500, 300);
            Chart chartPage = myChart.Chart;
            chartPage.HasLegend = false;
            chartPage.HasTitle = true;
            chartPage.ChartTitle.Text = "Количество заявок";

            chartRange = worksheet.get_Range("A1", "B9");
            chartPage.SetSourceData(chartRange, misValue);
            chartPage.ChartType =  XlChartType.xlColumnClustered;
            chartPage.Location(XlChartLocation.xlLocationAsObject, "Лист2");

            return RedirectToPage("AdminPanel");
        }

        public async Task<IActionResult> OnPostDelRevAsync(int id)
        {
         
            using (ApplicationContext db = new ApplicationContext())
            {
                var review = await db.Reviews.FindAsync(id);
                if (review != null)
                {
                    db.Reviews.Remove(review);
                    await db.SaveChangesAsync();
                }
                return RedirectToPage();
            }
            
        }

        public async Task<IActionResult> OnPostDelReqAsync(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var request = await db.Requests.FindAsync(id);
                if (request != null)
                {
                    db.Requests.Remove(request);
                    await db.SaveChangesAsync();
                }
                return RedirectToPage();
            }

        }

        public async Task<IActionResult> OnPostDelSpecAsync(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var spec = await db.Specialisations.FindAsync(id);
                if (spec != null)
                {
                    System.IO.File.Delete(_env.WebRootPath + "/pdf/" + spec.PdfLink);
                    System.IO.File.Delete(_env.WebRootPath + "/excel/" + spec.ExcelLink);
                    System.IO.File.Delete(_env.WebRootPath + "/images/imgs/" + spec.Photo);
                    db.Specialisations.Remove(spec);
                    await db.SaveChangesAsync();
                }
                return RedirectToPage();
            }

        }
    }
}
