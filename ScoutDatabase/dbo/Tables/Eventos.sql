CREATE TABLE [dbo].[Eventos] (
    [IdEvento]          INT          IDENTITY (1, 1) NOT NULL,
    [Titulo]            VARCHAR (50) NOT NULL,
    [Fecha]             DATETIME     NOT NULL,
    [Lugar]             VARCHAR (50) NOT NULL,
    [Descripcion]       VARCHAR (50) NOT NULL,
    [Encargado]         VARCHAR (50) NOT NULL,
    [ContactoEncargado] VARCHAR (50) NOT NULL,
    [RutaImagen] VARCHAR(MAX) NULL, 
    PRIMARY KEY CLUSTERED ([IdEvento] ASC)
);

