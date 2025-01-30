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
    public class IndexModel : PageModel
    {
        private readonly Cinema4.Data.Cinema4Context _context;

        public IndexModel(Cinema4.Data.Cinema4Context context)
        {
            _context = context;
        }

        public IList<Models.Film> Films { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Films = await _context.Films
                .Include(f => f.Director)
                .Include(f => f.Genre)
                .Include(f => f.Studio).ToListAsync();
        }
    }
}
