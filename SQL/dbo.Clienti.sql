CREATE TABLE [dbo].[Clienti] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [CodiceFiscale] NVARCHAR (16)  NOT NULL,
    [Cognome]       NVARCHAR (50)  NOT NULL,
    [Nome]          NVARCHAR (50)  NOT NULL,
    [Citta]         NVARCHAR (50)  NOT NULL,
    [Provincia]     NVARCHAR (2)   NOT NULL,
    [Email]         NVARCHAR (100) NULL,
    [Telefono]      CHAR (15)      NULL,
    [Cellulare]     CHAR (13)      NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    UNIQUE NONCLUSTERED ([CodiceFiscale] ASC)
);

