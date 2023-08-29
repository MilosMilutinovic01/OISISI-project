using System;
using System.Collections.Generic;
using ConsoleApp1.Manager;
using ConsoleApp1.Model;
using System.Text.RegularExpressions;
using ConsoleApp1.Manager.Serialization;
using System.Linq;

namespace ConsoleApp1.Console
{
    class StudentConsoleView : ConsoleView
    {
        private StudentManager manager;

        private PredmetManager managerPredmeta;

        private NepolozeniPredmetiManager managerVeze;

        private OcenaManager managerOcena;
        public StudentConsoleView(StudentManager manager, NepolozeniPredmetiManager managerVeze, PredmetManager managerPredmeta, OcenaManager managerOcena)
        {
            this.manager = manager;
            this.managerVeze = managerVeze;
            this.managerPredmeta = managerPredmeta;
            this.managerOcena = managerOcena;

        }

        public void IspisiStudente(List<Student> studenti)
        {
            System.Console.WriteLine("Studenti: ");
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

            List<Predmet> predmeti = new List<Predmet>();
            string fileName3 = "predmeti.txt";
            Serializer<Predmet> serializer3 = new Serializer<Predmet>();
            predmeti = serializer3.FromCSV(fileName3);



            foreach (Student s in studenti)
            {

                foreach (NepolozeniPredmeti np in nepolozeniPredmeti)
                {
                    if (np.indeks == s.BrojIndeksa)
                    {

                        s.spisakNepolozenih.Add(np);

                    }
                }

                double prosek = 0;
                int brojac = 0;

                foreach (Ocena o in ocene)
                {
                    if (o.studentKojiJePolozio == s.BrojIndeksa)
                    {

                        s.spisakPolozenih.Add(o);
                        prosek += o.ocenaIspita;
                        brojac++;

                    }
                }
                if (brojac != 0)
                {
                    prosek = prosek / brojac;
                }

                s.ProsecnaOcena = prosek;

                System.Console.WriteLine(s + "\n");

                if (s.spisakNepolozenih.Any())
                {
                    System.Console.WriteLine("Lista predmeta koji nisu polozeni: ");
                    foreach (NepolozeniPredmeti np in s.spisakNepolozenih)
                    {
                        
                        Predmet p = predmeti.Find(pp => pp.sifraPredmeta == np.sifraPredmeta);
                        System.Console.WriteLine(p.nazivPredmeta);
                    }

                }
                s.spisakNepolozenih.Clear();

                if (s.spisakPolozenih.Any())
                {
                    System.Console.WriteLine("Lista predmeta koji su polozeni: ");
                    foreach (Ocena o in s.spisakPolozenih)
                    {
                        Predmet p = predmeti.Find(pp => pp.sifraPredmeta == o.predmet);
                        System.Console.WriteLine(p.nazivPredmeta);
                    }

                }
                s.spisakPolozenih.Clear();


            }
        }

         public void UveziNepolozenePredmete()
         {

            List<Student> studenti = manager.VratiSveStudente();
            List<Predmet> predmeti = managerPredmeta.VratiSvePredmete();
            NepolozeniPredmeti veza = new NepolozeniPredmeti();

            System.Console.WriteLine("Unesi id studenta: ");
                string ids=System.Console.ReadLine();

                while (studenti.Find(s => s.BrojIndeksa == ids) == null)
                {
                    System.Console.Write("Unesi id studenta ponovo: ");
                    ids = System.Console.ReadLine();
                }
                veza.indeks = ids;


            System.Console.WriteLine("Unesi id predmeta: ");
                string idp = System.Console.ReadLine();

            while (predmeti.Find(p => p.sifraPredmeta == idp) == null)
            {
                System.Console.Write("Unesi id predmeta ponovo: ");
                idp = System.Console.ReadLine();
            }
            veza.sifraPredmeta = idp;
            veza.indeks = ids;
            managerVeze.DodajNepolozeniPredmeti(veza);



        }

