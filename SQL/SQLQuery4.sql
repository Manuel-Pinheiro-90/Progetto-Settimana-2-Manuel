SELECT p.ID AS PrenotazioneID, c.Nome, c.Cognome, c.Email, c.Telefono, 
       s.Descrizione AS ServizioDescrizione, ps.Data AS DataServizio, ps.Quantita, s.Prezzo AS PrezzoUnitario,
       (ps.Quantita * s.Prezzo) AS PrezzoTotaleServizio
FROM Prenotazioni p
JOIN Clienti c ON p.CodiceFiscaleCliente = c.ID
JOIN PrenotazioniServizi ps ON p.ID = ps.NumeroPrenotazione
JOIN Servizi s ON ps.ServizioID = s.ID
WHERE c.CodiceFiscale = 'RSSMRA85M01H501Z'  
ORDER BY p.ID, ps.Data;