using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GroceryStore.Pages.SamplePages
{
    public class BasicsModel : PageModel
    {
        public string MyName;
        
        [TempData]
        public string FeedBack { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? id { get; set; }

        public void OnGet()
        {
            Random rnd = new Random();
            int oddeven = rnd.Next(0,25);
            if (oddeven % 2 == 0)
            {
                MyName = $"Zach is even {oddeven}";
            }
            else 
            {
                MyName = null;
            }
        }
        
        public IActionResult OnPost()
        {
            Thread.Sleep(2000);

            string buttonvalue = Request.Form["theButton"];
            FeedBack = $"Button pressed is {buttonvalue} with numeric input of {id}";
            //return Page(); //does not issue an OnGet()
            return RedirectToPage(new {id = id}); //request for OnGet()
        }
    }
}
