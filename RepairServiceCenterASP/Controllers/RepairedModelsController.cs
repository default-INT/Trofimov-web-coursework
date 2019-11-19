using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RepairServiceCenterASP.Data;
using RepairServiceCenterASP.Models;
using RepairServiceCenterASP.Services;

namespace RepairServiceCenterASP.Controllers
{
    public class RepairedModelsController : Controller
    {
        private readonly RepairServiceCenterContext _context;
        private readonly ICachingModel<RepairedModel> _cachingModel;
        private const string KEY_CACHE = "RepairedModel50";
        private const int PAGE_SIZE = 10;

        public RepairedModelsController(RepairServiceCenterContext context, ICachingModel<RepairedModel> cachingModel)
        {
            _context = context;
            _cachingModel = cachingModel;
        }

        // GET: RepairedModels
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public IActionResult Index(int page = 1)
        {
            // Разбиение на страницы
            var count = _cachingModel.ReadAllCache(KEY_CACHE).Count();
            var rModels = _cachingModel.ReadAllCache(KEY_CACHE);
                                       //.Skip((page - 1) * PAGE_SIZE)
                                       //.Take(PAGE_SIZE);

            return View(rModels);
        }

        // GET: RepairedModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairedModel = await _context.RepairedModels
                .FirstOrDefaultAsync(m => m.RepairedModelId == id);
            if (repairedModel == null)
            {
                return NotFound();
            }

            return View(repairedModel);
        }

        // GET: RepairedModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RepairedModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RepairedModelId,Name,Type,Manufacturer,TechSpecification,Features")] RepairedModel repairedModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(repairedModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(repairedModel);
        }

        // GET: RepairedModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairedModel = await _context.RepairedModels.FindAsync(id);
            if (repairedModel == null)
            {
                return NotFound();
            }
            return View(repairedModel);
        }

        // POST: RepairedModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RepairedModelId,Name,Type,Manufacturer,TechSpecification,Features")] RepairedModel repairedModel)
        {
            if (id != repairedModel.RepairedModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repairedModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepairedModelExists(repairedModel.RepairedModelId))
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
            return View(repairedModel);
        }

        // GET: RepairedModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repairedModel = await _context.RepairedModels
                .FirstOrDefaultAsync(m => m.RepairedModelId == id);
            if (repairedModel == null)
            {
                return NotFound();
            }

            return View(repairedModel);
        }

        // POST: RepairedModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repairedModel = await _context.RepairedModels.FindAsync(id);
            _context.RepairedModels.Remove(repairedModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepairedModelExists(int id)
        {
            return _context.RepairedModels.Any(e => e.RepairedModelId == id);
        }
    }
}
