using ConsoleApp1.Console;
using ConsoleApp1.Manager;
using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Console
{

    class AdresaConsoleView : ConsoleView
    {
        private AdresaManager manager;
        public AdresaConsoleView(AdresaManager manager)
        {
            this.manager = manager;
        }

        public void IspisiAdrese(List<Adresa> adrese)
        {
            System.Console.WriteLine("Adrese: ");
            //string header = string.Format("ID {0,11} | Ulica {1,21} | Adresni broj {2,30} |", "", "", "");
            //System.Console.WriteLine(header);
            foreach (Adresa a in adrese)
            {
                System.Console.WriteLine(a);
            }
        }

        static public Adresa UnesiAdresu()
        {
            Adresa adresa = new Adresa();

            System.Console.Write("\nUnesi ulicu: ");
            string ulica = System.Console.ReadLine();
            adresa.ulica = ulica;

            System.Console.Write("Unesi adresni broj: ");
            int adresniBroj = Convert.ToInt32(System.Console.ReadLine());
            adresa.adresniBroj = adresniBroj;


            System.Console.Write("Unesi grad: ");
            string grad = System.Console.ReadLine();
            adresa.grad = grad;

            System.Console.Write("Unesi drzavu: ");
            string drzava = System.Console.ReadLine();
            adresa.drzava= drzava;
            
            return adresa;
        }

        private int UnesiID()
        {
            System.Console.Write("Unesi ID adrese: ");
            int id = Convert.ToInt32(System.Console.ReadLine());
            return id;
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
                    IspisiAdrese(manager.VratiSveAdrese());
                    break;
                case "2":
                    DodajAdresu();
                    break;
                case "3":
                    AzurirajAdresu();
                    break;
                case "4":
                    UkloniAdresu();
                    break;

            }
        }
        */
        public void UkloniAdresu()
        {
            int id = UnesiID();
            Adresa uklonjenaAdresa = manager.UkloniAdresu(id);
            if (uklonjenaAdresa == null)
            {
                System.Console.WriteLine("Adresa nije pronadjena!");
                return;
            }
            System.Console.WriteLine("Adresa uklonjena");
        }

        public void AzurirajAdresu()
        {
            //int id = UnesiID();
            Adresa adresa = UnesiAdresu();
            //adresa.id = id;
            Adresa azuriranaAdresa = manager.AzurirajAdresu(adresa);
            if (azuriranaAdresa == null)
            {
                System.Console.WriteLine("Adresa nije pronadjena!");
                return;
            }
            System.Console.WriteLine("Adresa azurirana!");
        }

        public void DodajAdresu()
        {
            Adresa adresa = UnesiAdresu();
            manager.DodajAdresu(adresa);
            System.Console.WriteLine("Adresa dodata!");
        }
        /*
        public void ShowMenu()
        {
            System.Console.WriteLine("\nIzaberi opciju: ");
            System.Console.WriteLine("1: Prikaži sve adrese");
            System.Console.WriteLine("2: Dodaj adresu");
            System.Console.WriteLine("3: Ažuriraj adresu");
            System.Console.WriteLine("4: Ukloni adresu");
            System.Console.WriteLine("0: Zatvori");
        }
        */
    }
}