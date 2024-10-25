using ProyectoTFG.Modelos;
using System.Data.Entity;

namespace ProyectoTFG.DAL
{
    public class Contexto : DbContext
    {
        public Contexto() : base("ProyectoTFG")
        {
            Database.SetInitializer<Contexto>(new DropCreateDatabaseIfModelChanges<Contexto>());
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Tipo_Persona> Tipos_Persona { get; set; }
        public DbSet<Citas> Citas { get; set; }
        public DbSet<Ejercicios> Ejercicios { get; set; }
        public DbSet<Entrenamiento> Entrenamientos { get; set; }
        public DbSet<Entrenamiento_Ejercicios> EntrenamientosEjercicios { get; set; }
        public DbSet<Historial> Historiales { get; set; }
        public DbSet<Notificaciones> Notificaciones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuraciones para las entidades
            modelBuilder.Entity<Citas>()
                .HasRequired(c => c.Persona) 
                .WithMany()
                .HasForeignKey(c => c.Persona_Id);

            modelBuilder.Entity<Historial>()
                .HasRequired(h => h.Persona)
                .WithMany()
                .HasForeignKey(h => h.Persona_Id);

            modelBuilder.Entity<Entrenamiento>()
                .HasRequired(e => e.Persona)
                .WithMany()
                .HasForeignKey(e => e.Persona_Id);

            modelBuilder.Entity<Notificaciones>()
                .HasRequired(n => n.Persona)
                .WithMany()
                .HasForeignKey(n => n.Persona_Id);

            modelBuilder.Entity<Entrenamiento_Ejercicios>()
                .HasRequired(ee => ee.Entrenamiento)
                .WithMany(e => e.EntrenamientoEjercicios)
                .HasForeignKey(ee => ee.Entrenamiento_Id);

            modelBuilder.Entity<Entrenamiento_Ejercicios>()
                .HasRequired(ee => ee.Ejercicios)
                .WithMany(ej => ej.EntrenamientoEjercicios)
                .HasForeignKey(ee => ee.Ejercicios_Id);

            modelBuilder.Entity<Tipo_Persona>()
                .ToTable("Tipo_Persona");
        }
    }
}
