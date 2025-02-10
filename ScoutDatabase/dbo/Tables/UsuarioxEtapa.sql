CREATE TABLE [dbo].[UsuarioxEtapa] (
    [IdUsuarioEtapa] INT NOT NULL,
    [IdUsuario]      INT NOT NULL,
    [IdEtapa]        INT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdUsuarioEtapa] ASC),
    CONSTRAINT [FK_UsuarioxEtapa_Etapa] FOREIGN KEY ([IdEtapa]) REFERENCES [dbo].[Etapa] ([IdEtapa]),
    CONSTRAINT [FK_UsuarioxEtapa_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([IdUsuario])
);

