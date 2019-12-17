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
    public class EmployeesController : Controller
    {
        private readonly RepairServiceCenterContext _context;

        public EmployeesController(RepairServiceCenterContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string fullName, int? experience, int page = 1, 
            Employee.SortState sortOrder = Employee.SortState.FullNameAsc)
        {
            int pageSize = 10;

            IQueryable<Employee> source = _context.Employees.Include(e => e.Post);

            if (!String.IsNullOrEmpty(fullName))
                source = source.Where(e => e.FullName.Contains(fullName));

            if (experience != null)
                source = source.Where(e => e.Experience == experience.Value);

            source = EmployeesSort(source, sortOrder);

            int count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            EmployeesViewModel viewModel = new EmployeesViewModel()
            {
                EmployeesSort = new EmployeesSort(sortOrder),
                Employees = items,
                PageViewModel = new PageViewModel(count, page, pageSize),
                EmployeesFilter = new EmployeesFilter(fullName, experience)
            };

            return View(viewModel);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Post)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FullName,Experience,PostId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "Name", employee.PostId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "Name", employee.PostId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FullName,Experience,PostId")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            ViewData["PostId"] = new SelectList(_context.Posts, "PostId", "Name", employee.PostId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Post)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }

        private IQueryable<Employee> EmployeesSort(IQueryable<Employee> source, Employee.SortState sortOrder)
        {
            switch (sortOrder)
            {
                case Employee.SortState.FullNameAsc:
                    return source.OrderBy(e => e.FullName);

                case Employee.SortState.FullNameDesc:
                    return source.OrderByDescending(e => e.FullName);

                case Employee.SortState.ExperienceAsc:
                    return source.OrderBy(e => e.FullName);

                case Employee.SortState.ExperienceDesc:
                    return source.OrderByDescending(e => e.Experience);

                case Employee.SortState.PostAsc:
                    return source.OrderBy(e => e.Post.Name);

                case Employee.SortState.PostDesc:
                    return source.OrderByDescending(e => e.Post.Name);
                default:
                    return source;
            }
        }
    }
}
