using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaBookingSystem.Data;
using CinemaBookingSystem.Models;

namespace CinemaBookingSystem.Pages.Bookings
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; } = new Booking();

        public SelectList ShowtimeOptions { get; private set; }

        public IActionResult OnGet()
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/AccessDenied");
            }

            var showtimes = _context.Showtimes.Include(s => s.Movie).ToList();
            ShowtimeOptions = new SelectList(
                showtimes.Select(s => new
                {
                    s.Id,
                    Text = $"{s.Movie.Title} - {s.StartTime:dd-MM-yyyy HH:mm}"
                }),
                "Id", "Text");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToPage("/AccessDenied");
            }

            Booking.UserName = userName;

            var showtimes = _context.Showtimes.Include(s => s.Movie).ToList();
            ShowtimeOptions = new SelectList(
                showtimes.Select(s => new
                {
                    s.Id,
                    Text = $"{s.Movie.Title} - {s.StartTime:dd-MM-yyyy HH:mm}"
                }),
                "Id", "Text");

            ModelState.Remove("Booking.Showtime");
            ModelState.Remove("Booking.UserName");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
