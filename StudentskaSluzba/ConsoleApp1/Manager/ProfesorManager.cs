using ConsoleApp1.Manager.Serialization;
using System.Collections.Generic;
using System;
using ConsoleApp1.Model;

namespace ConsoleApp1.Manager
{
    public class ProfesorManager
    {
        public List<Profesor> profesori;
        public Serializer<Profesor> serializer;

        private readonly string fileName = "profesori.txt";

        /*
    private List<Predmet> predmeti;
    private Serializer<Predmet> serializerPredmeta;
    private readonly string fileNameP = "predmeti.txt";
        */




        public ProfesorManager()
        {
            serializer = new Serializer<Profesor>();
            UcitajProfesore();
        }

        public void UcitajProfesore()
        {
            profesori = serializer.FromCSV(fileName);
        }

        public void SacuvajProfesore()
        {
            serializer.ToCSV(fileName, profesori);
        }

        public int GenerisiId()
        {
            if (profesori.Count == 0) return 0;
            return Convert.ToInt32(profesori[profesori.Count - 1].id) + 1;
        }

        public Profesor DodajProfesora(Profesor profesor)
        {
            profesor.id = GenerisiId();
            profesori.Add(profesor);


            /*
            serializerPredmeta = new Serializer<Predmet>();
            predmeti = serializerPredmeta.FromCSV(fileNameP);

            foreach (Predmet predmet in predmeti)
            {
                profesor = profesori.Find(p => p.id == predmet.ProfesorId);
                profesor.predmeti.Add(predmet);
                predmet.Profesor = profesor;
            }
            */
            SacuvajProfesore();
            return profesor;

        }

        public Profesor AzurirajProfesora(Profesor profesor)
        {
            Profesor stariProfesor = VratiProfesoraPoId(profesor.id);
            if (stariProfesor == null) return null;

            stariProfesor.ime = profesor.ime;
            stariProfesor.prezime = profesor.prezime;
            stariProfesor.datumRodjenja = profesor.datumRodjenja;
            stariProfesor.adresaStanovanja = profesor.adresaStanovanja;
            stariProfesor.kontaktTelefon = profesor.kontaktTelefon;
            stariProfesor.emailAdresa = profesor.emailAdresa;
            stariProfesor.adresaKancelarije = profesor.adresaKancelarije;
            stariProfesor.brojLicneKarte = profesor.brojLicneKarte;
            stariProfesor.zvanje = profesor.zvanje;
            stariProfesor.godineStaza = profesor.godineStaza;
            stariProfesor.predmeti = stariProfesor.predmeti;
            stariProfesor.idKat = profesor.idKat;


            SacuvajProfesore();
            return stariProfesor;
        }

        public Profesor UkloniProfesora(int id)
        {
            List<Katedra> katedre = new List<Katedra>();
            string fileName = "katedre.txt";
            Serializer<Katedra> serializer = new Serializer<Katedra>();
            katedre = serializer.FromCSV(fileName);

            Profesor profesor = VratiProfesoraPoId(id);
            if (profesor == null) return null;

            List<Katedra> kat = new List<Katedra>();
            if (katedre.Find(k => k.sefKatedre == id) != null)
            {
                kat = katedre.FindAll(k => k.sefKatedre == id);
                katedre.RemoveAll(k => k.sefKatedre == id);
                foreach(Katedra ka in kat)
                {
                    ka.sefKatedre = -1;
                    ka.spisakProfesora.RemoveAll(pr => pr.id == id);
                }
                foreach (Katedra ka in kat)
                {
                    katedre.Add(ka);
                }
                serializer.ToCSV(fileName, katedre);
            }
            

            profesori.Remove(profesor);
            SacuvajProfesore();
            return profesor;
        }

        public Profesor VratiProfesoraPoId(int id)
        {
            return profesori.Find(v => v.id == id);
        }

        public List<Profesor> VratiSveProfesore()
        {
            return profesori;
        }
    }
}
