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
using Lin_Tiffany_HW5_V2.Utilities;
using Microsoft.AspNetCore.Identity;

namespace Lin_Tiffany_HW5_V2.Controllers
{
    //only logged-in users can access registrations
    //kind of like orders in your HW4
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrdersController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public IActionResult Index()
        {
            //return _context.Order != null ? 
            //            View(await _context.Order.ToListAsync()) :
            //            Problem("Entity set 'AppDbContext.Order'  is null.");



            //Set up a list of registrations to display
            List<Order> orders;

            //User.IsInRole -- they see ALL registrations and detail
            if (User.IsInRole("Admin"))
            {
                orders = _context.Orders
                        .Include(r => r.OrderDetails)
                            .ThenInclude(od => od.Product)
                        .ToList();
            }
            else //user is a customer, so only display their records
            //registration is assocated with a particular user (look on the registration model class)
            //every logged in user is allowed to access index page, but their results will be different
            {
                orders = _context.Orders
                                .Include(r => r.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                .Where(r => r.User.UserName == User.Identity.Name)
                                .ToList();
            }

            //
            return View(orders);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //the user did not specify a registration to view
            if (id == null)
            {
                return View("Error", new String[] { "Please specify a order to view!" });
            }

            //find the registration in the database
            Order order = await _context.Orders
                                .Include(r => r.OrderDetails)
                                    .ThenInclude(od => od.Product)
                                .Include(r => r.User)
                                .FirstOrDefaultAsync(m => m.OrderID == id);

            //registration was not found in the database
            if (order == null)
            {
                return View("Error", new String[] { "This order was not found!" });
            }

            //make sure this registration belongs to this user
            if (User.IsInRole("Customer") && order.User.UserName != User.Identity.Name)
            {
                return View("Error", new String[] { "This is not your order!  Don't be such a snoop!" });
            }

            //Send the user to the details page
            return View(order);
        }


        // GET: Orders/Create
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create()
        {
            if (User.IsInRole("Customer"))
            {
                Order ord = new Order();
                ord.User = await _userManager.FindByNameAsync(User.Identity.Name);
                return View(ord);
            }
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,OrderNumber,OrderDate,OrderNotes")] Order order)
        {

            //Find the next registration number from the utilities class
            order.OrderNumber = Utilities.GenerateNextOrderNumber.GetNextOrderNumber(_context);

            //Set the date of this order
            order.OrderDate = DateTime.Now;

            var currentUser = await _userManager.GetUserAsync(User);

            order.User = currentUser;

            ////Associate the registration with the logged-in customer
            //order.User = await _userManager.FindByNameAsync(order.User.UserName);

            //if code gets this far, add the registration to the database
            _context.Add(order);
            await _context.SaveChangesAsync();

            //send the user on to the action that will allow them to 
            //create a registration detail.  Be sure to pass along the RegistrationID
            //that you created when you added the registration to the database above
            return RedirectToAction("Create", "OrderDetails", new { orderID = order.OrderID });
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            //find the registration in the database, and be sure to include details
            Order order = _context.Orders
                        .Include(r => r.OrderDetails)
                        .ThenInclude(r => r.Product)
                        .Include(r => r.User)
                        .FirstOrDefault(r => r.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,OrderNumber,OrderDate,OrderNotes")] Order order)
        {
            //this is a security measure to make sure the user is editing the correct registration
            if (id != order.OrderID)
            {
                return View("Error", new String[] { "There was a problem editing this order. Try again!" });
            }

            //if there is something wrong with this order, try again
            if (ModelState.IsValid == false)
            {
                return View(order);
            }

            //if code gets this far, update the record
            try
            {
                //find the record in the database
                Order dbOrder = _context.Orders.Find(order.OrderID);

                //update the notes
                dbOrder.OrderNotes = order.OrderNotes;

                _context.Update(dbOrder);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return View("Error", new String[] { "There was an error updating this order!", ex.Message });
            }

            //send the user to the Registrations Index page.
            return RedirectToAction(nameof(Index));
        }

        //// GET: Orders/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Order == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Order
        //        .FirstOrDefaultAsync(m => m.OrderID == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //// POST: Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Order == null)
        //    {
        //        return Problem("Entity set 'AppDbContext.Order'  is null.");
        //    }
        //    var order = await _context.Order.FindAsync(id);
        //    if (order != null)
        //    {
        //        _context.Order.Remove(order);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.OrderID == id)).GetValueOrDefault();
        }
    }
}
