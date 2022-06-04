using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using uwierzytelnianie.Interfaces;
using uwierzytelnianie.Models;

namespace uwierzytelnianie.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPeopleService _personService;
        public IQueryable<People> Records { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IPeopleService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (ModelState.IsValid)
            {
                if (userId != null)
                {
                    _personService.AddEntry(Request.Form["Name"], Request.Form["LastName"], userId);
                }
                ViewData["Message"] = "Pomyślnie dodano użytkownika " + Request.Form["Name"] + " " + Request.Form["LastName"] + " do bazy danych";
                ViewData["MessageClass"] = "success";
            }
            else
            {
                ViewData["Message"] = "Podano błędne dane";
                ViewData["MessageClass"] = "warning";
            }
            return Page();
        }
    }
}