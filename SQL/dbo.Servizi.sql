CREATE TABLE [dbo].[Servizi] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [Descrizione] NVARCHAR (100)  NOT NULL,
    [Prezzo]      DECIMAL (10, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

