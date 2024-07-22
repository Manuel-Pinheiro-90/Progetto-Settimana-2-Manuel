CREATE TABLE [dbo].[Prenotazioni] (
    [ID]                   INT             IDENTITY (1, 1) NOT NULL,
    [CodiceFiscaleCliente] INT             NOT NULL,
    [NumeroCamera]         INT             NOT NULL,
    [DataPrenotazione]     DATETIME2 (7)   NOT NULL,
    [Anno]                 INT             NOT NULL,
    [PeriodoDal]           DATETIME2 (7)   NOT NULL,
    [PeriodoAl]            DATETIME2 (7)   NOT NULL,
    [CaparraConfirmatoria] DECIMAL (10, 2) NULL,
    [TariffaApplicata]     DECIMAL (10, 2) NOT NULL,
    [TipoSoggiorno]        NVARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([CodiceFiscaleCliente]) REFERENCES [dbo].[Clienti] ([ID]),
    FOREIGN KEY ([NumeroCamera]) REFERENCES [dbo].[Camere] ([ID])
);

