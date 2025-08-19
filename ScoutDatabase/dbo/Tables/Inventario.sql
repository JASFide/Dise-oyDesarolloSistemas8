CREATE TABLE [dbo].[Inventario]
(
	[IdInvetario] INT        IDENTITY (1, 1) NOT NULL, 
    [Nombre] VARCHAR(MAX) NULL, 
    [Descripcion] VARCHAR(MAX) NULL, 
    [Estado] VARCHAR(MAX) NULL, 
    [Seccion ] NCHAR(10) NULL,
)
