using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Manager;
using ConsoleApp1.Model;

namespace ConsoleApp1.Console
{
    class MainConsoleView
    {
        static StudentManager managerStudent = new StudentManager();
        static NepolozeniPredmetiManager managerVeze = new NepolozeniPredmetiManager();
        static PredmetManager managerPredmet = new PredmetManager();
        static OcenaManager managerOcena = new OcenaManager();
        static ProfesorManager managerProfesor = new ProfesorManager();
        static KatedraManager managerKatedra = new KatedraManager();
        static AdresaManager managerAdresa = new AdresaManager();

        StudentConsoleView student = new StudentConsoleView(managerStudent, managerVeze, managerPredmet, managerOcena);

        ProfesorConsoleView profesor = new ProfesorConsoleView(managerProfesor);

        PredmetConsoleView predmet = new PredmetConsoleView(managerPredmet, managerOcena);

        KatedraConsoleView katedra = new KatedraConsoleView(managerKatedra);

        AdresaConsoleView adresa = new AdresaConsoleView(managerAdresa);

        OcenaConsoleView ocena = new OcenaConsoleView(managerOcena, managerVeze);

        public void PovezivacProfesorPredmet()
        {
            //povezivanje i brisanje veze ako je profesor obrisan u konzoli
            foreach (Predmet predmetLink in managerPredmet.predmeti)
            {
                if (managerProfesor.profesori.Find(p => p.id == predmetLink.ProfesorId) != null)
                {
                    Profesor profesorLink = managerProfesor.profesori.Find(p => p.id == predmetLink.ProfesorId);

                    if (!(profesorLink.predmeti.Contains(predmetLink)))
                    {
                        profesorLink.predmeti.Add(predmetLink);
                        predmetLink.Profesor = profesorLink;
                        managerPredmet.AzurirajPredmet(predmetLink);
                        managerProfesor.AzurirajProfesora(profesorLink);
                    }


                }
                else
                {
                    predmetLink.Profesor = null;
                    managerPredmet.AzurirajPredmet(predmetLink);

                }

            }

            /*        foreach (Profesor profesorDelink in managerProfesor.profesori)
                    {
                        foreach (Predmet predmetDelink in profesorDelink.predmeti)
                        {
                            if (managerProfesor.profesori.Find(p => p.id == predmetDelink.ProfesorId) == null)
                            {
                                profesorDelink.predmeti.Remove(predmetDelink);
                            }
                        }

                    }  */
            //povezivanje i brisanje veze ako je predmet obrisan u konzoli
            foreach (Profesor profesorDelink in managerProfesor.profesori)
            {

                if (profesorDelink.predmeti.Any())
                    foreach (Predmet predmetDelink in profesorDelink.predmeti.ToList())
                    {
                        if (!managerPredmet.predmeti.Contains(predmetDelink))
                        {
                            profesorDelink.predmeti.Remove(predmetDelink);

                        }

                        if (profesorDelink.id != predmetDelink.ProfesorId)
                        {
                            profesorDelink.predmeti.Remove(predmetDelink);
                        }

                    }

                    


            }
        }


        public void RunMenu()
        {
            PovezivacProfesorPredmet();
            while (true)
            {
                GlavniMeni();
                string unos = System.Console.ReadLine();
                if (unos == "0") break;
                HandleMenuInput(unos);
            }
        }

