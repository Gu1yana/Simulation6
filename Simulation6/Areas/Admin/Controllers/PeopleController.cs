using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulation6.DataAccessLayer;
using Simulation6.Models;
using Simulation6.ViewModels.PeopleVM;
using Simulation6.ViewModels.ProfessionVM;
using System;

namespace Simulation6.Areas.Admin.Controllers;

[Area("Admin")]
public class PeopleController(DewiDbContext _context) : Controller
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
    public async Task<IActionResult> Create()
    {
        await ViewBags();
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PeopleCreateVM vm)
    {
        await ViewBags();
        if (!ModelState.IsValid) return View(vm);
        if (!vm.ImageFile.ContentType.StartsWith("image"))
        {
            ModelState.AddModelError("ImageFile", "Fayl tipi shekil olmalidir!");
            return View(vm);
        }
        if (vm.ImageFile.Length > 1024 * 1024 * 2)
        {
            ModelState.AddModelError("ImageFile", "Fayl olchusu 2mb dan az olmalidir!");
            return View(vm);
        }
        string newFileName = Guid.NewGuid().ToString() + vm.ImageFile.FileName;
        string path = Path.Combine("wwwroot", "images", newFileName);
        using FileStream fs = new(path, FileMode.OpenOrCreate);
        await vm.ImageFile.CopyToAsync(fs);
        Person person = new();
        person.Fullname = vm.Fullname;
        person.ImageUrl = newFileName;
        person.Description = vm.Description;
        person.ProfessionId = vm.ProfessionId;
        await _context.People.AddAsync(person);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? id)
    {
        if (!id.HasValue || id < 1) return BadRequest();
        var entity = await _context.People.FirstOrDefaultAsync(x => x.Id == id);
        if (entity is null) return NotFound();
        _context.People.Remove(entity);

        if (System.IO.File.Exists(Path.Combine("wwwroot", "images", entity.ImageUrl)))
            System.IO.File.Delete(Path.Combine("wwwroot", "images", entity.ImageUrl));
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int? id)
    {
        await ViewBags();
        if (!id.HasValue || id < 1) return BadRequest();
        var people = await _context.People.FirstOrDefaultAsync(x => x.Id == id);
        if (people is null) return BadRequest();
        PeopleUpdateVM vm = new();
        vm.Fullname = people.Fullname;
        vm.ProfessionId = people.ProfessionId;
        vm.Description = people.Description;
        vm.ImageUrl = people.ImageUrl;
        return View(vm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int? id, PeopleUpdateVM vm)
    {
        await ViewBags();
        if (!id.HasValue || id < 1) return BadRequest();
        var people = await _context.People.FirstOrDefaultAsync(x => x.Id == id);
        if (people is null) return BadRequest();
        if (!ModelState.IsValid) return View(vm);
        if (vm.ImageFile is not null)
        {
            if (!vm.ImageFile.ContentType.StartsWith("image"))
            {
                ModelState.AddModelError("ImageFile", "Fayl tipi shekil olmalidir!");
                return View(vm);
            }
            if (vm.ImageFile.Length > 1024 * 1024 * 2)
            {
                ModelState.AddModelError("ImageFile", "Fayl olchusu 2mb dan az olmalidir!");
                return View(vm);
            }
            string newFileName = Guid.NewGuid().ToString() + vm.ImageFile.FileName;
            string path = Path.Combine("wwwroot", "images", newFileName);
            using FileStream fs = new(path, FileMode.OpenOrCreate);

            if (System.IO.File.Exists(Path.Combine("wwwroot", "images", people.ImageUrl)))
                System.IO.File.Delete(Path.Combine("wwwroot", "images", people.ImageUrl));

            await vm.ImageFile.CopyToAsync(fs);
            people.ImageUrl = newFileName;
        }
        people.Fullname = vm.Fullname;
        people.Description = vm.Description;
        people.ProfessionId = vm.ProfessionId;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task ViewBags()
    {
        var professions = await _context.Professions.Select(x => new ProfessionGetVM
        {
            Id = x.Id,
            Name = x.Name,
        }).ToListAsync();
        ViewBag.Professions = professions;
    }
}