        public void IzbrisiNepolozenePredmete()
        {
            List<NepolozeniPredmeti> nepolozeniPredmeti = managerVeze.getNepolozeniPredmeti();
            //List<Predmet> predmeti = managerPredmeta.VratiSvePredmete();
            //List<Student> studenti = manager.VratiSveStudente();
             
            System.Console.WriteLine("Unesi indeks studenta: ");
            string indeks = System.Console.ReadLine();

            while (nepolozeniPredmeti.Find(np => np.indeks == indeks) == null)
            {
                System.Console.Write("Unesi indeks ponovo: ");
                indeks = System.Console.ReadLine();
            }

            

                System.Console.WriteLine("Unesi id predmeta: ");
            string idp = System.Console.ReadLine();

            while ((nepolozeniPredmeti.Find(np => np.sifraPredmeta == idp) == null) || (nepolozeniPredmeti.Find(np => np.indeks == indeks) == null))
            {
                System.Console.Write("Unesi id predmeta ponovo: ");
                idp = System.Console.ReadLine();
            }

            managerVeze.UkloniNepolozeniIspiti(indeks, idp);

        }




        public void IspisiNepolozene(List <NepolozeniPredmeti> veze)
        {
            foreach (NepolozeniPredmeti v in veze)
            {
                System.Console.WriteLine("Id studenta: " + v.indeks + " Id predmeta: " + v.sifraPredmeta);
                System.Console.WriteLine("\n");
            }


        }







        public Student UnesiStudenta()
        {
            Student student = new Student();

            student.BrojIndeksa = UnesiIndeks(1);

            System.Console.Write("Unesi ime studenta: ");
            string ime = System.Console.ReadLine();
            student.Ime = ime;

            System.Console.Write("Unesi prezime studenta: ");
            string prezime = System.Console.ReadLine();
            student.Prezime = prezime;
            
            student.datumRodjenja = UnesiDatum();

            System.Console.Write("Unesi adresu stanovanja studenta: ");
            student.adresaStanovanja = AdresaConsoleView.UnesiAdresu();

            List<Adresa> adrese = new List<Adresa>();
            string fileName = "adrese.txt";
            Serializer<Adresa> serializer = new Serializer<Adresa>();
            AdresaManager manager = new AdresaManager();
            adrese = serializer.FromCSV(fileName);
            manager.DodajAdresu(student.adresaStanovanja);
            //DOVRSI!!!
            
            System.Console.Write("Unesi kontakt telefon studenta: ");
            string kontaktTelefon = System.Console.ReadLine();
            student.kontaktTelefon = kontaktTelefon;

            System.Console.Write("Unesi e-mail adresu studenta: ");
            string mail = System.Console.ReadLine();
            student.mail = mail;

            System.Console.Write("Unesi godinu upisa studenta: ");
            int godinaUpisa = Convert.ToInt32(System.Console.ReadLine());
            student.GodinaUpisa = godinaUpisa;

            System.Console.Write("Unesi trenutnu godinu studija studenta: ");
            int trenutnaGodinaStudija = Convert.ToInt32(System.Console.ReadLine());
            student.trenutnaGodinaStudija = trenutnaGodinaStudija;

            student.Status = UnesiStatus();

            List<NepolozeniPredmeti> np = new List<NepolozeniPredmeti>();
            string fileName1 = "nepolozeniPredmeti.txt";
            Serializer<NepolozeniPredmeti> serializer1 = new Serializer<NepolozeniPredmeti>();
            NepolozeniPredmetiManager manager1 = new NepolozeniPredmetiManager();
            np = serializer1.FromCSV(fileName1);
            foreach(NepolozeniPredmeti n in np)
            {
                student.spisakNepolozenih.Add(n);
            }

            return student;
        }

