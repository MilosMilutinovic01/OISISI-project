using ConsoleApp1.Manager.Serialization;
using System.Collections.Generic;
using System;
using ConsoleApp1.Model;

namespace ConsoleApp1.Manager
{
    class OcenaManager
    {
        private List<Ocena> ocene;
        private Serializer<Ocena> serializer;

        private readonly string fileName = "ocene.txt";

        public OcenaManager()
        {
            serializer = new Serializer<Ocena>();
            UcitajOcene();
        }

        private void UcitajOcene()
        {
            ocene = serializer.FromCSV(fileName);
        }

        private void SacuvajOcene()
        {
            serializer.ToCSV(fileName, ocene);
        }

        private int GenerisiId()
        {
            if (ocene.Count == 0) return 0;
            return Convert.ToInt32(ocene[ocene.Count - 1].id) + 1;
        }

        public Ocena DodajOcenu(Ocena ocena)
        {
            ocena.id = GenerisiId();
            ocene.Add(ocena);
            SacuvajOcene();
            return ocena;
        }

        public Ocena AzurirajOcenu(Ocena ocena)
        {
            Ocena staraOcena = VratiOcenuPoId(ocena.id);
            if (staraOcena == null) return null;

            staraOcena.ocenaIspita = ocena.ocenaIspita;
            staraOcena.datumPolaganjaIspita = ocena.datumPolaganjaIspita;

            SacuvajOcene();
            return staraOcena;
        }

        public Ocena UkloniOcenu(int id)
        {
            Ocena ocena = VratiOcenuPoId(id);
            if (ocena == null) return null;

            ocene.Remove(ocena);
            SacuvajOcene();
            return ocena;
        }

        public Ocena VratiOcenuPoId(int id)
        {
            return ocene.Find(o => o.id == id);
        }

        public List<Ocena> VratiSveOcene()
        {
            return ocene;
        }
    }
}
