using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairServiceCenterASP.Data;
using RepairServiceCenterASP.Models;
using RepairServiceCenterASP.Services;
using RepairServiceCenterASP.ViewModels;

namespace RepairServiceCenterASP.Controllers
{
    public class RepairedModelsController : Controller
    {
        private readonly RepairServiceCenterContext _context;
        private readonly ICachingModel<RepairedModel> _cachingModel;
        private const string KEY_CACHE = "RepairedModel50";
        private const int PAGE_SIZE = 10;

        public RepairedModelsController(RepairServiceCenterContext context, 
                                        ICachingModel<RepairedModel> cachingModel)
        {
            _context = context;
            _cachingModel = cachingModel;
        }

        // GET: RepairedModels
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 300)]
        public IActionResult Index(int page = 1)
        {
            int pageSize = 10;
            // Разбиение на страницы
            var count = _cachingModel.ReadAllCache(KEY_CACHE).Count();
            var rModels = _cachingModel.ReadAllCache(KEY_CACHE, count, page, pageSize);

            RepairedViewModels viewModels = new RepairedViewModels()
            {
                RepairedModels = rModels,
                PageViewModel = new PageViewModel(count, page, pageSize)
            };
            return View(viewModels);
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RepairedModelId,Name,Type,Manufacturer," +
                                                "TechSpecification,Features")] RepairedModel repairedModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(repairedModel);
                await Task.Run(() =>
                {
                    _context.SaveChangesAsync();
                    _cachingModel.RefreshCache(KEY_CACHE);
                });
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RepairedModelId,Name,Type,Manufacturer," +
                                              "TechSpecification,Features")] RepairedModel repairedModel)
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
                    await Task.Run(() =>
                    {
                        _context.SaveChangesAsync();
                        _cachingModel.RefreshCache(KEY_CACHE);
                    });
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
            await Task.Run(() =>
            {
                _context.SaveChangesAsync();
                _cachingModel.RefreshCache(KEY_CACHE);
            });
            return RedirectToAction(nameof(Index));
        }

        private bool RepairedModelExists(int id)
        {
            return _context.RepairedModels.Any(e => e.RepairedModelId == id);
        }
    }
}
