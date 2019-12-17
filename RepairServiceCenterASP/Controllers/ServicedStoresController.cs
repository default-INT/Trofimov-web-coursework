using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairServiceCenterASP.Data;
using RepairServiceCenterASP.Models;
using RepairServiceCenterASP.ViewModels;

namespace RepairServiceCenterASP.Controllers
{
    public class ServicedStoresController : Controller
    {
        private readonly RepairServiceCenterContext _context;

        public ServicedStoresController(RepairServiceCenterContext context)
        {
            _context = context;
        }

        // GET: ServicedStores
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var source = _context.ServicedStores;
            int count = await source.CountAsync();

            var stores = await source.Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();
            ServicedStoresViewModel storesViewModel = new ServicedStoresViewModel()
            {
                ServicedStores = stores,
                PageViewModel = new PageViewModel(count, page, pageSize)
            };
            return View(storesViewModel);
        }

        // GET: ServicedStores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicedStore = await _context.ServicedStores
                .FirstOrDefaultAsync(m => m.ServicedStoreId == id);
            if (servicedStore == null)
            {
                return NotFound();
            }

            return View(servicedStore);
        }

        // GET: ServicedStores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServicedStores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicedStoreId,Name,Address,PhoneNumber")] ServicedStore servicedStore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servicedStore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servicedStore);
        }

        // GET: ServicedStores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicedStore = await _context.ServicedStores.FindAsync(id);
            if (servicedStore == null)
            {
                return NotFound();
            }
            return View(servicedStore);
        }

        // POST: ServicedStores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicedStoreId,Name,Address,PhoneNumber")] ServicedStore servicedStore)
        {
            if (id != servicedStore.ServicedStoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicedStore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicedStoreExists(servicedStore.ServicedStoreId))
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
            return View(servicedStore);
        }

        // GET: ServicedStores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicedStore = await _context.ServicedStores
                .FirstOrDefaultAsync(m => m.ServicedStoreId == id);
            if (servicedStore == null)
            {
                return NotFound();
            }

            return View(servicedStore);
        }

        // POST: ServicedStores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicedStore = await _context.ServicedStores.FindAsync(id);
            _context.ServicedStores.Remove(servicedStore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicedStoreExists(int id)
        {
            return _context.ServicedStores.Any(e => e.ServicedStoreId == id);
        }
    }
}
