using ConsoleApp1.Manager.Serialization;
using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Manager
{
    class KatedraManager
    {
        private List<Katedra> katedre;
        private Serializer<Katedra> serializer;

        private readonly string fileName = "katedre.txt";

        public KatedraManager()
        {
            serializer = new Serializer<Katedra>();
            UcitajKatedre();
        }

        public void UcitajKatedre()
        {
            katedre = serializer.FromCSV(fileName);
        }

        public void SacuvajKatedre()
        {
            serializer.ToCSV(fileName, katedre);
        }

        /*private int GenerisiId()
        {
            if (studenti.Count == 0) return 0;
            return Convert.ToInt32(studenti[studenti.Count - 1].brojIndeksa) + 1;
        }*/

        public Katedra DodajKatedru(Katedra katedra)
        {
            katedre.Add(katedra);
            SacuvajKatedre();
            return katedra;
        }

        public Katedra AzurirajKatedru(Katedra katedra)
        {
            Katedra staraKatedra = VratiKatedruPoId(katedra.sifraKatedre);
            if (staraKatedra == null) return null;
            
            staraKatedra.nazivKatedre = katedra.nazivKatedre;
            staraKatedra.sefKatedre = katedra.sefKatedre;

            SacuvajKatedre();
            return staraKatedra;
        }

        public Katedra UkloniKatedru(string sifraKatedre)
        {
            Katedra katedra = VratiKatedruPoId(sifraKatedre);
            if (katedra == null) return null;

            katedre.Remove(katedra);
            SacuvajKatedre();
            return katedra;
        }

        public Katedra VratiKatedruPoId(string sifraKatedre)
        {
            return katedre.Find(k => k.sifraKatedre == sifraKatedre);
        }

        public List<Katedra> VratiSveKatedre()
        {
            UcitajKatedre();
            return katedre;
        }

    }
}
