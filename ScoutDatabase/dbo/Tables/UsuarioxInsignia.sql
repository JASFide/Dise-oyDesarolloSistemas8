CREATE TABLE [dbo].[UsuarioxInsignia] (
    [IdUsuarioxInsignias] INT     IDENTITY (1, 1)     NOT NULL,
    [IdUsuario]           INT          NOT NULL,
    [IdInsignia]          INT          NOT NULL,
    [Estado]              VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdUsuarioxInsignias] ASC),
    CONSTRAINT [FK_UsuarioxInsignia_Insignias] FOREIGN KEY ([IdInsignia]) REFERENCES [dbo].[Insignias] ([IdInsignia]),
    CONSTRAINT [FK_UsuarioxInsignia_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([IdUsuario])
);

