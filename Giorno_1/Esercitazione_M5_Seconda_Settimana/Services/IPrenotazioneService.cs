﻿using Esercitazione_M5_Seconda_Settimana.Models;

namespace Esercitazione_M5_Seconda_Settimana.Services
{
    public interface IPrenotazioneService
    {
        public Prenotazione CreaPrenotazione(Prenotazione p);
        public PrenotazioniServizio CreaPrenotazioneServizio(PrenotazioniServizio p);
    }
}
