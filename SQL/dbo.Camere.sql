CREATE TABLE [dbo].[Camere] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Numero]      INT            NOT NULL,
    [Descrizione] NVARCHAR (MAX) NULL,
    [Tipologia]   NVARCHAR (10)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    UNIQUE NONCLUSTERED ([Numero] ASC)
);

