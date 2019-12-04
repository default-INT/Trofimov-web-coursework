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
using RepairServiceCenterASP.ViewModels.Filters;
using RepairServiceCenterASP.ViewModels.Sortings;

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
        public async Task<IActionResult> Index(DateTime? dateOrder, string fullNameCust, int? employee,
            bool? guarantee, int page = 1, Order.SortState sortOrder = Order.SortState.DateOrderDesc)
        {
            int pageSize = 20;

            IQueryable<Order> source = _context.Orders.Include(o => o.Employee)
                                        .Include(o => o.RepairedModel)
                                        .Include(o => o.ServicedStore)
                                        .Include(o => o.TypeOfFault);

            if (dateOrder != null)
                source = source.Where(o => o.DateOrder.Year == dateOrder.Value.Year
                                        && o.DateOrder.Month == dateOrder.Value.Month
                                        && o.DateOrder.Day == dateOrder.Value.Day);

            if (!String.IsNullOrEmpty(fullNameCust))
                source = source.Where(o => o.FullNameCustumer.Contains(fullNameCust));
           
            if (employee != null && employee != 0)
                source = source.Where(o => o.EmployeeId == employee);

            if (guarantee != null)
                source = source.Where(o => o.GuaranteeMark == guarantee.Value);

            source = OrdersSort(source, sortOrder);

            int count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var employees = await _context.Employees.ToListAsync();

            OrdersViewModel ordersViewModels = new OrdersViewModel()
            {
                OrdersSort = new OrdersSort(sortOrder),
                OrdersFilter = new OrdersFilter(dateOrder, fullNameCust, employees, employee, guarantee),
                Orders = items,
                PageViewModel = new PageViewModel(count, page, pageSize)
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
            "TypeOfFaultId,ServicedStoreId,GuaranteeMark,GuaranteePeriod,EmployeeId")] Order order)
        {
            order.Price = (double)_context.TypeOfFaults.Where(t => t.TypeOfFaultId == order.TypeOfFaultId)
                                               .Select(t => t.WorkPrice)
                                               .FirstOrDefault();
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
            "RepairedModelId,TypeOfFaultId,ServicedStoreId,GuaranteeMark,GuaranteePeriod,EmployeeId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.Price = (double)_context.TypeOfFaults.Where(t => t.TypeOfFaultId == order.TypeOfFaultId)
                                               .Select(t => t.WorkPrice)
                                               .FirstOrDefault();
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

        private IQueryable<Order> OrdersSort(IQueryable<Order> orders, Order.SortState sortOrder)
        {
            switch (sortOrder)
            {
                case Order.SortState.DateOrderAsc:
                    return orders.OrderBy(o => o.DateOrder);

                case Order.SortState.DateOrderDesc:
                    return orders.OrderByDescending(o => o.DateOrder);

                case Order.SortState.ReturnDateAsc:
                    return orders.OrderBy(o => o.ReturnDate);

                case Order.SortState.ReturnDateDesc:
                    return orders.OrderByDescending(o => o.ReturnDate);

                case Order.SortState.FullNameCustAsc:
                    return orders.OrderBy(o => o.FullNameCustumer);

                case Order.SortState.FullNameCustDesc:
                    return orders.OrderByDescending(o => o.FullNameCustumer);

                case Order.SortState.RepModelAsc:
                    return orders.OrderBy(o => o.RepairedModel.Name);

                case Order.SortState.RepModelDesc:
                    return orders.OrderByDescending(o => o.RepairedModel.Name);

                case Order.SortState.TypeOfFaultAsc:
                    return orders.OrderBy(o => o.TypeOfFault.Name);

                case Order.SortState.TypeOfFaultDesc:
                    return orders.OrderByDescending(o => o.TypeOfFault.Name);

                case Order.SortState.GuaranteeMarkAsc:
                    return orders.OrderBy(o => o.GuaranteeMark);

                case Order.SortState.GuaranteeMarkDesc:
                    return orders.OrderByDescending(o => o.GuaranteeMark);

                case Order.SortState.GuaranteePeriodAsc:
                    return orders.OrderBy(o => o.GuaranteePeriod);

                case Order.SortState.GuaranteePeriodDesc:
                    return orders.OrderByDescending(o => o.GuaranteePeriod);

                case Order.SortState.PriceAsc:
                    return orders.OrderBy(o => o.Price);

                case Order.SortState.PriceDesc:
                    return orders.OrderByDescending(o => o.Price);

                case Order.SortState.EmployeeAsc:
                    return orders.OrderBy(o => o.Employee.FullName);

                case Order.SortState.EmployeeDesc:
                    return orders.OrderByDescending(o => o.Employee.FullName);

                default:
                    return orders.OrderBy(o => o.DateOrder);
            }
        }
    }
}
