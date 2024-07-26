CREATE TABLE [dbo].[PrenotazioniServizi] (
    [ID]                 INT           IDENTITY (1, 1) NOT NULL,
    [NumeroPrenotazione] INT           NOT NULL,
    [ServizioID]         INT           NOT NULL,
    [Data]               DATETIME2 (7) NOT NULL,
    [Quantita]           INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([NumeroPrenotazione]) REFERENCES [dbo].[Prenotazioni] ([ID]),
    FOREIGN KEY ([ServizioID]) REFERENCES [dbo].[Servizi] ([ID])
);

