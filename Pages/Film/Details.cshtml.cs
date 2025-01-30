using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cinema4.Data;
using Cinema4.Models;

namespace Cinema4.Pages.Film
{
    public class DetailsModel : PageModel
    {
        private readonly Cinema4.Data.Cinema4Context _context;

        public DetailsModel(Cinema4.Data.Cinema4Context context)
        {
            _context = context;
        }

        public Models.Film Film { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }
            else
            {
                Film = film;
            }
            return Page();
        }
    }
}
