using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapaDAL.Migrations
{
    public partial class Libro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //crear autor
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_InsertarLibro
                                    @Nombre varchar(200),
                                    @FechaPublicacion smalldatetime,
                                    @IdLibro int OUTPUT

                                    AS
                                    BEGIN
                                     SET NOCOUNT ON;
	                                    insert into Libro(Nombre, FechaPublicacion)
	                                    values(@Nombre, @FechaPublicacion)  

	                                    select @IdLibro = SCOPE_IDENTITY()
                                    END");

            //consultar libros.
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_ConsultarLibro
                                    AS
                                    BEGIN
                                    select * from Libro
                                    END");

            //consultar libro por id
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_ConsultarLibroPorID
                                @idLibro int
                                AS
                                BEGIN
                                select * from Libro
                                where IdLibro = @idLibro
                                END");

            //Eliminar libro
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_EliminarLibro
                                    @idLibro int,
                                    @resultado int OUTPUT
                                    AS
                                    BEGIN
                                    delete from Libro
                                    where IdLibro = @idLibro
                                    set @resultado= @@rowcount
                                    END");


            //Modificar Libro
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_ModificarLibro
                                    @IdLibro int,
                                    @Nombre varchar(200),
                                    @FechaPublicacion smalldatetime,
                                    @resultado int OUTPUT

                                    AS
                                    BEGIN
                                    update Libro
                                    set  Nombre= @Nombre, FechaPublicacion= @FechaPublicacion
                                    where IdLibro= @IdLibro
                                    set @resultado= @@rowcount
                                    END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE SP_InsertarLibro");
            migrationBuilder.Sql("DROP PROCEDURE SP_ConsultarLibro");
            migrationBuilder.Sql("DROP PROCEDURE SP_ConsultarLibroPorID");
            migrationBuilder.Sql("DROP PROCEDURE SP_EliminarLibro");
            migrationBuilder.Sql("DROP PROCEDURE SP_ModificarLibro");
        }
    }
}
