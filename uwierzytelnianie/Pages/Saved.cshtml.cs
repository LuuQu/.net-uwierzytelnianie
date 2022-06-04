using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using uwierzytelnianie.Data;
using uwierzytelnianie.Interfaces;
using uwierzytelnianie.Models;

namespace uwierzytelnianie.Pages
{
    [Authorize]
    public class SavedModel : PageModel
    {
        private readonly ILogger<SavedModel> _logger;
        private readonly IPeopleService _personService;
        private readonly ApplicationDbContext _context;
        public IQueryable<People> Records { get; set; }

        public SavedModel(ILogger<SavedModel> logger, IPeopleService personService, ApplicationDbContext context)
        {
            _logger = logger;
            _personService = personService;
            _context = context;
        }

        public void OnGet()
        {
            Records = _personService.GetAllEntries();
        }
        public IActionResult OnPost(int Id)
        {
            People people = _context.People.FirstOrDefault(p => p.Id == Id);
            if (people.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                if (Id != 0)
                {
                    _personService.DeletePeople(Id);
                }
                ViewData["Message"] = "Usun¹³eœ osobê " + people.FirstName + " " + people.LastName + " z bazy danych.";
                ViewData["MessageClass"] = "success";
            }
            else
            {
                ViewData["Message"] = "Nie mo¿esz usun¹æ osoby, której nie doda³eœ!";
                ViewData["MessageClass"] = "warning";
            }
            Records = _personService.GetAllEntries();
            return Page();
        }
    }
}
