CREATE TABLE [dbo].[Seccion] (
    [IdSeccion]   INT        IDENTITY (1, 1) NOT NULL,
    [Nombre]      VARCHAR(50) NOT NULL,
    [EdadMinima]  INT        NOT NULL,
    [EdadMaxima]  INT        NOT NULL,
    [JefeSeccion] VARCHAR(50) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdSeccion] ASC)
);

