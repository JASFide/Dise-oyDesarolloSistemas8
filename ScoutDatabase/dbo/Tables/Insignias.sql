CREATE TABLE [dbo].[Insignias] (
    [IdInsignia] INT        IDENTITY (1, 1) NOT NULL,
    [Nombre]     NCHAR (10) NULL,
    [Seccion]    NCHAR (10) NULL,
    [Tipo]       NCHAR (10) NULL,
    [Estado]     NCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([IdInsignia] ASC)
);

