using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulation6.DataAccessLayer;
using Simulation6.ViewModels.PeopleVM;

namespace Simulation6.Controllers;

public class HomeController(DewiDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var people = await _context.People.Include(x => x.Profession).Select(x => new PeopleGetVM()
        {
            Id = x.Id,
            Fullname = x.Fullname,
            Description = x.Description,
            ProfessionId = x.ProfessionId,
            ImageUrl = x.ImageUrl,
            Profession = new()
            {
                Name = x.Profession.Name,
            }
        }).ToListAsync();
        return View(people);
    }
}