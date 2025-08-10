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
    public class IndexModel : PageModel
    {
        private readonly CinemaBookingSystem.Data.ApplicationDbContext _context;

        public IndexModel(CinemaBookingSystem.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Showtime> Showtime { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Showtime = await _context.Showtimes
                .Include(s => s.Movie).ToListAsync();
        }
    }
}
