Database GestionBiblioteca
GO
USE GestionBiblioteca
GO
CREATE TABLE [dbo].[Autor](
	[IdAutor] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](500) NOT NULL,
	[Apellido] [varchar](500) NOT NULL,
	[FechaNacimiento] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Autor] PRIMARY KEY CLUSTERED 
(
	[IdAutor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Libro](
	[IdLibro] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](500) NOT NULL,
	[FechaPublicacion] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Libro] PRIMARY KEY CLUSTERED 
(
	[IdLibro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[AutorLibro](
	[IdAutor] [int] NULL,
	[IdLibro] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AutorLibro]  WITH CHECK ADD  CONSTRAINT [FK_AutorLibro_Autor] FOREIGN KEY([IdAutor])
REFERENCES [dbo].[Autor] ([IdAutor])
GO

ALTER TABLE [dbo].[AutorLibro] CHECK CONSTRAINT [FK_AutorLibro_Autor]
GO

ALTER TABLE [dbo].[AutorLibro]  WITH CHECK ADD  CONSTRAINT [FK_AutorLibro_Libro] FOREIGN KEY([IdLibro])
REFERENCES [dbo].[Libro] ([IdLibro])
GO

ALTER TABLE [dbo].[AutorLibro] CHECK CONSTRAINT [FK_AutorLibro_Libro]
GO
