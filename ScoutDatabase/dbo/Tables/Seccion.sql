CREATE TABLE [dbo].[Seccion] (
    [IdSeccion]   INT        IDENTITY (1, 1) NOT NULL,
    [Nombre]      NCHAR (10) NOT NULL,
    [EdadMinima]  INT        NOT NULL,
    [EdadMaxima]  INT        NOT NULL,
    [JefeSeccion] NCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdSeccion] ASC)
);

