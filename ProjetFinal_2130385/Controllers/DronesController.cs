using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjetFinal_2130385.Data;
using ProjetFinal_2130385.Models;
using ProjetFinal_2130385.ViewModels;

namespace ProjetFinal_2130385.Controllers
{
    public class DronesController : Controller
    {
        private readonly DronesDatabaseContext _context;

        public DronesController(DronesDatabaseContext context)
        {
            _context = context;
        }

        // GET: Drones
        public async Task<IActionResult> Index()
        {
            var dronesDatabaseContext = _context.Drones.Include(d => d.Modele);
            return View(await dronesDatabaseContext.ToListAsync());
        }

        // GET: VueDrones
        public async Task<IActionResult> VueDrones()
        {
            return View(await _context.VwDrones.ToListAsync());
        }

        // GET: RevenusModeles
        public async Task<IActionResult> RevenusModeles()
        {
            return View(await _context.Modeles.ToListAsync());
        }

        // GET: RevenusPourUnModele
        public async Task<IActionResult> RevenusPourUnModele(int id)
        {
            try
            {
                string query = "EXEC Magasins.usp_RevenusPourUnModele @ModeleID, @Revenu OUTPUT";

                var parameters = new[]
                {
            new SqlParameter("@ModeleID", id),
            new SqlParameter
            {
                ParameterName = "@Revenu",
                SqlDbType = SqlDbType.Decimal,
                Precision = 10,
                Scale = 2,
                Direction = ParameterDirection.Output
            }
        };

                await _context.Database.ExecuteSqlRawAsync(query, parameters);

                decimal revenu = (decimal)parameters[1].Value;

                return View(revenu);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Une erreur est survenue. Veuillez réessayer.");
                return View(null);
            }
        }

        public async Task<IActionResult> ImagesModeles()
        {
            return View(await _context.Modeles.ToListAsync());
        }

        public async Task<IActionResult> ImagePourUnModele(int? id)
        {
            Modele? modele = await _context.Modeles.Where(x => x.ModeleId == id).FirstOrDefaultAsync();
            return View(modele);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjoutImageAuModele(int? id)
        {
            if(id == null || _context.Modeles == null)
            {
                return NotFound();
            }
            Modele? modele = await _context.Modeles.Where(x => x.ModeleId == id).FirstOrDefaultAsync();
            if(modele == null)
            {
                return NotFound();
            }
            ImageUploadVM imgVM = new ImageUploadVM();
            imgVM.NomModele = modele.Nom;
            imgVM.FormFile = null;
            return View(imgVM);
        }


        // GET: Drones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Drones == null)
            {
                return NotFound();
            }

            var drone = await _context.Drones
                .Include(d => d.Modele)
                .FirstOrDefaultAsync(m => m.DroneId == id);
            if (drone == null)
            {
                return NotFound();
            }

            return View(drone);
        }

        // GET: Drones/Create
        public IActionResult Create()
        {
            ViewData["ModeleId"] = new SelectList(_context.Modeles, "ModeleId", "ModeleId");
            return View();
        }

        // POST: Drones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DroneId,ModeleId,NumSerie")] Drone drone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModeleId"] = new SelectList(_context.Modeles, "ModeleId", "ModeleId", drone.ModeleId);
            return View(drone);
        }

        // GET: Drones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Drones == null)
            {
                return NotFound();
            }

            var drone = await _context.Drones.FindAsync(id);
            if (drone == null)
            {
                return NotFound();
            }
            ViewData["ModeleId"] = new SelectList(_context.Modeles, "ModeleId", "ModeleId", drone.ModeleId);
            return View(drone);
        }

        // POST: Drones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DroneId,ModeleId,NumSerie")] Drone drone)
        {
            if (id != drone.DroneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DroneExists(drone.DroneId))
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
            ViewData["ModeleId"] = new SelectList(_context.Modeles, "ModeleId", "ModeleId", drone.ModeleId);
            return View(drone);
        }

        // GET: Drones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Drones == null)
            {
                return NotFound();
            }

            var drone = await _context.Drones
                .Include(d => d.Modele)
                .FirstOrDefaultAsync(m => m.DroneId == id);
            if (drone == null)
            {
                return NotFound();
            }

            return View(drone);
        }

        // POST: Drones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Drones == null)
            {
                return Problem("Entity set 'DronesDatabaseContext.Drones'  is null.");
            }
            var drone = await _context.Drones.FindAsync(id);
            if (drone != null)
            {
                _context.Drones.Remove(drone);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DroneExists(int id)
        {
            return (_context.Drones?.Any(e => e.DroneId == id)).GetValueOrDefault();
        }
    }
}
