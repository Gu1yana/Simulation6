using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulation6.DataAccessLayer;
using Simulation6.Models;
using Simulation6.ViewModels.ProfessionVM;

namespace Simulation6.Areas.Admin.Controllers;

[Area("Admin")]
public class ProfessionController(DewiDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var professions=await _context.Professions.Select(x=>new ProfessionGetVM ()
        {
            Id=x.Id,
            Name=x.Name,
        }).ToListAsync();
        return View(professions);
    }
    public async Task<IActionResult> Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProfessionCreateVM vm)
    {
        if (!ModelState.IsValid) return View(vm);
        Profession profession = new();
        profession.Name=vm.Name;
        await _context.Professions.AddAsync(profession);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int?id)
    {
        if(!id.HasValue || id<1) return BadRequest();

        var entity = await _context.Professions.Where(x => x.Id == id).ExecuteDeleteAsync();
        if (entity == 0) return NotFound();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int? id)
    {
        if (!id.HasValue || id < 1) return BadRequest();
        var profession=await _context.Professions.FirstOrDefaultAsync(x => x.Id == id);
        if (profession is null) return BadRequest();
        ProfessionUpdateVM vm = new();
        vm.Name = profession.Name;
        return View(vm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int?id, ProfessionUpdateVM vm)
    {
        if (!id.HasValue || id < 1) return BadRequest();
        if (!ModelState.IsValid) return View(vm);
        var entity=await _context.Professions.Where(x=>x.Id == id).FirstOrDefaultAsync();   
        if(entity is null) return BadRequest();
        entity.Name = vm.Name;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

}