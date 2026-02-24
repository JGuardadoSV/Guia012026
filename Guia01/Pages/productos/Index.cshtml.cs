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
    public class IndexModel : PageModel
    {
        private readonly Guia01.entidades.AppDbContext _context;

        public IndexModel(Guia01.entidades.AppDbContext context)
        {
            _context = context;
        }

        public IList<Producto> Producto { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Producto = await _context.Productos
                .Include(p => p.Categoria).ToListAsync();
        }
    }
}
