CREATE TABLE [dbo].[Etapa] (
    [IdEtapa] INT        IDENTITY (1, 1) NOT NULL,
    [Nombre]  VARCHAR(50) NOT NULL,
    [Seccion] VARCHAR(50) NOT NULL,
    [Estado]  VARCHAR(50) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdEtapa] ASC)
);

