using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Plato> Platos { get; set; }
        public virtual DbSet<Porcion> Porciones { get; set; }
        public virtual DbSet<ClasificacionComida> ClasificacionComida { get; set; }
        public virtual DbSet<TipoComida> TipoComida { get; set; }
        public virtual DbSet<Disponibilidad> Disponibilidad { get; set; }
        public virtual DbSet<Bebida> Bebidas { get; set; }
        public virtual DbSet<TipoBebida> TipoBebida { get; set; }
        public virtual DbSet<ClasificacionBebida> ClasificacionBebida { get; set; }
        public virtual DbSet<Tamano> Tamano { get; set; }
        public virtual DbSet<MetodoPago> MetodoPagos { get; set; }
        public virtual DbSet<ListaPrecio> ListaPrecios { get; set; }
        public virtual DbSet<Promociones> Promociones { get; set;}

        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<ProductosPedido> ProductosPedidos { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer("name=conexion");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Delivery>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<ProductosPedido>().Property(t => t.Id).ValueGeneratedOnAdd();
        }
    }
}