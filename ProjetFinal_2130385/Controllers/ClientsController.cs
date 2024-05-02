using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjetFinal_2130385.Data;
using ProjetFinal_2130385.Models;

namespace ProjetFinal_2130385.Controllers
{
    public class ClientsController : Controller
    {
        private readonly DronesDatabaseContext _context;

        public ClientsController(DronesDatabaseContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index()
        {
            await ChiffrementNomFamilleClients();
            var dronesDatabaseContext = _context.Clients.Include(c => c.Adresse);
            return View(await dronesDatabaseContext.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }
            await DechiffrementNomFamilleClient(id);
            var client = await _context.Clients
                .Include(c => c.Adresse)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdresseId");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,AdresseId,Prenom,NomFamille,NoTel1,NoTel2,DateNaissance")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                await ChiffrementNomFamilleClient(client.ClientId);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdresseId", client.AdresseId);
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdresseId", client.AdresseId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,AdresseId,Prenom,NomFamille,NoTel1,NoTel2,DateNaissance")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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
            ViewData["AdresseId"] = new SelectList(_context.Adresses, "AdresseId", "AdresseId", client.AdresseId);
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Adresse)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'DronesDatabaseContext.Clients'  is null.");
            }
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return (_context.Clients?.Any(e => e.ClientId == id)).GetValueOrDefault();
        }
        [HttpPost]
        public async Task<IActionResult> ChiffrementNomFamilleClients()
        {
            string query = "EXEC Clients.usp_ChiffrerNomFamilleTousLesClients";
            try
            {
                await _context.Database.ExecuteSqlRawAsync(query);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Une erreur est survenue. Veuillez réessayer.");
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> DechiffrementNomFamilleClients()
        {
            string query = "EXEC Clients.usp_DechiffrerNomFamilleTousLesClients";
            try
            {
                await _context.Database.ExecuteSqlRawAsync(query);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Une erreur est survenue. Veuillez réessayer.");
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ChiffrementNomFamilleClient(int id)
        {
            string query = "EXEC Clients.usp_ChiffrerNomFamilleClient @ClientID";
            SqlParameter parameters = new SqlParameter { ParameterName = "@ClientID", Value = id };
            try
            {
                await _context.Database.ExecuteSqlRawAsync(query, parameters);
                return RedirectToAction("Details", id);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Une erreur est survenue. Veuillez réessayer.");
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> DechiffrementNomFamilleClient(int id)
        {
            string query = "EXEC Clients.usp_DechiffrerNomFamilleClient @ClientID";
            SqlParameter parameters = new SqlParameter { ParameterName = "@ClientID", Value = id };
            try
            {
                await _context.Database.ExecuteSqlRawAsync(query, parameters);
                return RedirectToAction("Details", id);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Une erreur est survenue. Veuillez réessayer.");
                return View();
            }
        }
    }
}
