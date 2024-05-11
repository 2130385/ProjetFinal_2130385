using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetFinal_2130385.Data;
using ProjetFinal_2130385.Models;
using ProjetFinal_2130385.ViewModels;

namespace ProjetFinal_2130385.Controllers
{
    public class AdressesController : Controller
    {
        private readonly DronesDatabaseContext _context;
        private const int PageSize = 5;

        public AdressesController(DronesDatabaseContext context)
        {
            _context = context;
        }

        // GET: Adresses
        public async Task<IActionResult> Index(string villeFilter, string provinceFilter, int page = 1)
        {
            var adresses = _context.Adresses.AsQueryable();

            var villes = await adresses.Select(a => a.Ville).Distinct().ToListAsync();
            var provinces = await adresses.Select(a => a.Province).Distinct().ToListAsync();

            villes.Insert(0, "");
            provinces.Insert(0, "");

            ViewBag.Villes = villes;
            ViewBag.Provinces = provinces;
            ViewBag.VilleSelectionnee = villeFilter;
            ViewBag.ProvinceSelectionnee = provinceFilter;

            if (!string.IsNullOrEmpty(villeFilter))
            {
                adresses = adresses.Where(a => a.Ville == villeFilter);
            }
            if (!string.IsNullOrEmpty(provinceFilter))
            {
                adresses = adresses.Where(a => a.Province == provinceFilter);
            }

            int totalAdresses = await adresses.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalAdresses / PageSize);
            adresses = adresses.Skip((page - 1) * PageSize).Take(PageSize);

            var viewModel = new AdressesVM
            {
                Adresses = await adresses.ToListAsync(),
                TotalPages = totalPages,
                CurrentPage = page
            };

            return View(viewModel);
        }

        // GET: Adresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Adresses == null)
            {
                return NotFound();
            }

            var adresse = await _context.Adresses
                .FirstOrDefaultAsync(m => m.AdresseId == id);
            if (adresse == null)
            {
                return NotFound();
            }

            return View(adresse);
        }

        // GET: Adresses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdresseId,NoPorte,NoApp,Rue,Ville,CodePostal,Province,Pays")] Adresse adresse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adresse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adresse);
        }

        // GET: Adresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Adresses == null)
            {
                return NotFound();
            }

            var adresse = await _context.Adresses.FindAsync(id);
            if (adresse == null)
            {
                return NotFound();
            }
            return View(adresse);
        }

        // POST: Adresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdresseId,NoPorte,NoApp,Rue,Ville,CodePostal,Province,Pays")] Adresse adresse)
        {
            if (id != adresse.AdresseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adresse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdresseExists(adresse.AdresseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(adresse);
        }

        // GET: Adresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Adresses == null)
            {
                return NotFound();
            }

            var adresse = await _context.Adresses
                .FirstOrDefaultAsync(m => m.AdresseId == id);
            if (adresse == null)
            {
                return NotFound();
            }

            return View(adresse);
        }

        // POST: Adresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Adresses == null)
            {
                return Problem("Entity set 'DronesDatabaseContext.Adresses'  is null.");
            }
            var adresse = await _context.Adresses.FindAsync(id);
            if (adresse != null)
            {
                _context.Adresses.Remove(adresse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdresseExists(int id)
        {
          return (_context.Adresses?.Any(e => e.AdresseId == id)).GetValueOrDefault();
        }
    }
}
