using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaBookingSystem.Data;
using CinemaBookingSystem.Models;
using Microsoft.AspNetCore.Http;

namespace CinemaBookingSystem.Pages.Bookings
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;

        public SelectList ShowTimeOptions { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return RedirectToPage("/AccessDenied");
            }

            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            Booking = booking;

            var showtimes = await _context.Showtimes
                .Include(s => s.Movie)
                .ToListAsync();

            ShowTimeOptions = new SelectList(
                showtimes.Select(s => new {
                    s.Id,
                    DisplayText = $"{s.Movie.Title} - {s.StartTime:dd-MM-yyyy HH:mm}"
                }),
                "Id", "DisplayText", Booking.ShowTimeId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return RedirectToPage("/AccessDenied");
            }

            if (!ModelState.IsValid)
            {
                var showtimes = await _context.Showtimes
                    .Include(s => s.Movie)
                    .ToListAsync();

                ShowTimeOptions = new SelectList(
                    showtimes.Select(s => new {
                        s.Id,
                        DisplayText = $"{s.Movie.Title} - {s.StartTime:dd-MM-yyyy HH:mm}"
                    }),
                    "Id", "DisplayText", Booking.ShowTimeId);

                return Page();
            }

            _context.Attach(Booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(Booking.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
