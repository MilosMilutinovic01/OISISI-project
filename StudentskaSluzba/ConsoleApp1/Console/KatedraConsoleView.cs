using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Model;
using ConsoleApp1.Manager;
using System.Text.RegularExpressions;
using ConsoleApp1.Manager.Serialization;

namespace ConsoleApp1.Console
{
    class KatedraConsoleView : ConsoleView
    {
        public KatedraManager manager;

        public KatedraConsoleView(KatedraManager manager)
        {
            this.manager = manager;
        }

        public void IspisiKatedre(List<Katedra> katedre)
        {
            System.Console.WriteLine("Katedre: ");
            //string header = string.Format("BrojIndeksa {0,7} | Ime {1,15} | Prezime {2,25} | Datum rodjenja {3,35} | Adresa stanovanja", "", "", "","");
            //System.Console.WriteLine(header);

            List<Profesor> profesori = new List<Profesor>();
            string fileName = "profesori.txt";
            Serializer<Profesor> serializer = new Serializer<Profesor>();
            profesori = serializer.FromCSV(fileName);

            string ime = "", prezime = "";
            foreach (Katedra k in katedre)
            {
                ime = "";
                prezime = "";
                foreach (Profesor p in profesori)
                {
                    if ((p.id == k.sefKatedre) && (p.idKat == k.sifraKatedre))

                    {
                        ime = p.ime;
                        prezime = p.prezime;
                    }
                }
                System.Console.WriteLine(k + ime + " " + prezime);

                System.Console.WriteLine("\n");

                foreach (Profesor profLink in profesori)
                {
                    if (Equals(profLink.idKat, k.sifraKatedre))
                    {
                        k.spisakProfesora.Add(profLink);
                        //System.Console.WriteLine("\n" + profLink.ime + " " + profLink.prezime +" " +profLink.idKat);
                    }
                }
                if (k.spisakProfesora.Any())
                {
                    System.Console.WriteLine("Lista profesora na katedri: ");
                    foreach (Profesor p in k.spisakProfesora)
                    {

                        System.Console.WriteLine(p.ime + " " + p.prezime);
                    }

                }
                k.spisakProfesora.Clear();

            }
        }

        public Katedra UnesiKatedru()
        {
            List<Profesor> profesori = new List<Profesor>();
            string fileName = "profesori.txt";
            Serializer<Profesor> serializer = new Serializer<Profesor>();
            profesori = serializer.FromCSV(fileName);

            Katedra katedra = new Katedra();

            katedra.sifraKatedre = UnesiSifru(1);

            System.Console.Write("Unesi naziv katedre: ");
            string naziv = System.Console.ReadLine();
            katedra.nazivKatedre = naziv;

            ProfesorManager m = new ProfesorManager();
            int idProfesoraKatedre = -2;
            while (idProfesoraKatedre != -1)
            {
                System.Console.Write("Unesi profesore katedre(unesi -1 za kraj unosa): ");
                idProfesoraKatedre = Convert.ToInt32(System.Console.ReadLine());
                if (profesori.Find(v => v.id == idProfesoraKatedre) != null)
                {
                    Profesor p = profesori.Find(v => v.id == idProfesoraKatedre);
                    p.idKat = katedra.sifraKatedre;
                    m.AzurirajProfesora(p);

                }
            }
            /*
                        System.Console.Write("Unesi sefa katedre(unesi -1 ako nema sefa katedre): ");
                        int sef = Convert.ToInt32(System.Console.ReadLine());
                        while ((profesori.Find(v => v.idKat == katedra.nazivKatedre) == null) && sef!=-1)
                        {
                            System.Console.Write("Unesi sefa katedre ponovo(unesi -1 ako nema sefa katedre): ");
                            sef = Convert.ToInt32(System.Console.ReadLine());
                        }
                        katedra.sefKatedre = sef;
            */


            System.Console.Write("Unesi  sefa katedre(unesi -1 ako nema sefa katedre): ");
            int sef = Convert.ToInt32(System.Console.ReadLine());
            bool provera = false;
            while (provera = false && sef != -1)
            {

                System.Console.Write("Nije pronadjen, unesi sefa katedre ponovo(unesi -1 ako nema sefa katedre): ");
                sef = Convert.ToInt32(System.Console.ReadLine());
                if (profesori.Find(v => v.id == sef) != null)
                {
                    Profesor profa = profesori.Find(v => v.id == sef);
                    if (profa.idKat == katedra.nazivKatedre)
                    {
                        provera = true;
                    }
                }
            }






            serializer.ToCSV(fileName, profesori);
            return katedra;
        }

