CREATE TABLE [dbo].[ConfirmacionEvento] (
    [IdConfirmacionEvento] INT          NOT NULL,
    [Asistencia]           VARCHAR (50) NOT NULL,
    [IdEvento]             INT          NOT NULL,
    [IdUsuario]            INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([IdConfirmacionEvento] ASC),
    CONSTRAINT [FK_ConfirmacionEvento_Eventos] FOREIGN KEY ([IdEvento]) REFERENCES [dbo].[Eventos] ([IdEvento]),
    CONSTRAINT [FK_ConfirmacionEvento_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([IdUsuario])
);

