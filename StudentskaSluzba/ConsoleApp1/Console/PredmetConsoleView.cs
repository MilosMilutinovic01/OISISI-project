using System;
using System.Collections.Generic;
using ConsoleApp1.Manager;
using ConsoleApp1.Model;
using System.Text.RegularExpressions;
using ConsoleApp1.Manager.Serialization;
using System.Linq;

namespace ConsoleApp1.Console
{
    class PredmetConsoleView : ConsoleView
    {
        private PredmetManager manager;

        private OcenaManager managerOcena;
        public PredmetConsoleView(PredmetManager manager, OcenaManager managerOcena)
        {
            this.manager = manager;
            this.managerOcena = managerOcena;
        }

        public void IspisiPredmete(List<Predmet> predmeti)
        {
            System.Console.WriteLine("Predmeti: ");
            //string header = string.Format("BrojIndeksa {0,7} | Ime {1,15} | Prezime {2,25} | Datum rodjenja {3,35} | Adresa stanovanja", "", "", "","");
            //System.Console.WriteLine(header);

            List<Ocena> ocene = new List<Ocena>();
            string fileName = "ocene.txt";
            Serializer<Ocena> serializer = new Serializer<Ocena>();
            ocene = serializer.FromCSV(fileName);

            List<NepolozeniPredmeti> nepolozeniPredmeti = new List<NepolozeniPredmeti>();
            string fileName2 = "nepolozeniPredmeti.txt";
            Serializer<NepolozeniPredmeti> serializer2 = new Serializer<NepolozeniPredmeti>();
            nepolozeniPredmeti = serializer2.FromCSV(fileName2);

            List<Student> studenti = new List<Student>();
            string fileName3 = "studenti.txt";
            Serializer<Student> serializer3 = new Serializer<Student>();
            studenti = serializer3.FromCSV(fileName3);




            foreach (Predmet p in predmeti)
            {


                foreach(NepolozeniPredmeti np in nepolozeniPredmeti)
                {
                    if (np.sifraPredmeta == p.sifraPredmeta)
                    {
                        
                        p.nisuPolozili.Add(studenti.Find(s => s.BrojIndeksa ==np.indeks));

                    }
                }


                foreach (Ocena o in ocene)
                {
                    if (o.predmet == p.sifraPredmeta)
                    {

                        p.polozili.Add(studenti.Find(s => s.BrojIndeksa == o.studentKojiJePolozio));

                    }
                }


                System.Console.WriteLine(p + "\n");

                if (p.nisuPolozili.Any())
                {
                    System.Console.WriteLine("Lista studenata koji nisu polozili: ");
                    foreach (Student s in p.nisuPolozili)
                    {

                        System.Console.WriteLine(s.Ime + " " + s.Prezime);
                    }

                }
                p.nisuPolozili.Clear();

                if (p.polozili.Any())
                {
                    System.Console.WriteLine("Lista studenata koji su polozili: ");
                    foreach (Student s in p.polozili)
                    {

                        System.Console.WriteLine(s.Ime + " " + s.Prezime);
                    }

                }
                p.polozili.Clear();



            }



        }

        public Predmet UnesiPredmet()
        {
            Predmet predmet = new Predmet();
            
            predmet.sifraPredmeta = UnesiSifru(1);

            System.Console.Write("Unesi naziv predmeta: ");
            string nazivPredmeta = System.Console.ReadLine();
            predmet.nazivPredmeta = nazivPredmeta;

            predmet.semestar = UnesiSemestar();
            
            System.Console.Write("Unesi godinu studija: ");
            string godinaStudija = System.Console.ReadLine();
            predmet.godinaStudija = Convert.ToInt32(godinaStudija);

            System.Console.Write("Unesi id predmetnog profesora: ");
            int predmetniProfesor = Convert.ToInt32(System.Console.ReadLine());
            predmet.ProfesorId = predmetniProfesor;

            System.Console.Write("Broj ESPB bodova: ");
            string brojESPB = System.Console.ReadLine();
            predmet.brojESPB = Convert.ToInt32(brojESPB);
            
            return predmet;
        }