        private void HandleMenuInput(string unos)
        {
            switch (unos)
            {
                case "1":
                    StudentMeni();
                    string studentUnos = System.Console.ReadLine();
                    HandleStudentMenuInput(studentUnos);
                    while (studentUnos != "0")
                    {
                        StudentMeni();
                        studentUnos = System.Console.ReadLine();
                        HandleStudentMenuInput(studentUnos);
                    }
                    break;
                case "2":
                    ProfesorMeni();
                    //PovezivacProfesorPredmet();
                    string profesorUnos = System.Console.ReadLine();
                    HandleProfesorMenuInput(profesorUnos);
                    while (profesorUnos == "0")
                    {
                        
                        ProfesorMeni();
                        //PovezivacProfesorPredmet();
                        profesorUnos = System.Console.ReadLine();
                        HandleProfesorMenuInput(profesorUnos);
                    }
                    break;
                case "3":
                    PredmetMeni();
                    //PovezivacProfesorPredmet();
                    string predmetUnos = System.Console.ReadLine();
                    HandlePredmetMenuInput(predmetUnos);
                    while (predmetUnos != "0")
                    {
                        PredmetMeni();
                        //PovezivacProfesorPredmet();
                        predmetUnos = System.Console.ReadLine();
                        HandlePredmetMenuInput(predmetUnos);
                    }
                    break;
                case "4":
                    OcenaMeni();
                    string ocenaUnos = System.Console.ReadLine();
                    HandleOcenaMenuInput(ocenaUnos);
                    while (ocenaUnos != "0")
                    {
                        OcenaMeni();
                        ocenaUnos = System.Console.ReadLine();
                        HandleOcenaMenuInput(ocenaUnos);
                    }
                    break;
                case "5":
                    KatedraMeni();
                    string katedraUnos = System.Console.ReadLine();
                    HandleKatedraMenuInput(katedraUnos);
                    while (katedraUnos != "0")
                    {
                        KatedraMeni();
                        katedraUnos = System.Console.ReadLine();
                        HandleKatedraMenuInput(katedraUnos);
                    }
                    break;
                /*case "6":
                    AdresaMeni();
                    string adresaUnos = System.Console.ReadLine();
                    HandleAdresaMenuInput(adresaUnos);
                    while (adresaUnos != "0")
                    {
                        AdresaMeni();
                        adresaUnos = System.Console.ReadLine();
                        HandleAdresaMenuInput(adresaUnos);
                    }
                    break;
                    */
                default:
                    System.Console.Write("Pogrešan unos!");
                    break;
            }
        }

        public void GlavniMeni()
        {
            System.Console.WriteLine("Izaberi entitet sa kojim radis: ");
            System.Console.WriteLine("1: Student");
            System.Console.WriteLine("2: Profesor");
            System.Console.WriteLine("3: Predmet");
            System.Console.WriteLine("4: Ocena na ispitu");
            System.Console.WriteLine("5: Katedra");
            //System.Console.WriteLine("6: Adresa");
            System.Console.WriteLine("0: Zatvori");
        }

        public void PokreniMeni()
        {
            PovezivacProfesorPredmet();
            while (true)
            {
                GlavniMeni();
                string unos = System.Console.ReadLine();
                if (unos == "0") break;
                HandleMenuInput(unos);
            }
        }

        private void StudentMeni()
        {
            System.Console.WriteLine("\nIzaberi opciju: ");
            System.Console.WriteLine("1: Prikaži sve studente");
            System.Console.WriteLine("2: Dodaj studenta");
            System.Console.WriteLine("3: Ažuriraj studenta");
            System.Console.WriteLine("4: Ukloni studenta");
            System.Console.WriteLine("5: Poveži studenta i predmet");
            System.Console.WriteLine("6: Ispiši povezane studente i predmete");
            System.Console.WriteLine("7: Ukloni vezu predmeta i studenta");
            System.Console.WriteLine("0: Zatvori");
        }

        private void ProfesorMeni()
        {
            System.Console.WriteLine("\nIzaberi opciju: ");
            System.Console.WriteLine("1: Prikaži sve profesore");
            System.Console.WriteLine("2: Dodaj profesora");
            System.Console.WriteLine("3: Ažuriraj profesora");
            System.Console.WriteLine("4: Ukloni profesora");
            System.Console.WriteLine("0: Zatvori");
        }

        private void PredmetMeni()
        {
            System.Console.WriteLine("\nIzaberi opciju: ");
            System.Console.WriteLine("1: Prikaži sve predmete");
            System.Console.WriteLine("2: Dodaj predmet");
            System.Console.WriteLine("3: Ažuriraj predmet");
            System.Console.WriteLine("4: Ukloni predmet");
            System.Console.WriteLine("0: Zatvori");
        }

        private void OcenaMeni()
        {
            System.Console.WriteLine("\nIzaberi opciju: ");
            System.Console.WriteLine("1: Prikaži sve ocene");
            System.Console.WriteLine("2: Dodaj ocenu");
            System.Console.WriteLine("3: Ažuriraj ocenu");
            System.Console.WriteLine("4: Ukloni ocenu");
            System.Console.WriteLine("0: Zatvori");
        }

        private void KatedraMeni()
        {
            System.Console.WriteLine("\nIzaberi opciju: ");
            System.Console.WriteLine("1: Prikaži sve katedre");
            System.Console.WriteLine("2: Dodaj katedru");
            System.Console.WriteLine("3: Ažuriraj katedru");
            System.Console.WriteLine("4: Ukloni katedru");
            System.Console.WriteLine("0: Zatvori");
        }