        public DateTime UnesiDatum()
        {
            System.Console.Write("Unesi datum rodjenja(mesec/dan/godina): ");
            string datum = System.Console.ReadLine();
            DateTime dt;

            var validan = DateTime.TryParse(datum, out dt);
            if (validan)
                return dt;
            else
            {
                while(!validan)
                {
                    System.Console.Write("Morate uneti ispravan format datuma! \nUnesi datum rodjenja(dan/mesec/godina): ");
                    datum = System.Console.ReadLine();
                    validan = DateTime.TryParse(datum, out dt);
                    if (validan)
                        break;
                }
                return dt;
            }
        }

        public string UnesiIndeks(int funkcija)
        {
            System.Console.Write("Unesi broj indeksa: ");
            string indeks = System.Console.ReadLine().ToUpper();

            string Regex = @"[A-Z]{2} [0-9]{1,3}/[0-9]{4}";

            Regex re = new Regex(Regex, RegexOptions.IgnoreCase);
            
            switch (funkcija)
            {
                case 1: //ovaj slucaj je kada radimo unos
                    if (manager.VratiStudentaPoId(indeks) == null)
                    {
                        if (!re.IsMatch(indeks))
                        {
                            while (!re.IsMatch(indeks))
                            {
                                System.Console.Write("Pogrešan format indeksa, unesi ispravan indeks: ");
                                indeks = System.Console.ReadLine();
                            }
                            while (manager.VratiStudentaPoId(indeks) != null)
                            {
                                System.Console.Write("Morate uneti indeks koji postoji!\nUnesi indeks: ");
                                indeks = System.Console.ReadLine().ToUpper();
                            }
                        }
                    }
                    else
                    {
                        while (manager.VratiStudentaPoId(indeks) != null)
                        {
                            System.Console.Write("Morate uneti indeks koji ne postoji!\nUnesi indeks: ");
                            indeks = System.Console.ReadLine().ToUpper();
                        }
                        if (!re.IsMatch(indeks))
                        {
                            while (!re.IsMatch(indeks))
                            {
                                System.Console.Write("Pogrešan format indeksa, unesi ispravan indeks: ");
                                indeks = System.Console.ReadLine().ToUpper();
                            }
                            while (manager.VratiStudentaPoId(indeks) != null)
                            {
                                System.Console.Write("Morate uneti indeks koji ne postoji!\nUnesi indeks: ");
                                indeks = System.Console.ReadLine().ToUpper();
                            }
                        }
                    }
                    break;
                case 2: //ovaj kada radimo azuriranje
                    if(manager.VratiStudentaPoId(indeks) == null)
                    {
                        if (!re.IsMatch(indeks))
                        {
                            while (!re.IsMatch(indeks))
                            {
                                System.Console.Write("Pogrešan format indeksa, unesi ispravan indeks: ");
                                indeks = System.Console.ReadLine();
                            }
                            while (manager.VratiStudentaPoId(indeks) == null)
                            {
                                System.Console.Write("Ne postoji student sa datim indeksom!\nUnesi postojeći indeks: ");
                                indeks = System.Console.ReadLine().ToUpper();
                            }
                        }
                    }
                    if (!re.IsMatch(indeks))
                    {
                        while (manager.VratiStudentaPoId(indeks) != null)
                        {
                            while (!re.IsMatch(indeks))
                            {
                                System.Console.Write("Pogrešan format indeksa, unesi ispravan indeks: ");
                                indeks = System.Console.ReadLine();
                            }
                        }
                    }
                    break;
            }
            return indeks.ToUpper();
        }

        public Status UnesiStatus()
        {
            Status s = Status.B;
            System.Console.Write("Status(B,S): ");
            string unos = System.Console.ReadLine();
            if (unos.Equals("S") || unos.Equals("s"))
            {
                s = Status.S;
            }
            else if(unos.Equals("B") || unos.Equals("b"))
            {
                s = Status.B;
            }
            else
            {
                System.Console.Write("Morate uneti ispravan format statusa(B,S):");
                unos = System.Console.ReadLine();
                while (!unos.Equals("B") && !unos.Equals("S"))
                {
                    System.Console.Write("Morate uneti ispravan format statusa(B,S):");
                    unos = System.Console.ReadLine();
                }
                if (unos.Equals("B") || unos.Equals("b"))
                {
                    s = Status.B;
                }
                else if (unos.Equals("S") || unos.Equals("s"))
                {
                    s = Status.S;
                }
            }
            return s;
        }
        
