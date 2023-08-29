using ConsoleApp1.Manager.Serialization;
using System.Collections.Generic;
using System;
using ConsoleApp1.Model;

namespace ConsoleApp1.Manager
{
    class PredmetManager
    {
        public List<Predmet> predmeti;
        public Serializer<Predmet> serializer;

        private readonly string fileName = "predmeti.txt";

        public PredmetManager()
        {
            serializer = new Serializer<Predmet>();
            UcitajPredmete();
        }

        public void UcitajPredmete()
        {
            predmeti = serializer.FromCSV(fileName);
        }

        public void SacuvajPredmete()
        {
            serializer.ToCSV(fileName, predmeti);
        }
        

        public Predmet DodajPredmet(Predmet predmet)
        {
            predmeti.Add(predmet);
            SacuvajPredmete();
            return predmet;
        }

        public Predmet AzurirajPredmet(Predmet predmet)
        {
            Predmet stariPredmet = VratiPredmetPoId(predmet.sifraPredmeta);
            if (stariPredmet == null) return null;
            
            stariPredmet.nazivPredmeta = predmet.nazivPredmeta;
            stariPredmet.semestar = predmet.semestar;
            stariPredmet.godinaStudija = predmet.godinaStudija;
            stariPredmet.Profesor = predmet.Profesor;
            stariPredmet.brojESPB = predmet.brojESPB;
            stariPredmet.ProfesorId = predmet.ProfesorId;
            stariPredmet.polozili = stariPredmet.polozili;
            stariPredmet.nisuPolozili = stariPredmet.nisuPolozili;
            SacuvajPredmete();
            return stariPredmet;
        }

        public Predmet UkloniPredmet(string sifraPredmeta)
        {
            Predmet predmet = VratiPredmetPoId(sifraPredmeta);
            if (predmet == null) return null;

            predmeti.Remove(predmet);
            SacuvajPredmete();
            return predmet;
        }

        public Predmet VratiPredmetPoId(string sifraPredmeta)
        {
            return predmeti.Find(p => p.sifraPredmeta == sifraPredmeta);
        }

        public List<Predmet> VratiSvePredmete()
        {
            return predmeti;
        }
    }
}
