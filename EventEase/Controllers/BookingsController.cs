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

    }
}
