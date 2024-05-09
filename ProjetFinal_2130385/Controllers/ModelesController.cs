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
    public class ModelesController : Controller
    {
        private readonly DronesDatabaseContext _context;

        public ModelesController(DronesDatabaseContext context)
        {
            _context = context;
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

        //public async Task<IActionResult> DetailsModeleImage(int? id)
        //{
        //    if (id == null || _context.Modeles == null)
        //    {
        //        return NotFound();
        //    }
        //    var modele = await _context.Modeles.FirstOrDefaultAsync(x => x.ModeleId == id);
        //    if (modele == null)
        //    {
        //        return NotFound();
        //    }
        //    DetailsModeleImageVM detailVM = new DetailsModeleImageVM();
        //    detailVM.NomModele = modele.Nom;
        //    detailVM.ImageModele = modele.Image;
        //    return View(detailVM);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImage(ImageUploadVM imgVM)
        {
            if (ModelState.IsValid)
            {
                if(imgVM.FormFile !=null && imgVM.FormFile.Length>= 0)
                {
                    MemoryStream stream = new MemoryStream();
                    await imgVM.FormFile.CopyToAsync(stream);
                    byte[] fichierImage = stream.ToArray();
                    imgVM.Image.FichierImage = fichierImage;
                }
                _context.Add(imgVM.Image);
                var modele = await _context.Modeles.FirstOrDefaultAsync(x => x.Nom == imgVM.NomModele);
                //modele.Image= imgVM.Image;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(imgVM.Image);
        }

        public async Task<IActionResult> AjoutImagePourUnModele(int? id)
        {
            var modele = await _context.Modeles.FirstOrDefaultAsync(x => x.ModeleId == id);
            ImageUploadVM imgVM = new ImageUploadVM();
            imgVM.NomModele = modele.Nom;
            return View(imgVM);
        }


        // GET: Modeles
        public async Task<IActionResult> Index()
        {
              return _context.Modeles != null ? 
                          View(await _context.Modeles.ToListAsync()) :
                          Problem("Entity set 'DronesDatabaseContext.Modeles'  is null.");
        }

        // GET: Modeles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Modeles == null)
            {
                return NotFound();
            }

            var modele = await _context.Modeles
                .FirstOrDefaultAsync(m => m.ModeleId == id);
            if (modele == null)
            {
                return NotFound();
            }

            return View(modele);
        }

        // GET: Modeles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Modeles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModeleId,Nom,Vitesse,Prix,DateSortie")] Modele modele)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modele);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modele);
        }

        // GET: Modeles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Modeles == null)
            {
                return NotFound();
            }

            var modele = await _context.Modeles.FindAsync(id);
            if (modele == null)
            {
                return NotFound();
            }
            return View(modele);
        }

        // POST: Modeles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModeleId,Nom,Vitesse,Prix,DateSortie")] Modele modele)
        {
            if (id != modele.ModeleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modele);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModeleExists(modele.ModeleId))
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
            return View(modele);
        }

        // GET: Modeles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Modeles == null)
            {
                return NotFound();
            }

            var modele = await _context.Modeles
                .FirstOrDefaultAsync(m => m.ModeleId == id);
            if (modele == null)
            {
                return NotFound();
            }

            return View(modele);
        }

        // POST: Modeles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Modeles == null)
            {
                return Problem("Entity set 'DronesDatabaseContext.Modeles'  is null.");
            }
            var modele = await _context.Modeles.FindAsync(id);
            if (modele != null)
            {
                _context.Modeles.Remove(modele);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModeleExists(int id)
        {
          return (_context.Modeles?.Any(e => e.ModeleId == id)).GetValueOrDefault();
        }
    }
}
