using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace Guia01.entidades
{ // inicio del namespace
    public class Entidades
    {

    }

    
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Display(Name = "Nombre de Categoría")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Descripcion { get; set; }

        // Propiedad de navegación: una categoría tiene muchos productos
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();

    }

    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150)]
        [Display(Name = "Nombre del Producto")]
        public string Nombre { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, 999999.99, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Display(Name = "Cantidad en Stock")]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        // Clave foránea
        [Required]
        [Display(Name = "Categoría")]
        public int CategoriaId { get; set; }

        // Propiedad de navegación hacia Categoria
        public Categoria? Categoria { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación explícita 1:N
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Datos semilla
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nombre = "Electrónica", Descripcion = "Gadgets y dispositivos" },
                new Categoria { Id = 2, Nombre = "Ropa", Descripcion = "Prendas de vestir" },
                new Categoria { Id = 3, Nombre = "Alimentación", Descripcion = "Productos alimenticios" }
            );

            modelBuilder.Entity<Producto>().HasData(
                new Producto { Id = 1, Nombre = "Laptop HP", Precio = 12500m, Stock = 15, CategoriaId = 1 },
                new Producto { Id = 2, Nombre = "Audífonos", Precio = 499.99m, Stock = 40, CategoriaId = 1 },
                new Producto { Id = 3, Nombre = "Camiseta XL", Precio = 199m, Stock = 100, CategoriaId = 2 }
            );
        }
    }





} // fin del namespace
