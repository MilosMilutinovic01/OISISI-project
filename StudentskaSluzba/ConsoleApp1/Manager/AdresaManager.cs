using ConsoleApp1.Manager.Serialization;
using System.Collections.Generic;
using System;
using ConsoleApp1.Model;

namespace ConsoleApp1.Manager
{
    class AdresaManager
    {
        private List<Adresa> adrese;
        private Serializer<Adresa> serializer;

        private readonly string fileName = "adrese.txt";

        public AdresaManager()
        {
            serializer = new Serializer<Adresa>();
            UcitajAdrese();
        }

        public void UcitajAdrese()
        {
            adrese = serializer.FromCSV(fileName);
        }

        public void SacuvajAdrese()
        {
            serializer.ToCSV(fileName, adrese);
        }

        public int GenerisiId()
        {
            if (adrese.Count == 0) return 0;
            return Convert.ToInt32(adrese[adrese.Count - 1].id) + 1;
        }

        public Adresa DodajAdresu(Adresa adresa)
        {
            adresa.id = GenerisiId();
            adrese.Add(adresa);
            SacuvajAdrese();
            return adresa;
        }

        public Adresa AzurirajAdresu(Adresa adresa)
        {
            Adresa staraAdresa = VratiAdresuPoId(adresa.id);
            if (staraAdresa == null) return null;

            staraAdresa.adresniBroj = adresa.adresniBroj;
            staraAdresa.ulica = adresa.ulica;
            staraAdresa.grad = adresa.grad;
            staraAdresa.drzava = adresa.drzava;


            SacuvajAdrese();
            return staraAdresa;
        }

        public Adresa UkloniAdresu(int id)
        {
            Adresa adresa = VratiAdresuPoId(id);
            if (adresa == null) return null;

            adrese.Remove(adresa);
            SacuvajAdrese();
            return adresa;
        }

        public Adresa VratiAdresuPoId(int id)
        {
            return adrese.Find(a => a.id == id);
        }

        public List<Adresa> VratiSveAdrese()
        {
            return adrese;
        }
    }
}