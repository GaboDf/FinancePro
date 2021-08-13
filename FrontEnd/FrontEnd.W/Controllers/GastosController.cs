using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrontEnd.W.Models;

namespace FrontEnd.W.Controllers
{
    public class GastosController : Controller
    {
        private readonly financeproContext _context;

        public GastosController(financeproContext context)
        {
            _context = context;
        }

        // GET: Gastos
        public async Task<IActionResult> Index()
        {
            var financeproContext = _context.Gastos.Include(g => g.IdcategoriaNavigation).Include(g => g.IdclienteNavigation);
            return View(await financeproContext.ToListAsync());
        }

        // GET: Gastos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gastos = await _context.Gastos
                .Include(g => g.IdcategoriaNavigation)
                .Include(g => g.IdclienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gastos == null)
            {
                return NotFound();
            }

            return View(gastos);
        }

        // GET: Gastos/Create
        public IActionResult Create()
        {
            ViewData["Idcategoria"] = new SelectList(_context.Categorias, "Id", "Nombre");
            ViewData["Idcliente"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Gastos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Monto,Nombre,Descripcion,Fecha,Idcliente,Idcategoria")] Gastos gastos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gastos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcategoria"] = new SelectList(_context.Categorias, "Id", "Nombre", gastos.Idcategoria);
            ViewData["Idcliente"] = new SelectList(_context.AspNetUsers, "Id", "Id", gastos.Idcliente);
            return View(gastos);
        }

        // GET: Gastos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gastos = await _context.Gastos.FindAsync(id);
            if (gastos == null)
            {
                return NotFound();
            }
            ViewData["Idcategoria"] = new SelectList(_context.Categorias, "Id", "Nombre", gastos.Idcategoria);
            ViewData["Idcliente"] = new SelectList(_context.AspNetUsers, "Id", "Id", gastos.Idcliente);
            return View(gastos);
        }

        // POST: Gastos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Monto,Nombre,Descripcion,Fecha,Idcliente,Idcategoria")] Gastos gastos)
        {
            if (id != gastos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gastos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GastosExists(gastos.Id))
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
            ViewData["Idcategoria"] = new SelectList(_context.Categorias, "Id", "Nombre", gastos.Idcategoria);
            ViewData["Idcliente"] = new SelectList(_context.AspNetUsers, "Id", "Id", gastos.Idcliente);
            return View(gastos);
        }

        // GET: Gastos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gastos = await _context.Gastos
                .Include(g => g.IdcategoriaNavigation)
                .Include(g => g.IdclienteNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gastos == null)
            {
                return NotFound();
            }

            return View(gastos);
        }

        // POST: Gastos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gastos = await _context.Gastos.FindAsync(id);
            _context.Gastos.Remove(gastos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GastosExists(int id)
        {
            return _context.Gastos.Any(e => e.Id == id);
        }
    }
}
