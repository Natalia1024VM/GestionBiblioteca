select * from Autor
GO
CREATE PROCEDURE SP_InsertarAutor
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

GO
CREATE PROCEDURE SP_ConsultarAutor
                                    AS
                                    BEGIN
                                    select * from Autor
                                    END
GO

CREATE PROCEDURE SP_ConsultarAutorPorNombre
@nombre varchar(500)
AS
BEGIN
	select * from Autor
	where Nombre LIKE '%' + @nombre + '%'
END

GO
CREATE PROCEDURE SP_EliminarAutor
@idAutor int,
@resultado int OUTPUT
AS
BEGIN
	delete from Autor
	where IdAutor = @idAutor
	set @resultado= @@rowcount
END
GO
CREATE PROCEDURE SP_ModificarAutor
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
END

GO
CREATE PROCEDURE SP_AdicionarLibroAutor
@IdAutor int,
@IdLibro varchar(200),
@resultado int OUTPUT

AS
BEGIN
	Insert into AutorLibro (IdAutor, IdLibro)
	values(@IdAutor, @IdLibro)
	set @resultado= @@rowcount
END


GO
CREATE PROCEDURE SP_EliminarLibroAutor
@IdAutor int,
@IdLibro varchar(200),
@resultado int OUTPUT

AS
BEGIN
	delete from AutorLibro 
	where IdAutor= @IdAutor and IdLibro= @IdLibro
	set @resultado= @@rowcount
END
GO
CREATE PROCEDURE SP_ConsultarLibrosAutor
@idAutor int

AS
BEGIN
	select a.IdAutor, b.* from AutorLibro a 
	inner join Libro b on a.IdLibro = b.IdLibro
	where a.IdAutor = @idAutor
END

GO
CREATE PROCEDURE SP_ConsultarLibroAsignado
@idLibro int

AS
BEGIN
	select a.IdAutor, b.* from AutorLibro a 
	inner join Libro b on a.IdLibro = b.IdLibro
	where a.IdLibro= @idLibro
END
GO