using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Guia01.entidades;

namespace Guia01.Pages.productos
{
    public class DetailsModel : PageModel
    {
        private readonly Guia01.entidades.AppDbContext _context;

        public DetailsModel(Guia01.entidades.AppDbContext context)
        {
            _context = context;
        }

        public Producto Producto { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FirstOrDefaultAsync(m => m.Id == id);

            if (producto is not null)
            {
                Producto = producto;

                return Page();
            }

            return NotFound();
        }
    }
}
