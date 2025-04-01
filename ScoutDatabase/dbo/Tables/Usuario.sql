CREATE TABLE [dbo].[Usuario] (
    [IdUsuario]       INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]          VARCHAR (50)  NOT NULL,
    [Apellido1]       VARCHAR (50)  NOT NULL,
    [Apellido2]       VARCHAR (50)  NOT NULL,
    [FechaNacimiento] DATE          NOT NULL,
    [TipoUsuario]     VARCHAR (50)  NOT NULL,
    [Estado]          BIT           DEFAULT ((1)) NOT NULL,
    [IdSeccion]       INT           NOT NULL,
    [Direccion]       VARCHAR (50)  NOT NULL,
    [Correo]          VARCHAR (50)  NOT NULL,
    [NumeroTelefono]  VARCHAR (50)  NOT NULL,
    [Contrasena]      VARCHAR (255) NOT NULL,
    [IdRole]          INT           NULL,
    PRIMARY KEY CLUSTERED ([IdUsuario] ASC),
    CONSTRAINT [FK_Usuario_Role] FOREIGN KEY ([IdRole]) REFERENCES [dbo].[Role] ([Id]), 
    CONSTRAINT [FK_Usuario_Seccion] FOREIGN KEY ([IdSeccion]) REFERENCES [dbo].[Seccion] ([IdSeccion])
);





