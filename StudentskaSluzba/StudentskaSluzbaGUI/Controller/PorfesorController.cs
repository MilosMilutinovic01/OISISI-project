using System;
using System.Collections.Generic;
using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Model.DAO;
using StudentskaSluzbaGUI.Observer;
using StudentskaSluzbaGUI.Serializer;
using StudentskaSluzbaGUI.Storage;

namespace StudentskaSluzbaGUI.Controller
{
    class PorfesorController
    {
        /*private ProfesorDAO _profesors;

        public List<Profesor> GetAllPrefosors()
        {
            return _profesors.GetAll();
        }*/
        public void Create(Profesor profesor)
        {
            profesor.Id = GenerisiId();
            profesori.Add(profesor);
        }
        public void Delete(Profesor profesor)
        {
            profesori.Remove(profesor);
        }
        /*
        public void Subscribe(IObserver observer)
        {
            _profesors.Subscribe(observer);
        }
        */
        private List<Profesor> profesori;
        //private Serializer<Profesor> serializer;
        private ProfesorStorage ps;

        //private readonly string fileName = "profesori.txt";

        public PorfesorController()
        {
            //_profesors = new ProfesorDAO();
            //serializer = new Serializer<Profesor>();
            ps = new ProfesorStorage();
            profesori = ps.Ucitaj();
        }

        public int GenerisiId()
        {
            if (profesori.Count == 0) return 0;
            return Convert.ToInt32(profesori[profesori.Count - 1].Id) + 1;
        }

        /*public void UcitajProfesore()
        {
            profesori = serializer.FromCSV(fileName);
        }

        public void SacuvajProfesore()
        {
            serializer.ToCSV(fileName, profesori);
        }*/

        /*private int GenerisiId()
        {
            if (studenti.Count == 0) return 0;
            return Convert.ToInt32(studenti[studenti.Count - 1].brojIndeksa) + 1;
        }*/

        public Profesor DodajProfesora(Profesor profesor)
        {
            profesor.Id = GenerisiId();
            profesori.Add(profesor);
            ps.Sacuvaj(profesori);
            return profesor;
        }


        public Profesor UkloniSProfesora(int id)
        {
            Profesor profesor = VratiProfesoraPoId(id);
            if (profesor == null) return null;

            profesori.Remove(profesor);
            ps.Sacuvaj(profesori);
            return profesor;
        }

        public Profesor VratiProfesoraPoId(int id)
        {
            return profesori.Find(p => p.Id == id);
        }

        public List<Profesor> VratiSveProfesore()
        {
            return profesori;
        }
    }
}
