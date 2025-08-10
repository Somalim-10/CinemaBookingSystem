using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CinemaBookingSystem.Data;
using CinemaBookingSystem.Models;

namespace CinemaBookingSystem.Pages.ShowTimes
{
    public class DetailsModel : PageModel
    {
        private readonly CinemaBookingSystem.Data.ApplicationDbContext _context;

        public DetailsModel(CinemaBookingSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Showtime Showtime { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showtime = await _context.Showtimes.FirstOrDefaultAsync(m => m.Id == id);
            if (showtime == null)
            {
                return NotFound();
            }
            else
            {
                Showtime = showtime;
            }
            return Page();
        }
    }
}