        public string UnesiSifru(int funkcija)
        {
            System.Console.Write("Unesi sifru: ");
            string sifra = System.Console.ReadLine().ToUpper();

            string Regex = @"[A-Z]{2}[1-9]{1}";

            Regex re = new Regex(Regex, RegexOptions.IgnoreCase);

            switch (funkcija)
            {
                case 1: //ovaj slucaj je kada radimo unos
                    if (manager.VratiKatedruPoId(sifra) == null)
                    {
                        if (!re.IsMatch(sifra))
                        {
                            while (!re.IsMatch(sifra))
                            {
                                System.Console.Write("Pogrešan format sifre, unesi ispravnu sifru: ");
                                sifra = System.Console.ReadLine();
                            }
                            while (manager.VratiKatedruPoId(sifra) != null)
                            {
                                System.Console.Write("Morate uneti sifru koja ne postoji!\nUnesi sifru: ");
                                sifra = System.Console.ReadLine().ToUpper();
                            }
                        }
                    }
                    else
                    {
                        while (manager.VratiKatedruPoId(sifra) != null)
                        {
                            System.Console.Write("Morate uneti sifru koja ne postoji!\nUnesi sifru: ");
                            sifra = System.Console.ReadLine().ToUpper();
                        }
                        if (!re.IsMatch(sifra))
                        {
                            while (!re.IsMatch(sifra))
                            {
                                System.Console.Write("Pogrešan format sifre, unesi ispravnu sifru: ");
                                sifra = System.Console.ReadLine().ToUpper();
                            }
                            while (manager.VratiKatedruPoId(sifra) != null)
                            {
                                System.Console.Write("Morate uneti sifru koja ne postoji!\nUnesi sifru: ");
                                sifra = System.Console.ReadLine().ToUpper();
                            }
                        }
                    }
                    break;
                case 2: //ovaj kada radimo azuriranje
                    if (manager.VratiKatedruPoId(sifra) == null)
                    {
                        if (!re.IsMatch(sifra))
                        {
                            while (!re.IsMatch(sifra))
                            {
                                System.Console.Write("Pogrešan format sifre, unesi ispravnu sifru: ");
                                sifra = System.Console.ReadLine();
                            }
                            while (manager.VratiKatedruPoId(sifra) == null)
                            {
                                System.Console.Write("Ne postoji katedra sa datom sifrom!\nUnesi postojeću sifru: ");
                                sifra = System.Console.ReadLine().ToUpper();
                            }
                        }
                    }
                    if (!re.IsMatch(sifra))
                    {
                        while (manager.VratiKatedruPoId(sifra) != null)
                        {
                            while (!re.IsMatch(sifra))
                            {
                                System.Console.Write("Pogrešan format sifre, unesi ispravnu sifru: ");
                                sifra = System.Console.ReadLine();
                            }
                        }
                    }
                    break;





            }
            return sifra.ToUpper();
        }

        public void UkloniKatedru()
        {
            string sifra = UnesiSifru(2);
            List<Profesor> profesori = new List<Profesor>();
            string fileName = "profesori.txt";
            Serializer<Profesor> serializer = new Serializer<Profesor>();
            profesori = serializer.FromCSV(fileName);


            Katedra uklonjenaKatedra = manager.UkloniKatedru(sifra);
            if (uklonjenaKatedra == null)
            {
                System.Console.WriteLine("Katedra nije pronadjena!");
                return;
            }
            System.Console.WriteLine("Katedra uklonjena");


            foreach (Profesor profLink in profesori)
            {
                if (profLink.idKat == uklonjenaKatedra.sifraKatedre)
                {
                    profLink.idKat = "none";
                }

                serializer.ToCSV(fileName, profesori);

            }





        }

