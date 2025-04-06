using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDBContext _context; //readonly field

        public EventsController(ApplicationDBContext context) //parameterised contructor 
        {
            _context = context;
        }

        public async Task<IActionResult> Index() //asynchronous task method
        {
            var events = await _context.Events.ToListAsync();
            return View(events);
        }


        public IActionResult CreateEvents()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvents(Events events)
        {
            if (ModelState.IsValid)
            {
                _context.Add(events);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(events);
        }

        public async Task<IActionResult> EventsDetails(int id)
        {
            var events = await _context.Events.FirstOrDefaultAsync(m => m.eventsID == id);

            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        public async Task<IActionResult> DeleteEvents(int id)
        {
            var events = await _context.Events.FirstOrDefaultAsync(m => m.eventsID == id);

            if (events == null)
            {
                return NotFound();
            }

            return View(events);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {

            var events = await _context.Events.FindAsync(id);
            _context.Events.Remove(events);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool EventsExists(int id)
        {
            return _context.Events.Any(e => e.eventsID == id);
        }

        public async Task<IActionResult> EditEvents(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var events = await _context.Events.FindAsync(id);

            if (id == null)
            {
                return NotFound();
            }

            return View(events);
        }


        [HttpPost]

        public async Task<IActionResult> EditEvents(int id, Events events)
        {
            if (id != events.eventsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(events);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsExists(events.eventsID))
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
