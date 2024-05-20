Select * from Libro
GO
CREATE PROCEDURE SP_InsertarLibro
@Nombre varchar(200),
@FechaPublicacion smalldatetime,
@IdLibro int output

AS
BEGIN
	insert into Libro(Nombre, FechaPublicacion)
	values(@Nombre, @FechaPublicacion)

	select @IdLibro = SCOPE_IDENTITY()
END

GO
CREATE PROCEDURE SP_ConsultarLibro
                                    AS
                                    BEGIN
                                    select * from Libro
                                    END
GO

CREATE PROCEDURE SP_ConsultarLibroPorID
@idLibro int
AS
BEGIN
	select * from Libro
	where IdLibro = @idLibro
END

GO
CREATE PROCEDURE SP_EliminarLibro
@idLibro int,
@resultado int OUTPUT
AS
BEGIN
	delete from Libro
	where IdLibro = @idLibro
	set @resultado= @@rowcount
END
GO
CREATE PROCEDURE SP_ModificarLibro
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

GO
DROP PROCEDURE 
