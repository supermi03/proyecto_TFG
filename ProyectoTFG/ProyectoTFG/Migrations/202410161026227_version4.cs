namespace ProyectoTFG.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Administrador", "Persona_Id", "dbo.Persona");
            DropForeignKey("dbo.Cliente", "Persona_Id", "dbo.Persona");
            DropForeignKey("dbo.Monitor", "Persona_Id", "dbo.Persona");
            DropForeignKey("dbo.Citas", "Cliente_Id", "dbo.Cliente");
            DropForeignKey("dbo.Citas", "Monitor_Id", "dbo.Monitor");
            DropForeignKey("dbo.historial", "Cliente_Id", "dbo.Cliente");
            DropForeignKey("dbo.Entrenamiento", "Cliente_Id", "dbo.Cliente");
            DropForeignKey("dbo.Notificaciones", "Administrador_Persona_Id", "dbo.Administrador");
            DropIndex("dbo.Administrador", new[] { "Persona_Id" });
            DropIndex("dbo.Cliente", new[] { "Persona_Id" });
            DropIndex("dbo.Monitor", new[] { "Persona_Id" });
            DropPrimaryKey("dbo.Administrador");
            DropPrimaryKey("dbo.Cliente");
            DropPrimaryKey("dbo.Monitor");
            AddColumn("dbo.Administrador", "Fecha_Registro", c => c.DateTime(nullable: false));
            AddColumn("dbo.Administrador", "Nombre", c => c.String(nullable: false));
            AddColumn("dbo.Administrador", "Apellido", c => c.String(nullable: false));
            AddColumn("dbo.Administrador", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Administrador", "Telefono", c => c.String(nullable: false));
            AddColumn("dbo.Administrador", "Fecha_nacimiento", c => c.DateTime(nullable: false));
            AddColumn("dbo.Administrador", "Direccion", c => c.String(nullable: false));
            AddColumn("dbo.Administrador", "Password", c => c.String(nullable: false));
            AddColumn("dbo.Cliente", "Nombre", c => c.String(nullable: false));
            AddColumn("dbo.Cliente", "Apellido", c => c.String(nullable: false));
            AddColumn("dbo.Cliente", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Cliente", "Telefono", c => c.String(nullable: false));
            AddColumn("dbo.Cliente", "Fecha_nacimiento", c => c.DateTime(nullable: false));
            AddColumn("dbo.Cliente", "Direccion", c => c.String(nullable: false));
            AddColumn("dbo.Cliente", "Password", c => c.String(nullable: false));
            AddColumn("dbo.Monitor", "Fecha_Registro", c => c.DateTime(nullable: false));
            AddColumn("dbo.Monitor", "Nombre", c => c.String(nullable: false));
            AddColumn("dbo.Monitor", "Apellido", c => c.String(nullable: false));
            AddColumn("dbo.Monitor", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Monitor", "Telefono", c => c.String(nullable: false));
            AddColumn("dbo.Monitor", "Fecha_nacimiento", c => c.DateTime(nullable: false));
            AddColumn("dbo.Monitor", "Direccion", c => c.String(nullable: false));
            AddColumn("dbo.Monitor", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Administrador", "Persona_Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Cliente", "Persona_Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Monitor", "Persona_Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Administrador", "Persona_Id");
            AddPrimaryKey("dbo.Cliente", "Persona_Id");
            AddPrimaryKey("dbo.Monitor", "Persona_Id");
            AddForeignKey("dbo.Citas", "Cliente_Id", "dbo.Cliente", "Persona_Id", cascadeDelete: true);
            AddForeignKey("dbo.Citas", "Monitor_Id", "dbo.Monitor", "Persona_Id", cascadeDelete: true);
            AddForeignKey("dbo.historial", "Cliente_Id", "dbo.Cliente", "Persona_Id", cascadeDelete: true);
            AddForeignKey("dbo.Entrenamiento", "Cliente_Id", "dbo.Cliente", "Persona_Id", cascadeDelete: true);
            AddForeignKey("dbo.Notificaciones", "Administrador_Persona_Id", "dbo.Administrador", "Persona_Id");
            DropColumn("dbo.Administrador", "Administrador_Id");
            DropColumn("dbo.Cliente", "Cliente_Id");
            DropColumn("dbo.Monitor", "Monitor_Id");
            DropColumn("dbo.Monitor", "Especialidad");
            DropColumn("dbo.Monitor", "Fecha_Contratacion");
            DropTable("dbo.Persona");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Persona",
                c => new
                    {
                        Persona_Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Telefono = c.String(nullable: false),
                        Fecha_nacimiento = c.DateTime(nullable: false),
                        Direccion = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Persona_Id);
            
            AddColumn("dbo.Monitor", "Fecha_Contratacion", c => c.DateTime(nullable: false));
            AddColumn("dbo.Monitor", "Especialidad", c => c.String(nullable: false));
            AddColumn("dbo.Monitor", "Monitor_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Cliente", "Cliente_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Administrador", "Administrador_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Notificaciones", "Administrador_Persona_Id", "dbo.Administrador");
            DropForeignKey("dbo.Entrenamiento", "Cliente_Id", "dbo.Cliente");
            DropForeignKey("dbo.historial", "Cliente_Id", "dbo.Cliente");
            DropForeignKey("dbo.Citas", "Monitor_Id", "dbo.Monitor");
            DropForeignKey("dbo.Citas", "Cliente_Id", "dbo.Cliente");
            DropPrimaryKey("dbo.Monitor");
            DropPrimaryKey("dbo.Cliente");
            DropPrimaryKey("dbo.Administrador");
            AlterColumn("dbo.Monitor", "Persona_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Cliente", "Persona_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Administrador", "Persona_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Monitor", "Password");
            DropColumn("dbo.Monitor", "Direccion");
            DropColumn("dbo.Monitor", "Fecha_nacimiento");
            DropColumn("dbo.Monitor", "Telefono");
            DropColumn("dbo.Monitor", "Email");
            DropColumn("dbo.Monitor", "Apellido");
            DropColumn("dbo.Monitor", "Nombre");
            DropColumn("dbo.Monitor", "Fecha_Registro");
            DropColumn("dbo.Cliente", "Password");
            DropColumn("dbo.Cliente", "Direccion");
            DropColumn("dbo.Cliente", "Fecha_nacimiento");
            DropColumn("dbo.Cliente", "Telefono");
            DropColumn("dbo.Cliente", "Email");
            DropColumn("dbo.Cliente", "Apellido");
            DropColumn("dbo.Cliente", "Nombre");
            DropColumn("dbo.Administrador", "Password");
            DropColumn("dbo.Administrador", "Direccion");
            DropColumn("dbo.Administrador", "Fecha_nacimiento");
            DropColumn("dbo.Administrador", "Telefono");
            DropColumn("dbo.Administrador", "Email");
            DropColumn("dbo.Administrador", "Apellido");
            DropColumn("dbo.Administrador", "Nombre");
            DropColumn("dbo.Administrador", "Fecha_Registro");
            AddPrimaryKey("dbo.Monitor", "Persona_Id");
            AddPrimaryKey("dbo.Cliente", "Persona_Id");
            AddPrimaryKey("dbo.Administrador", "Persona_Id");
            CreateIndex("dbo.Monitor", "Persona_Id");
            CreateIndex("dbo.Cliente", "Persona_Id");
            CreateIndex("dbo.Administrador", "Persona_Id");
            AddForeignKey("dbo.Notificaciones", "Administrador_Persona_Id", "dbo.Administrador", "Persona_Id");
            AddForeignKey("dbo.Entrenamiento", "Cliente_Id", "dbo.Cliente", "Persona_Id");
            AddForeignKey("dbo.historial", "Cliente_Id", "dbo.Cliente", "Persona_Id");
            AddForeignKey("dbo.Citas", "Monitor_Id", "dbo.Monitor", "Persona_Id");
            AddForeignKey("dbo.Citas", "Cliente_Id", "dbo.Cliente", "Persona_Id");
            AddForeignKey("dbo.Monitor", "Persona_Id", "dbo.Persona", "Persona_Id");
            AddForeignKey("dbo.Cliente", "Persona_Id", "dbo.Persona", "Persona_Id");
            AddForeignKey("dbo.Administrador", "Persona_Id", "dbo.Persona", "Persona_Id");
        }
    }
}