        private void AdresaMeni()
        {
            System.Console.WriteLine("\nIzaberi opciju: ");
            System.Console.WriteLine("1: Prikaži sve adrese");
            System.Console.WriteLine("2: Dodaj adresu");
            System.Console.WriteLine("3: Ažuriraj profesora");
            System.Console.WriteLine("4: Ukloni profesora");
            System.Console.WriteLine("0: Zatvori");
        }

        private void HandleStudentMenuInput(string unos)
        {
            switch (unos)
            {
                case "0":
                    break;
                case "1":
                    student.IspisiStudente(managerStudent.VratiSveStudente());
                    break;
                case "2":
                    student.DodajStudenta();
                    break;
                case "3":
                    student.AzurirajStudenta();
                    break;
                case "4":
                    student.UkloniStudenta();
                    break;
                case "5":
                    student.UveziNepolozenePredmete();
                    break;

                case "6":
                    student.IspisiNepolozene(managerVeze.getNepolozeniPredmeti());
                    break;

                case "7":
                    student.IzbrisiNepolozenePredmete();
                    break;

                default:
                    System.Console.Write("Pogrešan unos!");
                    break;
            }
        }

        private void HandlePredmetMenuInput(string unos)
        {
          //  PovezivacProfesorPredmet();
            switch (unos)
            {
                case "0":
                    break;
                case "1":
                    PovezivacProfesorPredmet();
                    predmet.IspisiPredmete(managerPredmet.VratiSvePredmete());
                    break;
                case "2":
                    predmet.DodajPredmet();
                    PovezivacProfesorPredmet();
                    break;
                case "3":
                    predmet.AzurirajPredmet();
                    PovezivacProfesorPredmet();
                    break;
                case "4":
                    predmet.UkloniPredmet();
                    PovezivacProfesorPredmet();
                    break;
                default:
                    System.Console.Write("Pogrešan unos!");
                    break;
            }
        }
        
        private void HandleKatedraMenuInput(string unos)
        {
            switch (unos)
            {
                case "0":
                    break;
                case "1":
                    katedra.IspisiKatedre(managerKatedra.VratiSveKatedre());
                    break;
                case "2":
                    katedra.DodajKatedru();
                    break;
                case "3":
                    katedra.AzurirajKatedru();
                    break;
                case "4":
                    katedra.UkloniKatedru();
                    break;
                default:
                    System.Console.Write("Pogrešan unos!");
                    break;
            }
        }

        private void HandleProfesorMenuInput(string input)
        {
           // PovezivacProfesorPredmet();
            switch (input)
            {
                case "0":
                    break;
                case "1":
                    PovezivacProfesorPredmet();
                    profesor.IspisiProfesore(managerProfesor.VratiSveProfesore());
                    break;
                case "2":
                    profesor.DodajProfesora();
                    PovezivacProfesorPredmet();
                    break;
                case "3":
                    profesor.AzurirajProfesora();
                    PovezivacProfesorPredmet();
                    break;
                case "4":
                    profesor.UkloniProfesora();
                    PovezivacProfesorPredmet();
                    break;
                default:
                    System.Console.Write("Pogrešan unos!");
                    break;
            }
        }

        private void HandleOcenaMenuInput(string input)
        {
            switch (input)
            {
                case "0":
                    break;
                case "1":
                    ocena.IspisiOcene(managerOcena.VratiSveOcene());
                    break;
                case "2":
                    ocena.DodajOcenu();
                    break;
                case "3":
                    ocena.AzurirajOcenu();
                    break;
                case "4":
                    ocena.UkloniOcenu();
                    break;
                default:
                    System.Console.Write("Pogrešan unos!");
                    break;
            }
        }
        
        private void HandleAdresaMenuInput(string input)
        {
            switch (input)
            {
                case "0":
                    break;
                case "1":
                    adresa.IspisiAdrese(managerAdresa.VratiSveAdrese());
                    break;
                case "2":
                    adresa.DodajAdresu();
                    break;
                case "3":
                    adresa.AzurirajAdresu();
                    break;
                case "4":
                    adresa.UkloniAdresu();
                    break;
                default:
                    System.Console.Write("Pogrešan unos!");
                    break;
            }
        }
    }
}