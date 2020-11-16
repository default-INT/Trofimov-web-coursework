using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RepairServiceCenterASP.Data;
using RepairServiceCenterASP.Models;
using RepairServiceCenterASP.ViewModels;
using RepairServiceCenterASP.ViewModels.Filters;
using RepairServiceCenterASP.ViewModels.Sortings;

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
        public async Task<IActionResult> Index(int? model, string name, string methodRepair, string client,
            int page = 1, TypeOfFault.SortState sortOrder = TypeOfFault.SortState.NameAsc)
        {
            int pageSize = 20;

            IQueryable<TypeOfFault> source = _context.TypeOfFaults.Include(t => t.RepairedModel);

            if (model != null && model != 0)
                source = source.Where(t => t.RepairedModel.RepairedModelId == model.Value);

            if (!String.IsNullOrEmpty(name))
                source = source.Where(t => t.Name.Contains(name));

            if (!String.IsNullOrEmpty(methodRepair))
                source = source.Where(t => t.MethodRepair.Contains(methodRepair));
            
            if (!String.IsNullOrEmpty(client))
            {
                source = _context.Orders.Include(o => o.TypeOfFault)
                                        .Include(o => o.RepairedModel)
                                        .Where(o => o.FullNameCustumer.Contains(client))
                                        .Select(o => new TypeOfFault()
                                        {
                                            Name = o.TypeOfFault.Name,
                                            RepairedModelId = o.RepairedModelId.Value,
                                            RepairedModel = o.RepairedModel,
                                            MethodRepair = o.TypeOfFault.MethodRepair,
                                            WorkPrice = o.TypeOfFault.WorkPrice
                                        });
            }

            source = TypesOfFaultsSort(source, sortOrder);

            int count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var models = await _context.RepairedModels.ToListAsync();

            var viewModel = new TypeOfFaultsViewModel()
            {
                TypeOfFaults = items,
                TypesOfFaultsFilter = new TypesOfFaultsFilter(models, model, name, methodRepair, client),
                TypesOfFaultsSort = new TypesOfFaultsSort(sortOrder),
                PageViewModel = new PageViewModel(count, page, pageSize)
            };

            return View(viewModel);
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
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "Name");
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
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "Name", typeOfFault.RepairedModelId);
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
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "Name", typeOfFault.RepairedModelId);
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
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "Name", typeOfFault.RepairedModelId);
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

        private IQueryable<TypeOfFault> TypesOfFaultsSort(IQueryable<TypeOfFault> typesOfFaults, TypeOfFault.SortState sortOrder)
        {
            switch (sortOrder)
            {
                case TypeOfFault.SortState.NameAsc:
                    return typesOfFaults.OrderBy(t => t.Name);

                case TypeOfFault.SortState.NameDesc:

                    return typesOfFaults.OrderByDescending(t => t.Name);

                case TypeOfFault.SortState.RepairedModelAsc:
                    return typesOfFaults.OrderBy(t => t.RepairedModel.Name);

                case TypeOfFault.SortState.RepairedModelDesc:
                    return typesOfFaults.OrderByDescending(t => t.RepairedModel.Name);

                case TypeOfFault.SortState.MethodRepairAsc:
                    return typesOfFaults.OrderBy(t => t.MethodRepair);

                case TypeOfFault.SortState.MethodRepairDesc:
                    return typesOfFaults.OrderByDescending(t => t.MethodRepair);

                case TypeOfFault.SortState.WorkPriceAsc:
                    return typesOfFaults.OrderBy(t => t.WorkPrice);

                case TypeOfFault.SortState.WorkPriceDesc:
                    return typesOfFaults.OrderByDescending(t => t.WorkPrice);

                default:
                    return typesOfFaults.OrderBy(t => t.Name);
            }
        }
    }
}
