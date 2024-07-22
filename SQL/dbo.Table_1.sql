CREATE TABLE PrenotazioniServizi (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    NumeroPrenotazione INT NOT NULL,
    ServizioID INT NOT NULL,
    Data DATETIME2 NOT NULL,
    Quantita INT NOT NULL,
    FOREIGN KEY (NumeroPrenotazione) REFERENCES Prenotazioni(ID),
    FOREIGN KEY (ServizioID) REFERENCES Servizi(ID)
);