        public string UnesiSifru(int funkcija)
        {
            System.Console.Write("Unesi sifru predmeta: ");
            string sifra = System.Console.ReadLine().ToUpper();

            string Regex = @"[A-Z]{1}[1-9]{3}";

            Regex re = new Regex(Regex, RegexOptions.IgnoreCase);

            switch(funkcija)
            {
                case 1:
                    if (manager.VratiPredmetPoId(sifra) == null)
                    {
                        if (!re.IsMatch(sifra))
                        {
                            while (!re.IsMatch(sifra))
                            {
                                System.Console.Write("Pogrešan format sifre, unesi ispravnu sifru: ");
                                sifra = System.Console.ReadLine();
                            }
                            while (manager.VratiPredmetPoId(sifra) != null)
                            {
                                System.Console.Write("Morate uneti sifru koja ne postoji!\nUnesi sifru: ");
                                sifra = System.Console.ReadLine().ToUpper();
                            }
                        }
                    }
                    else
                    {
                        while (manager.VratiPredmetPoId(sifra) != null)
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
                            while (manager.VratiPredmetPoId(sifra) != null)
                            {
                                System.Console.Write("Morate uneti sifru koja ne postoji!\nUnesi sifru: ");
                                sifra = System.Console.ReadLine().ToUpper();
                            }
                        }
                    }
                    break;
                case 2:
                    if (manager.VratiPredmetPoId(sifra) == null)
                    {
                        if (!re.IsMatch(sifra))
                        {
                            while (!re.IsMatch(sifra))
                            {
                                System.Console.Write("Pogrešan format sifre, unesi ispravnu sifru: ");
                                sifra = System.Console.ReadLine();
                            }
                            while (manager.VratiPredmetPoId(sifra) != null)
                            {
                                System.Console.Write("Morate uneti sifru koja ne postoji!\nUnesi sifru: ");
                                sifra = System.Console.ReadLine().ToUpper();
                            }
                        }
                    }
                    if (!re.IsMatch(sifra))
                    {
                        while (manager.VratiPredmetPoId(sifra) != null)
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

        public Semestar UnesiSemestar()
        {
            Semestar s = Semestar.L;
            System.Console.Write("Semestar(L,Z): ");
            string unos = System.Console.ReadLine();
            if (unos.Equals("Z") || unos.Equals("z"))
            {
                s = Semestar.Z;
            }
            else if(unos.Equals("L") || unos.Equals("l"))
            {
                s = Semestar.L;
            }
            else
            {
                System.Console.Write("Morate uneti ispravan format semestra(L,Z):");
                unos = System.Console.ReadLine();
                while (!unos.Equals("L") && !unos.Equals("Z"))
                {
                    System.Console.Write("Morate uneti ispravan format semestra(L,Z):");
                    unos = System.Console.ReadLine();
                }
                if (unos.Equals("L") || unos.Equals("l"))
                {
                    s = Semestar.L;
                }
                else if (unos.Equals("Z") || unos.Equals("z"))
                {
                    s = Semestar.Z;
                }
            }
            return s;
        }

        public void UkloniPredmet()
        {
            string sifraPredmeta = UnesiSifru(2);

            List<Ocena> ocene = managerOcena.VratiSveOcene();
            if (ocene.Any())
            {
                foreach (Ocena o in ocene.ToList())
                {
                    if (o.predmet == sifraPredmeta)
                    {
                        managerOcena.UkloniOcenu(o.id);
                    }

                }
            }



            Predmet uklonjenPredmet = manager.UkloniPredmet(sifraPredmeta);
            if (uklonjenPredmet == null)
            {
                System.Console.WriteLine("Predmet nije pronadjen!");
                return;
            }
            System.Console.WriteLine("Predmet uklonjen");




        }

        public void AzurirajPredmet()
        {
            string sifra = UnesiSifru(2);
            Predmet predmet = manager.VratiPredmetPoId(sifra);
            predmet.sifraPredmeta = sifra;
            if (predmet == null)
            {
                System.Console.WriteLine("Predmet nije pronadjen!");
                return;
            }
            else
            {
                IzborPoljaPredmet();
                string unos = System.Console.ReadLine();
                HandlePredmetIzborPolja(unos, predmet);
            }
            System.Console.WriteLine("Predmet azuriran!");
        }

        private void IzborPoljaPredmet()
        {
            System.Console.WriteLine("\nIzaberi polje: ");
            System.Console.WriteLine("1: Naziv predmeta");
            System.Console.WriteLine("2: Semestar");
            System.Console.WriteLine("3: Godina studija");
            System.Console.WriteLine("4: Predmetni profesor");
            System.Console.WriteLine("5: Broj ESPB bodova");
            System.Console.WriteLine("0: Zatvori");
        }

        private void HandlePredmetIzborPolja(string unos, Predmet predmet)
        {
            switch (unos)
            {
                case "1":
                    System.Console.Write("Nov naziv predmeta: ");
                    string naziv = System.Console.ReadLine();
                    predmet.nazivPredmeta = naziv;
                    break;
                case "2":
                    System.Console.Write("Nov semestar: ");
                    Semestar semestar = UnesiSemestar();
                    predmet.semestar = semestar;
                    break;
                case "3":
                    System.Console.Write("Nova godina studija: ");
                    string godina = System.Console.ReadLine();
                    predmet.godinaStudija = Convert.ToInt32(godina);
                    break;
                case "4":
                    System.Console.Write("Nov predmetni profesor: ");
                    int prId = Convert.ToInt32(System.Console.ReadLine());
                    predmet.ProfesorId = prId;
                    break;
                case "5":
                    System.Console.Write("Nov broj ESPB: ");
                    string broj = System.Console.ReadLine();
                    predmet.brojESPB = Convert.ToInt32(broj);
                    break;
            }
        }

        public void DodajPredmet()
        {
            Predmet predmet = UnesiPredmet();
            manager.DodajPredmet(predmet);
            System.Console.WriteLine("Predmet dodat!");
        }
    }
}
