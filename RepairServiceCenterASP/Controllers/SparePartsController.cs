using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RepairServiceCenterASP.Data;
using RepairServiceCenterASP.Models;

namespace RepairServiceCenterASP.Controllers
{
    public class SparePartsController : Controller
    {
        private readonly RepairServiceCenterContext _context;

        public SparePartsController(RepairServiceCenterContext context)
        {
            _context = context;
        }

        // GET: SpareParts
        public async Task<IActionResult> Index()
        {
            var repairServiceCenterContext = _context.SpareParts.Include(s => s.RepairedModel).Include(s => s.TypeOfFault);
            return View(await repairServiceCenterContext.ToListAsync());
        }

        // GET: SpareParts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparePart = await _context.SpareParts
                .Include(s => s.RepairedModel)
                .Include(s => s.TypeOfFault)
                .FirstOrDefaultAsync(m => m.SparePartId == id);
            if (sparePart == null)
            {
                return NotFound();
            }

            return View(sparePart);
        }

        // GET: SpareParts/Create
        public IActionResult Create()
        {
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "Name");
            ViewData["TypeOfFaultId"] = new SelectList(_context.TypeOfFaults, "TypeOfFaultId", "Name");
            return View();
        }

        // POST: SpareParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SparePartId,Name,Functions,Price,RepairedModelId,TypeOfFaultId")] SparePart sparePart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sparePart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "Name", sparePart.RepairedModelId);
            ViewData["TypeOfFaultId"] = new SelectList(_context.TypeOfFaults, "TypeOfFaultId", "Name", sparePart.TypeOfFaultId);
            return View(sparePart);
        }

        // GET: SpareParts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparePart = await _context.SpareParts.FindAsync(id);
            if (sparePart == null)
            {
                return NotFound();
            }
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "Name", sparePart.RepairedModelId);
            ViewData["TypeOfFaultId"] = new SelectList(_context.TypeOfFaults, "TypeOfFaultId", "Name", sparePart.TypeOfFaultId);
            return View(sparePart);
        }

        // POST: SpareParts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SparePartId,Name,Functions,Price,RepairedModelId,TypeOfFaultId")] SparePart sparePart)
        {
            if (id != sparePart.SparePartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sparePart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SparePartExists(sparePart.SparePartId))
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
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "Name", sparePart.RepairedModelId);
            ViewData["TypeOfFaultId"] = new SelectList(_context.TypeOfFaults, "TypeOfFaultId", "Name", sparePart.TypeOfFaultId);
            return View(sparePart);
        }

        // GET: SpareParts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparePart = await _context.SpareParts
                .Include(s => s.RepairedModel)
                .Include(s => s.TypeOfFault)
                .FirstOrDefaultAsync(m => m.SparePartId == id);
            if (sparePart == null)
            {
                return NotFound();
            }

            return View(sparePart);
        }

        // POST: SpareParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sparePart = await _context.SpareParts.FindAsync(id);
            _context.SpareParts.Remove(sparePart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SparePartExists(int id)
        {
            return _context.SpareParts.Any(e => e.SparePartId == id);
        }
    }
}
