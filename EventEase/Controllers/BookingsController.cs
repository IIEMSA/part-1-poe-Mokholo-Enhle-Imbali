using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDBContext _context; //readonly field

        public BookingsController(ApplicationDBContext context) //parameterised contructor 
        {
            _context = context;
        }

        public async Task<IActionResult> Index() //asynchronous task method
        {
            var bookings = await _context.Bookings.ToListAsync();
            return View(bookings);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> Create(Bookings booking)
        {

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(booking);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var bookings = await _context.Bookings.FirstOrDefaultAsync(m => m.bookingsID == id);

            if (bookings==null)
            {
                return NotFound();
            }
            
            return View(bookings);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            var bookings = await _context.Bookings.FirstOrDefaultAsync(m=>m.bookingsID==id);

            if (bookings==null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(m => m.bookingsID == id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(int id)
        {
            return _context.Bookings.Any(e => e.bookingsID == id);

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.FindAsync(id);
            if (id==null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,Bookings booking)
        {
            if (id!=booking.bookingsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!BookingsExists(booking.bookingsID))
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

            return View(booking);
        }



    }
}
