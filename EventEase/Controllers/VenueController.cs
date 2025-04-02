using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class VenueController : Controller
    {
        private readonly ApplicationDBContext _context; //readonly field
        
        public VenueController(ApplicationDBContext context) //parameterised contructor 
        {
            _context = context;
        }

        public async Task<IActionResult> Index() //asynchronous task method
        {
            var venues = await _context.Venue.ToListAsync();
            return View(venues);
        }

        public IActionResult CreateVenue()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateVenue(Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venue);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(venue);
        }

       
    }
}
