using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Guia01.entidades;

namespace Guia01.Pages.categorias
{
    public class CreateModel : PageModel
    {
        private readonly Guia01.entidades.AppDbContext _context;

        public CreateModel(Guia01.entidades.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Categoria Categoria { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categorias.Add(Categoria);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
