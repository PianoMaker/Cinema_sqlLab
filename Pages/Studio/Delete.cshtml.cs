using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cinema4.Data;
using Cinema4.Models;

namespace Cinema4.Pages.Studio
{
    public class DeleteModel : PageModel
    {
        private readonly Cinema4.Data.Cinema4Context _context;

        public DeleteModel(Cinema4.Data.Cinema4Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Studio Studio { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studio = await _context.Studios.FirstOrDefaultAsync(m => m.Id == id);

            if (studio == null)
            {
                return NotFound();
            }
            else
            {
                Studio = studio;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studio = await _context.Studios.FindAsync(id);
            if (studio != null)
            {
                Studio = studio;
                _context.Studios.Remove(Studio);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
