CREATE TABLE [dbo].[Etapa] (
    [IdEtapa] INT        IDENTITY (1, 1) NOT NULL,
    [Nombre]  NCHAR (10) NOT NULL,
    [Seccion] NCHAR (10) NOT NULL,
    [Estado]  NCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdEtapa] ASC)
);

