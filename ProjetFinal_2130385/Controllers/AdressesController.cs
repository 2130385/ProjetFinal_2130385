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
    }
}
