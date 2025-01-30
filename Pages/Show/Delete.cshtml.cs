using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Cinema4.Data;
using Cinema4.Models;

namespace Cinema4.Pages.Show
{
    public class DeleteModel : PageModel
    {
        private readonly Cinema4.Data.Cinema4Context _context;

        public DeleteModel(Cinema4.Data.Cinema4Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Show Show { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Shows.FirstOrDefaultAsync(m => m.Id == id);

            if (show == null)
            {
                return NotFound();
            }
            else
            {
                Show = show;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Shows.FindAsync(id);
            if (show != null)
            {
                Show = show;
                _context.Shows.Remove(Show);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
