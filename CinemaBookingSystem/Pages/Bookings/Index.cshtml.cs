using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CinemaBookingSystem.Data;
using CinemaBookingSystem.Models;
using Microsoft.AspNetCore.Http;

namespace CinemaBookingSystem.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get; set; } = default!;

       public async Task<IActionResult> OnGetAsync()
{
    var userName = HttpContext.Session.GetString("UserName");
    var role = HttpContext.Session.GetString("Role");

    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(role))
    {
        return RedirectToPage("/AccessDenied");
    }

    var query = _context.Bookings
        .Include(b => b.Showtime)
        .ThenInclude(s => s.Movie)
        .AsQueryable();

    if (role != "Admin")
    {
        query = query.Where(b => b.UserName == userName);
    }

    Booking = await query.ToListAsync();
    return Page();
}

    }
}
