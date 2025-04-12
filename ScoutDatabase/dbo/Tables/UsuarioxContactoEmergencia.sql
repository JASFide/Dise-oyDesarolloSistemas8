CREATE TABLE [dbo].[UsuarioxContactoEmergencia] (
    [IdUsuarioxContactoEmergencia] INT IDENTITY (1, 1) NOT NULL,
    [IdUsuario]                    INT NULL,
    [IdContactoEmergencia]         INT NULL,
    PRIMARY KEY CLUSTERED ([IdUsuarioxContactoEmergencia] ASC),
    CONSTRAINT [FK_UsuarioxContactoEmergencia_ContactoEmergencia] FOREIGN KEY ([IdContactoEmergencia]) REFERENCES [dbo].[ContactoEmergencia] ([IdContactoEmergencia]),
    CONSTRAINT [FK_UsuarioxContactoEmergencia_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([IdUsuario])
);

