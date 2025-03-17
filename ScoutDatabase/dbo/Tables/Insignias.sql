CREATE TABLE [dbo].[Insignias] (
    [IdInsignia] INT        IDENTITY (1, 1) NOT NULL,
    [Nombre]     VARCHAR(50) NOT NULL,
    [Seccion]    VARCHAR(50) NOT NULL,
    [Tipo]       VARCHAR(50) NOT NULL,
    [Estado]     VARCHAR(50) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdInsignia] ASC)
);

