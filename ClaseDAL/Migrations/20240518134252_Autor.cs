using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapaDAL.Migrations
{
    public partial class Autor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //crear autor
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_InsertarAutor
                                @Nombre varchar(500),
                                @Apellido varchar(500),
                                @FechaNacimiento smalldatetime,
                                @IdAutor int output

                                AS
                                BEGIN
	                                insert into Autor(Nombre, Apellido, FechaNacimiento)
	                                values(@Nombre, @Apellido,@FechaNacimiento)

	                                select @IdAutor = SCOPE_IDENTITY()
                                END
                                ");


            //consultar autores.
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_ConsultarAutor
                                    AS
                                    BEGIN
                                    select * from Autor
                                    END
                                ");

            //Consultar Autor nombre
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_ConsultarAutorPorNombre
                            @nombre varchar(500)
                            AS
                            BEGIN
	                            select * from Autor
	                            where Nombre LIKE '%' + @nombre + '%'
                            END");

            //Consultar Autor ID
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_ConsultarAutorPorID
                                @idAutor int
                                AS
                                BEGIN
	                                select * from Autor
	                                where IdAutor = @idAutor
                                END");

            //Modificar autor
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_ModificarAutor
                                @IdAutor int,
                                @Nombre varchar(200),
                                @Apellido varchar(500),
                                @FechaNacimiento smalldatetime,
                                @resultado int OUTPUT

                                AS
                                BEGIN
                                update Autor
                                set  Nombre= @Nombre, Apellido= @Apellido, FechaNacimiento= @FechaNacimiento
                                where IdAutor= @IdAutor
                                set @resultado= @@rowcount
                                END");

            //Eliminar autor
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_EliminarAutor
                                    @idAutor int,
                                    @resultado int OUTPUT
                                    AS
                                    BEGIN
	                                    delete from Autor
	                                    where IdAutor = @idAutor
	                                    set @resultado= @@rowcount
                                    END");


            //Agregar libro a un autor
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_AdicionarLibroAutor
                                @IdAutor int,
                                @IdLibro varchar(200),
                                @resultado int OUTPUT

                                AS
                                BEGIN
	                                Insert into AutorLibro (IdAutor, IdLibro)
	                                values(@IdAutor, @IdLibro)
	                                set @resultado= @@rowcount
                                END");

            //eliminar libro de autor
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_EliminarLibroAutor
                                @IdAutor int,
                                @IdLibro varchar(200),
                                @resultado int OUTPUT

                                AS
                                BEGIN
                                delete from AutorLibro 
                                where IdAutor= @IdAutor and IdLibro= @IdLibro
                                set @resultado= @@rowcount
                                END");

            //consultar libros del autor
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_ConsultarLibrosAutor
                                @idAutor int

                                AS
                                BEGIN
	                                select a.IdAutor, b.* from AutorLibro a 
	                                inner join Libro b on a.IdLibro = b.IdLibro
	                                where a.IdAutor = @idAutor
                                END");

            //consultar si el libro ya esta asignado
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_ConsultarLibroAsignado
                                @idLibro int

                                AS
                                BEGIN
	                                select a.IdAutor, b.* from AutorLibro a 
	                                inner join Libro b on a.IdLibro = b.IdLibro
	                                where a.IdLibro= @idLibro
                                END
                                    ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE SP_InsertarAutor");
            migrationBuilder.Sql("DROP PROCEDURE SP_ConsultarAutor");
            migrationBuilder.Sql("DROP PROCEDURE SP_ConsultarAutorPorNombre");
            migrationBuilder.Sql("DROP PROCEDURE SP_ConsultarAutorPorID");
            migrationBuilder.Sql("DROP PROCEDURE SP_EliminarAutor");
            migrationBuilder.Sql("DROP PROCEDURE SP_ModificarAutor");
            migrationBuilder.Sql("DROP PROCEDURE SP_AdicionarLibroAutor");
            migrationBuilder.Sql("DROP PROCEDURE SP_EliminarLibroAutor");
            migrationBuilder.Sql("DROP PROCEDURE SP_ConsultarLibrosAutor");
            migrationBuilder.Sql("DROP PROCEDURE SP_ConsultarLibroAsignado");
        }
    }
}
