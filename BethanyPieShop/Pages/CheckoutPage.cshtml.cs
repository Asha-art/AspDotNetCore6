using BethanyPieShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BethanyPieShop.Pages
{
    public class CheckoutPageModel : PageModel
    {
        [BindProperty]
        public Order Order { get; set; }
        public void OnGet()
        {
            ViewData["CheckoutCompleteMessage"] = "Thanks for your order. You'll soon enjoy our delicious pies!";
        }
    }
}
