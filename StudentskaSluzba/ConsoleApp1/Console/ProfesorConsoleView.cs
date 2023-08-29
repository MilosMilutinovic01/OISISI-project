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

    class ProfesorConsoleView : ConsoleView
    {
        private ProfesorManager manager;
        public ProfesorConsoleView(ProfesorManager manager)
        {
            this.manager = manager;
        }

        public void IspisiProfesore(List<Profesor> profesori)
        {
            System.Console.WriteLine("Profesori: ");
            //string header = string.Format("ID {0,2} | Ime {1,4} | Prezime {2,4} | Datum rodjenja {3,4} | Kontakt telefon {4,4} | Mejl {5,4} | Broj licne karte {6,4} | Zvanje {7,4} | Godine staza {8,4} |  ", "", "", "", "", "", "", "", "", "");

            //System.Console.WriteLine(header);
            foreach (Profesor p in profesori)
            {

                System.Console.WriteLine(p + "\n");
            }
        }


        public Profesor UnesiProfesora()

        {
            Profesor profesor = new Profesor();

            System.Console.Write("Unesi ime profesora: ");
            string ime = System.Console.ReadLine();
            profesor.ime = ime;

            System.Console.Write("Unesi prezime profesora: ");
            string prezime = System.Console.ReadLine();
            profesor.prezime = prezime;

            System.Console.Write("Unesi datum rodjenja: ");
            DateTime datum = Convert.ToDateTime(System.Console.ReadLine());
            profesor.datumRodjenja = datum;

            System.Console.Write("Unesi adresu stanovanja: ");
            profesor.adresaStanovanja = AdresaConsoleView.UnesiAdresu();

            List<Adresa> adrese = new List<Adresa>();
            string fileName = "adrese.txt";
            Serializer<Adresa> serializer = new Serializer<Adresa>();
            AdresaManager manager = new AdresaManager();
            adrese = serializer.FromCSV(fileName);
            manager.DodajAdresu(profesor.adresaStanovanja);

            System.Console.Write("Unesi kontakt telefon: ");
            string tel = System.Console.ReadLine();
            profesor.kontaktTelefon = tel;

            System.Console.Write("Unesi mejl: ");
            string mejl = System.Console.ReadLine();
            profesor.emailAdresa = mejl;

            System.Console.Write("Unesi adresu kancelarije: ");
            profesor.adresaKancelarije = AdresaConsoleView.UnesiAdresu();

            manager.DodajAdresu(profesor.adresaKancelarije);

            System.Console.Write("Unesi broj licne karte: ");
            string blk = System.Console.ReadLine();
            profesor.brojLicneKarte = blk;

            System.Console.Write("Unesi zvanje profesora: ");
            string zv= System.Console.ReadLine();
            profesor.zvanje = zv;

            System.Console.Write("Unesi godine staza: ");
            int gs = Convert.ToInt32(System.Console.ReadLine());
            profesor.godineStaza = gs;

            return profesor;
        }

        private int UnesiID()

        {
            System.Console.WriteLine("Unesi ID profesora: ");
            int id = Convert.ToInt32(System.Console.ReadLine());
            return id;
        }


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
                    IspisiProfesore(manager.VratiSveProfesore());
                    break;
                case "2":
                    DodajProfesora();
                    break;
                case "3":
                    AzurirajProfesora();
                    break;
                case "4":
                    UkloniProfesora();
                    break;

            }
        }

        public void UkloniProfesora()

        {
            int id = UnesiID();
            Profesor uklonjenProfesor = manager.UkloniProfesora(id);
            if (uklonjenProfesor == null)
            {
                System.Console.WriteLine("Profesor nije pronadjen!");
                return;
            }
            System.Console.WriteLine("Profesora uklonjen");
        }


        public void AzurirajProfesora()

        {
            int id = UnesiID();
            Profesor profesor = UnesiProfesora();
            profesor.id = id;
            Profesor azuriranProfesor = manager.AzurirajProfesora(profesor);
            if (azuriranProfesor == null)
            {
                System.Console.WriteLine("Profesor nije pronadjen!");
                return;
            }
            System.Console.WriteLine("Profesor azuriran!");
        }


        public void DodajProfesora()

        {
            Profesor profesor = UnesiProfesora();
            manager.DodajProfesora(profesor);
            System.Console.WriteLine("Profesor dodat!");
        }


        private void ShowMenu()
        {
            System.Console.WriteLine("\nIzaberi opciju: ");
            System.Console.WriteLine("1: Prikaži sve profesore");
            System.Console.WriteLine("2: Dodaj profesora");
            System.Console.WriteLine("3: Ažuriraj profesora");
            System.Console.WriteLine("4: Ukloni profesora");
            System.Console.WriteLine("0: Zatvori");
        }

    }
}