using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinema4.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace Cinema4.Pages
{

    public class BuyModel : PageModel
    {
        private readonly Cinema4.Data.Cinema4Context _context;

        public BuyModel(Cinema4.Data.Cinema4Context context)
        {
            _context = context;
        }

        public Models.Show Show { get; set; }
        
        public string ErrorMessage { get; set; } = string.Empty;

        [BindProperty] 
        public string Name { get; set; }
        [BindProperty]
        public string SurName { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Shows
                .Include(s => s.Film)
                .Include(s => s.Cinema)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (show == null)
            {
                ErrorMessage = "Сеансу не знайдено";
                return Page();
            }
            else if(show.SeatsLeft<1)
            {
                ErrorMessage = "На цей сеанс немає вільних місць";
                return Page();
            }
            Show = show;            
            
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var show = _context.Shows
                .Include(s => s.Film)
                .Include(s => s.Cinema)
                .FirstOrDefault(s => s.Id == id);
            if (show == null || show.SeatsLeft < 1)
            {
                return OnPostBack();
            }            
            Show = show;
            Show.SeatsLeft--;
            _context.SaveChanges();
            RedirectToPage();

            var name = Name;
            var surname = SurName;
            var cinemaName = Show?.Cinema?.Name;
            var filmName = Show?.Film?.Name;
            var dateTime = Show?.DateTime.ToString("dd.MM.yyyy HH:mm");


            var document = new PdfDocument();
            document.Info.Title = "Ticket";

            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 12);

            double leftMargin = 56.7; // 2 см в точках
            var titleFont = new XFont("Verdana", 14, XFontStyleEx.Bold); // Жирний шрифт для заголовка
            var redBrush = new XSolidBrush(XColor.FromArgb(139, 0, 0)); // Темночервоний колір

            gfx.DrawString("Запрошення на сеанс", titleFont, redBrush, new XRect(leftMargin, 20, page.Width - leftMargin, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Name: {name}", font, XBrushes.Black, new XRect(leftMargin, 40, page.Width - leftMargin, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Surname: {surname}", font, XBrushes.Black, new XRect(leftMargin, 60, page.Width - leftMargin, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Cinema: {cinemaName}", font, XBrushes.Black, new XRect(leftMargin, 80, page.Width - leftMargin, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Film: \"{filmName}\"", font, XBrushes.Black, new XRect(leftMargin, 100, page.Width - leftMargin, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Date and Time: {dateTime}", font, XBrushes.Black, new XRect(leftMargin, 120, page.Width - leftMargin, page.Height), XStringFormats.TopLeft);
            // Збереження PDF в пам'яті
            using (var stream = new MemoryStream())
            {
                document.Save(stream, false);
                
                return File(stream.ToArray(), "application/pdf", "Ticket.pdf");
                
                
                
            }
            

        }
        public IActionResult OnPostBack()
        {
            
            return RedirectToPage("/index");
        }
    }
}
