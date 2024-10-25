namespace ProyectoTFG.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ejercicio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ejercicios", "Imagen_Ejercicio", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ejercicios", "Imagen_Ejercicio");
        }
    }
}
