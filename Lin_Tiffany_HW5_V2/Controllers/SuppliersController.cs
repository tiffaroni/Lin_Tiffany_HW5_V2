using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lin_Tiffany_HW5_V2.DAL;
using Lin_Tiffany_HW5_V2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lin_Tiffany_HW5_V2.Controllers
{
    //HAVE to be an admin to access the entire controller and create/manage suppliers
    //you'll get an error is you're not in that role
    [Authorize(Roles = "Admin")]
    public class SuppliersController : Controller
    {
        private readonly AppDbContext _context;

        public SuppliersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index()
        {
              return _context.Supplier != null ? 
                          View(await _context.Supplier.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Supplier'  is null.");
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Supplier == null)
            {
                return NotFound();
            }

            //find the department in the database
            Supplier supplier = await _context.Suppliers
                .Include(d => d.Products)
                .FirstOrDefaultAsync(m => m.SupplierID == id);


            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SupplierID,SupplierName,Email,PhoneNumber")] Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Supplier == null)
            {
                return NotFound();
            }

            //find the department in the database
            Supplier supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SupplierID,SupplierName,Email,PhoneNumber")] Supplier supplier)
        {
            if (id != supplier.SupplierID)
            {
                return NotFound();
            }

            //if the user messed up, send them back to the view to try again
            if (ModelState.IsValid == false)
            {
                return View(supplier);
            }

            //if code gets this far, make the updates
            try
            {
                _context.Update(supplier);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return View("Error", new String[] { "There was a problem editing this supplier.", ex.Message });
            }

            //send the user back to the view with all the departments
            return RedirectToAction(nameof(Index));
        }


        private bool SupplierExists(int id)
        {
          return (_context.Supplier?.Any(e => e.SupplierID == id)).GetValueOrDefault();
        }
    }
}
