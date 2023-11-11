using college.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace college.Pages.Администратор
{
    public class AdminAuthorizationModel : PageModel
    {
		[BindProperty]
		public Admin admin { get; set; } = new();
        public async Task<IActionResult> OnPostAuthorization()
        {
			try
			{
				if (admin.login == "qruwer" & admin.password == "admin")
				{
					return RedirectToPage("AdminPanel");
				}
				else
				{
                    return RedirectToPage("AdminAuthorization");
                }
			}
			catch (Exception)
			{
				await Response.WriteAsync("<script>alert('Error! Try again')</script>");
                return RedirectToPage("AdminAuthorization");
            }
        }
    }
	
}
