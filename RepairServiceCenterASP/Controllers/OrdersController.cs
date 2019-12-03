using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RepairServiceCenterASP.Data;
using RepairServiceCenterASP.Models;
using RepairServiceCenterASP.ViewModels;

namespace RepairServiceCenterASP.Controllers
{
    public class OrdersController : Controller
    {
        private readonly RepairServiceCenterContext _context;

        public OrdersController(RepairServiceCenterContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 20;

            var source = _context.Orders.Include(o => o.Employee)
                                                            .Include(o => o.RepairedModel)
                                                            .Include(o => o.ServicedStore)
                                                            .Include(o => o.TypeOfFault);

            int count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            OrdersViewModels ordersViewModels = new OrdersViewModels()
            {
                Orders = items,
                PageViewModel = pageViewModel
            };
            return View(ordersViewModels);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Employee)
                .Include(o => o.RepairedModel)
                .Include(o => o.ServicedStore)
                .Include(o => o.TypeOfFault)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FullName");
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "Name");
            ViewData["ServicedStoreId"] = new SelectList(_context.ServicedStores, "ServicedStoreId", "Name");
            ViewData["TypeOfFaultId"] = new SelectList(_context.TypeOfFaults, "TypeOfFaultId", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,DateOrder,ReturnDate,FullNameCustumer,RepairedModelId," +
            "TypeOfFaultId,ServicedStoreId,GuaranteeMark,GuaranteePeriod,Price,EmployeeId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FullName", order.EmployeeId);
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "Name", order.RepairedModelId);
            ViewData["ServicedStoreId"] = new SelectList(_context.ServicedStores, "ServicedStoreId", "Name", order.ServicedStoreId);
            ViewData["TypeOfFaultId"] = new SelectList(_context.TypeOfFaults, "TypeOfFaultId", "Name", order.TypeOfFaultId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FullName", order.EmployeeId);
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "Name", order.RepairedModelId);
            ViewData["ServicedStoreId"] = new SelectList(_context.ServicedStores, "ServicedStoreId", "Name", order.ServicedStoreId);
            ViewData["TypeOfFaultId"] = new SelectList(_context.TypeOfFaults, "TypeOfFaultId", "Name", order.TypeOfFaultId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,DateOrder,ReturnDate,FullNameCustumer," +
            "RepairedModelId,TypeOfFaultId,ServicedStoreId,GuaranteeMark,GuaranteePeriod,Price,EmployeeId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "FullName", order.EmployeeId);
            ViewData["RepairedModelId"] = new SelectList(_context.RepairedModels, "RepairedModelId", "Name", order.RepairedModelId);
            ViewData["ServicedStoreId"] = new SelectList(_context.ServicedStores, "ServicedStoreId", "Name", order.ServicedStoreId);
            ViewData["TypeOfFaultId"] = new SelectList(_context.TypeOfFaults, "TypeOfFaultId", "Name", order.TypeOfFaultId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Employee)
                .Include(o => o.RepairedModel)
                .Include(o => o.ServicedStore)
                .Include(o => o.TypeOfFault)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
