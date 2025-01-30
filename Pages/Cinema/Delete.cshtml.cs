using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cinema4.Data;
using Cinema4.Models;

namespace Cinema4.Pages.Cinema
{
    public class DeleteModel : PageModel
    {
        private readonly Cinema4.Data.Cinema4Context _context;

        public DeleteModel(Cinema4.Data.Cinema4Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Cinema Cinema { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinema = await _context.Cinemas.FirstOrDefaultAsync(m => m.Id == id);

            if (cinema == null)
            {
                return NotFound();
            }
            else
            {
                Cinema = cinema;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinema = await _context.Cinemas.FindAsync(id);
            if (cinema != null)
            {
                Cinema = cinema;
                _context.Cinemas.Remove(Cinema);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
