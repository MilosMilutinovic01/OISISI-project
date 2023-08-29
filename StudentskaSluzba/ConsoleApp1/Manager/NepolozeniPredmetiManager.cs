using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Model;
using ConsoleApp1.Manager.Serialization;

namespace ConsoleApp1.Manager
{
    class NepolozeniPredmetiManager
    {
        public List<NepolozeniPredmeti> nepolozeniPredmeti;
        public Serializer<NepolozeniPredmeti> serializer;

        private readonly string fileName = "nepolozeniPredmeti.txt";

        public NepolozeniPredmetiManager()
        {
            serializer = new Serializer<NepolozeniPredmeti>();
            UcitajNepolozeniPredmeti();
        }

        public void UcitajNepolozeniPredmeti()
        {
            nepolozeniPredmeti = serializer.FromCSV(fileName);
        }

        public void SacuvajNepolozeniPredmeti()
        {
            serializer.ToCSV(fileName, nepolozeniPredmeti);
        }

        public NepolozeniPredmeti DodajNepolozeniPredmeti(NepolozeniPredmeti np)
        {
            NepolozeniPredmeti nepPred = new NepolozeniPredmeti();
            nepPred.indeks = np.indeks;
            nepPred.sifraPredmeta = np.sifraPredmeta;
            nepolozeniPredmeti.Add(nepPred);
            SacuvajNepolozeniPredmeti();

            return nepPred;
        }


        public NepolozeniPredmeti UkloniNepolozeniIspiti(string indeks, string sifraPredmeta)
        {

            NepolozeniPredmeti np = null;

            foreach(NepolozeniPredmeti nepPre in nepolozeniPredmeti)
            {
                if((nepPre.indeks== indeks) && (nepPre.sifraPredmeta == sifraPredmeta))
                {
                    np = nepPre;
                }
            }

            if (np == null) return null;

            nepolozeniPredmeti.Remove(np);
            SacuvajNepolozeniPredmeti();
            return np;

        }

        /*public List<int> updateNepolozeniIspiti(int id)
        {
            NepolozeniPredmeti stari_np = getNpById(id);
            List<int> n_predmet = new List<int>();
            int unos = -1;

            while (true)
            {
                bool dobar_unos = false;
                System.Console.WriteLine("Unesite id predmeta koji ucenik slusa(za kraj unesite -1): ");
                while (!dobar_unos)
                {
                    unos = int.Parse(Console.ReadLine());
                    if (n_predmet.Contains(unos))
                        System.Console.WriteLine("Predmet je vec unet");
                    else
                        dobar_unos = true;
                }
                if (unos == -1)
                    break;
                n_predmet.Add(unos);
            }

            stari_ni.id_predmeta = n_predmet;
            SacuvajNepolozeniPredmeti();

            return n_predmet;
        }*/


        /*public NepolozeniPredmeti UkloniPolozeniPredmet(Predmet predmet)
        {
            NepolozeniPredmeti predmeti = getNpById(predmet.sifraPredmeta);
            if (predmeti == null) return null;

            nepolozeniPredmeti.Remove(predmeti);
            SacuvajNepolozeniPredmeti();
            return predmeti;
        }*/

        public NepolozeniPredmeti getNpById(string sifra)
        {
            return nepolozeniPredmeti.Find(p => p.sifraPredmeta == sifra);
        }

        public List<NepolozeniPredmeti> getNepolozeniPredmeti()
        {
            return nepolozeniPredmeti;
        }
    }
}
