CREATE TABLE [dbo].[ReqInsignia] (
    [IdReqInsignia] INT          IDENTITY (1, 1) NOT NULL,
    [Descripcion]   VARCHAR (50) NULL,
    [IdInsignias]   INT          NULL,
    PRIMARY KEY CLUSTERED ([IdReqInsignia] ASC),
    CONSTRAINT [FK_ReqInsignia_Insignias] FOREIGN KEY ([IdInsignias]) REFERENCES [dbo].[Insignias] ([IdInsignia])
);

