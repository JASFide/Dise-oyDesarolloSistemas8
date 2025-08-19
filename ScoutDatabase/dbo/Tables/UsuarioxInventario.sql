CREATE TABLE [dbo].[UsuarioxInventario]
(
	[IdUsuarioxInventario] INT     IDENTITY (1, 1)     NOT NULL,
	[Fecha de Salida]           DATE          NOT NULL,
	[Fecha de Regreso]           DATE          NOT NULL,
	[IdUsuario]           INT          NOT NULL,
    [IdInventario]          INT          NOT NULL,
	PRIMARY KEY CLUSTERED ([IdUsuarioxInventario] ASC),
    CONSTRAINT [FK_UsuarioxInventario_Insignias] FOREIGN KEY ([IdInventario]) REFERENCES [dbo].[Insignias] ([IdInsignia]),
    CONSTRAINT [FK_UsuarioxInventario_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([IdUsuario])
)
