using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RepairServiceCenterASP.Data;
using RepairServiceCenterASP.Models;

namespace RepairServiceCenterASP.Controllers
{
    public class TypeOfFaultsController : Controller
    {
        private readonly RepairServiceCenterContext _context;

        public TypeOfFaultsController(RepairServiceCenterContext context)
        {
            _context = context;
        }

        // GET: TypeOfFaults
        public async Task<IActionResult> Index()
        {
            var repairServiceCenterContext = _context.TypeOfFaults.Include(t => t.RepairedModel);
            return View(await repairServiceCenterContext.ToListAsync());
        }

        // GET: TypeOfFaults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfFault = await _context.TypeOfFaults
                .Include(t => t.RepairedModel)
                .FirstOrDefaultAsync(m => m.TypeOfFaultId == id);
            if (typeOfFault == null)
            {
                return NotFound();
            }

            return View(typeOfFault);
        }

        // GET: TypeOfFaults/Create
        public IActionResult Create()
        {
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "RepairedModelId");
            return View();
        }

        // POST: TypeOfFaults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeOfFaultId,RepairedModelId,Name,MethodRepair,WorkPrice")] TypeOfFault typeOfFault)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeOfFault);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "RepairedModelId", typeOfFault.RepairedModelId);
            return View(typeOfFault);
        }

        // GET: TypeOfFaults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfFault = await _context.TypeOfFaults.FindAsync(id);
            if (typeOfFault == null)
            {
                return NotFound();
            }
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "RepairedModelId", typeOfFault.RepairedModelId);
            return View(typeOfFault);
        }

        // POST: TypeOfFaults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TypeOfFaultId,RepairedModelId,Name,MethodRepair,WorkPrice")] TypeOfFault typeOfFault)
        {
            if (id != typeOfFault.TypeOfFaultId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfFault);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfFaultExists(typeOfFault.TypeOfFaultId))
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
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "RepairedModelId", typeOfFault.RepairedModelId);
            return View(typeOfFault);
        }

        // GET: TypeOfFaults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeOfFault = await _context.TypeOfFaults
                .Include(t => t.RepairedModel)
                .FirstOrDefaultAsync(m => m.TypeOfFaultId == id);
            if (typeOfFault == null)
            {
                return NotFound();
            }

            return View(typeOfFault);
        }

        // POST: TypeOfFaults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeOfFault = await _context.TypeOfFaults.FindAsync(id);
            _context.TypeOfFaults.Remove(typeOfFault);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfFaultExists(int id)
        {
            return _context.TypeOfFaults.Any(e => e.TypeOfFaultId == id);
        }
    }
}