        public void AzurirajKatedru()
        {
            string sifra = UnesiSifru(2);
            Katedra katedra = manager.VratiKatedruPoId(sifra);
            katedra.sifraKatedre = sifra;
            if (katedra == null)
            {
                System.Console.WriteLine("Katedra nije pronadjena!");
                return;
            }
            else
            {
                IzborPoljaKatedra();
                string unos = System.Console.ReadLine();
                HandleKatedraIzborPolja(unos, katedra);
            }
            Katedra azuriranaKatedra = manager.AzurirajKatedru(katedra);
            System.Console.WriteLine("Katedra azurirana!");
        }

        private void IzborPoljaKatedra()
        {
            System.Console.WriteLine("\nIzaberi polje: ");
            System.Console.WriteLine("1: Naziv");
            System.Console.WriteLine("2: Sef");
            System.Console.WriteLine("3: Dodaj profesore");
            System.Console.WriteLine("4: Izbrisi profesore");
            System.Console.WriteLine("0: Zatvori");
        }

        private void HandleKatedraIzborPolja(string unos, Katedra katedra)
        {
            List<Profesor> profesori = new List<Profesor>();
            string fileName = "profesori.txt";
            Serializer<Profesor> serializer = new Serializer<Profesor>();
            profesori = serializer.FromCSV(fileName);

            switch (unos)
            {
                case "1":
                    System.Console.Write("Nov naziv: ");
                    string naziv = System.Console.ReadLine();
                    katedra.nazivKatedre = naziv;
                    break;
                case "2":

                    System.Console.Write("Unesi novog sefa katedre(unesi -1 ako nema sefa katedre): ");
                    int sef = Convert.ToInt32(System.Console.ReadLine());
                    bool provera = false;
                    while (provera = false && sef != -1)
                    {

                        System.Console.Write("Nije pronadjen, unesi sefa katedre ponovo(unesi -1 ako nema sefa katedre): ");
                        sef = Convert.ToInt32(System.Console.ReadLine());
                        if (profesori.Find(v => v.id == sef) != null)
                        {
                            Profesor profa = profesori.Find(v => v.id == sef);
                            if (profa.idKat == katedra.nazivKatedre)
                            {
                                provera = true;
                            }
                        }
                    }
                    katedra.sefKatedre = sef;
                    break;

                case "3":

                    ProfesorManager m = new ProfesorManager();
                    int idProfesoraKatedre = -2;
                    while (idProfesoraKatedre != -1)
                    {
                        System.Console.Write("Unesi nove profesore katedre(unesi -1 za kraj unosa): ");
                        idProfesoraKatedre = Convert.ToInt32(System.Console.ReadLine());
                        if (profesori.Find(v => v.id == idProfesoraKatedre) != null)
                        {
                            Profesor p = m.VratiProfesoraPoId(idProfesoraKatedre);
                            string sifk = katedra.sifraKatedre;
                            p.idKat = sifk;
                            m.AzurirajProfesora(p);

                        }
                    }

                    break;

                case "4":
                    ProfesorManager md = new ProfesorManager();
                    //System.Console.Write("Unesi profesora katedre koga zelis da izbrises(unesi -1 ako si se predomislio): ");
                    int idp = -2;
                    while (idp != -1)
                    {

                        System.Console.Write("Unesi profesora katedre koga zelis da izbrises(unesi -1 ako si se predomislio): ");
                        idp = Convert.ToInt32(System.Console.ReadLine());
                        if (profesori.Find(v => v.id == idp) != null)
                        {
                            Profesor profa = profesori.Find(v => v.id == idp);
                            if (profa.idKat == katedra.sifraKatedre)
                            {
                                profa.idKat = "";
                                md.AzurirajProfesora(profa);

                            }
                        }
                    }

                    break;



            }
        }

        public void DodajKatedru()
        {
            Katedra katedra = UnesiKatedru();
            manager.DodajKatedru(katedra);
            System.Console.WriteLine("Katedra dodata!");
        }
    }
}
