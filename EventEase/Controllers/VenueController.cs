using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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

        public async Task<IActionResult> VenueDetails(int id)
        {
            var venue = await _context.Venue.FirstOrDefaultAsync(m => m.venueId == id);

            if (venue==null)
            {
                return NotFound();
            }

            return View(venue);
        }

        public async Task<IActionResult> DeleteVenue(int id)
        {
            var venue = await _context.Venue.FirstOrDefaultAsync(m => m.venueId == id);

            if (venue==null)
            {
                return NotFound();
            }

            return View(venue);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {

            var venue = await _context.Venue.FindAsync(id);
            _context.Venue.Remove(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool VenueExists(int id)
        {
            return _context.Venue.Any(e => e.venueId == id);
        }

        public async Task<IActionResult> EditVenue(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var venue = await _context.Venue.FindAsync(id);

            if (id==null)
            {
                return NotFound();
            }

            return View(venue);
        }


        [HttpPost]
        
        public async Task<IActionResult> EditVenue(int id, Venue venue)
        {
            if (id!=venue.venueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.venueId))
                    {
                        return NotFound();
                    }

                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToAction(nameof(Index));
        }
       
    }
}
