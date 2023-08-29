using ConsoleApp1.Console;
using ConsoleApp1.Manager;
using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Manager.Serialization;

namespace ConsoleApp1.Console
{

    class OcenaConsoleView : ConsoleView
    {
        private OcenaManager manager;

        private NepolozeniPredmetiManager managerNP;
        public OcenaConsoleView(OcenaManager manager, NepolozeniPredmetiManager managerNP)
        {
            this.manager = manager;
            this.managerNP = managerNP;
        }

        public void IspisiOcene(List<Ocena> ocene)
        {
            System.Console.WriteLine("Ocene: ");
            //string header = string.Format("ID {0,11} | OI  {1,21} | DPI {2,30} |", "", "", "");
            //System.Console.WriteLine(header);
            foreach (Ocena o in ocene)
            {

                System.Console.WriteLine(o + "\n");
            }
        }

        public Ocena UnesiOcenu()   //povezivanje ocene sa studentom i predmetom
        {
            Ocena ocena = new Ocena();

            System.Console.Write("Unesi vrednost ocene: ");
            int  vrednost= Convert.ToInt32(System.Console.ReadLine());
            while(vrednost < 6 || vrednost > 10)
            {
                System.Console.Write("Unesi vrednost ocene ponovo: ");
                vrednost = Convert.ToInt32(System.Console.ReadLine());

            }
            ocena.ocenaIspita = vrednost;

            
            List<NepolozeniPredmeti> veze = new List<NepolozeniPredmeti>();
            string fileName = "nepolozeniPredmeti.txt";
            Serializer<NepolozeniPredmeti> serializer = new Serializer<NepolozeniPredmeti>();
            veze = serializer.FromCSV(fileName);
            System.Console.Write("Unesi broj indeksa studenta koji je polozio: ");
            string indeks = System.Console.ReadLine().ToUpper();
            while (veze.Find(o => o.indeks == indeks) == null)
            {
                System.Console.Write("Unesi broj indeksa studenta koji je polozio ponovo: ");
                indeks = System.Console.ReadLine().ToUpper(); 
            }
            
            List<NepolozeniPredmeti> provera = veze.FindAll(v => v.indeks == indeks);


            System.Console.Write("Unesi sifru predmeta koji je polozio: ");
            string sifraPredmeta = System.Console.ReadLine().ToUpper();
            while ((provera.Find(o => o.sifraPredmeta == sifraPredmeta) == null))
            {
                System.Console.Write("Unesi sifru predmeta koja odgovara datom studentu:");
                sifraPredmeta = System.Console.ReadLine().ToUpper();
                
            }
            ocena.studentKojiJePolozio = indeks;
            ocena.predmet = sifraPredmeta;
            managerNP.UkloniNepolozeniIspiti(indeks, sifraPredmeta);


            System.Console.Write("Unesi datum polaganja ispita: ");
            string dlp = System.Console.ReadLine();
            ocena.datumPolaganjaIspita= dlp;

            return ocena;
        }

        private int UnesiID()
        {
            List<Ocena> ocene = manager.VratiSveOcene();
            System.Console.Write("Unesi ID ocene: ");
            int ido = Convert.ToInt32(System.Console.ReadLine());

            while (ocene.Find(o => o.id == ido) == null)
            {
                System.Console.Write("Unesi id ocene ponovo: ");
                ido = Convert.ToInt32(System.Console.ReadLine());
            }

            return ido;
        }
        /*
        public void RunMenu()
        {
            while (true)
            {
                ShowMenu();
                string userInput = System.Console.ReadLine();
                if (userInput == "0") break;
                HandleMenuInput(userInput);
            }
        }


        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    IspisiOcene(manager.VratiSveOcene());
                    break;
                case "2":
                    DodajOcenu();
                    break;
                case "3":
                    AzurirajOcenu();
                    break;
                case "4":
                    UkloniOcenu();
                    break;

            }
        }
        */
        public void UkloniOcenu()
        {
            int id = UnesiID();
            Ocena uklonjenaOcena = manager.UkloniOcenu(id);
            if (uklonjenaOcena == null)
            {
                System.Console.WriteLine("Ocena nije pronadjena!");
                return;
            }

            NepolozeniPredmeti veza = new NepolozeniPredmeti();
            veza.indeks = uklonjenaOcena.studentKojiJePolozio;
            veza.sifraPredmeta = uklonjenaOcena.predmet;
            managerNP.DodajNepolozeniPredmeti(veza);



            System.Console.WriteLine("Ocena uklonjena");
        }

        public void AzurirajOcenu()
        {
            int id = UnesiID();
            Ocena ocena = UnesiOcenu();
            ocena.id = id;
            Ocena azuriranaOcena = manager.AzurirajOcenu(ocena);
            if (azuriranaOcena == null)
            {
                System.Console.WriteLine("Ocena nije pronadjena!");
                return;
            }
            System.Console.WriteLine("Ocena azurirana!");
        }

        public void DodajOcenu()
        {
            Ocena ocena = UnesiOcenu();
            manager.DodajOcenu(ocena);
            System.Console.WriteLine("Ocena dodata!");
        }
        /*
        public void ShowMenu()
        {
            System.Console.WriteLine("\nIzaberi opciju: ");
            System.Console.WriteLine("1: Prikaži sve ocene");
            System.Console.WriteLine("2: Dodaj ocenu");
            System.Console.WriteLine("3: Ažuriraj ocenu");
            System.Console.WriteLine("4: Ukloni ocenu");
            System.Console.WriteLine("0: Zatvori");
        }
        */
    }
}
