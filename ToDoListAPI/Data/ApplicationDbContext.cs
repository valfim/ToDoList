using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Models;

namespace ToDoListAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet que representa la tabla TodoTasks en la base de datos
        public DbSet<TodoTask> TodoTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la tabla de tareas
            modelBuilder.Entity<TodoTask>(entity =>
            {
                entity.HasKey(e => e.Id); // Definir la clave primaria
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100); // El título es requerido y tiene un máximo de 100 caracteres
                entity.Property(e => e.Description).HasMaxLength(500); // La descripción puede tener hasta 500 caracteres
                entity.Property(e => e.IsCompleted).IsRequired(); // Campo obligatorio
                entity.Property(e => e.CreatedAt).IsRequired(); // Campo obligatorio
            });
        }
    }
}