        public void UkloniStudenta()
        {
            string indeks = UnesiIndeks(2);
            List<Ocena> ocene = managerOcena.VratiSveOcene();
            if (ocene.Any())
            {
                foreach (Ocena o in ocene.ToList())
                {
                    if (o.studentKojiJePolozio == indeks)
                    {
                        managerOcena.UkloniOcenu(o.id);
                    }

                }
            }
            Student uklonjenStudent = manager.UkloniStudenta(indeks);
            if (uklonjenStudent == null)
            {
                System.Console.WriteLine("Student nije pronadjen!");
                return;
            }
            System.Console.WriteLine("Student uklonjen");


        }

        public void AzurirajStudenta()
        {
            string indeks = UnesiIndeks(2);
            Student student = manager.VratiStudentaPoId(indeks);
            student.BrojIndeksa = indeks;
            if (student == null)
            {
                System.Console.WriteLine("Student nije pronadjen!");
                return;
            }
            else
            {
                IzborPoljaStudent();
                string unos = System.Console.ReadLine();
                HandleStudentIzborPolja(unos, student);
            }
            Student azuriranStudent = manager.AzurirajStudenta(student);
            System.Console.WriteLine("Student azuriran!");
        }

        private void IzborPoljaStudent()
        {
            System.Console.WriteLine("\nIzaberi polje: ");
            System.Console.WriteLine("1: Ime");
            System.Console.WriteLine("2: Prezime");
            System.Console.WriteLine("3: Datum rodjenja");
            System.Console.WriteLine("4: Adresa stanovanja");
            System.Console.WriteLine("5: Kontakt telefon");
            System.Console.WriteLine("6: E-mail");
            System.Console.WriteLine("7: Godina upisa");
            System.Console.WriteLine("8: Trenutna godina studija");
            System.Console.WriteLine("9: Status");
            System.Console.WriteLine("0: Zatvori");
        }

        private void HandleStudentIzborPolja(string unos, Student student)
        {
            switch (unos)
            {
                case "1":
                    System.Console.Write("Novo ime: ");
                    string ime = System.Console.ReadLine();
                    student.Ime = ime;
                    break;
                case "2":
                    System.Console.Write("Novo prezime: ");
                    string prezime = System.Console.ReadLine();
                    student.Prezime = prezime;
                    break;
                case "3":
                    System.Console.Write("Nov datum rodjenja: ");
                    student.datumRodjenja = UnesiDatum();
                    break;
                case "4":
                    System.Console.Write("Nova adresa stanovanja: ");
                    student.adresaStanovanja = AdresaConsoleView.UnesiAdresu();
                    break;
                case "5":
                    System.Console.Write("Nov kontakt telefon: ");
                    string kontaktTelefon = System.Console.ReadLine();
                    student.kontaktTelefon = kontaktTelefon;
                    break;
                case "6":
                    System.Console.Write("Nov e-mail: ");
                    string mail = System.Console.ReadLine();
                    student.mail = mail;
                    break;
                case "7":
                    System.Console.Write("Nova godina upisa: ");
                    string godinaUpisa = System.Console.ReadLine();
                    student.GodinaUpisa = Convert.ToInt32(godinaUpisa);
                    break;
                case "8":
                    System.Console.Write("Nova trenutna godina studija: ");
                    string trenutnaGodinaStudija = System.Console.ReadLine();
                    student.trenutnaGodinaStudija = Convert.ToInt32(trenutnaGodinaStudija); ;
                    break;
                case "9":
                    System.Console.Write("Nov status: ");
                    Status status = UnesiStatus();
                    student.Status = status;
                    break;
            }
        }

        public void DodajStudenta()
        {
            Student student = UnesiStudenta();
            manager.DodajStudenta(student);
            System.Console.WriteLine("Student dodat!");
        } 
    }
}
