CREATE TABLE [dbo].[ContactoEmergencia] (
    [IdContactoEmergencia] INT          IDENTITY (1, 1) NOT NULL,
    [Nombre]               VARCHAR (50) NULL,
    [Parentesco]           VARCHAR (50) NULL,
    [NumeroContacto]       VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([IdContactoEmergencia] ASC)
);

