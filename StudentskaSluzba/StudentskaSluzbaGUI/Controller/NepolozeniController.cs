using System;
using System.Collections.Generic;
using ConsoleApp1.Model;
using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Model.DAO;
using StudentskaSluzbaGUI.Observer;
using StudentskaSluzbaGUI.Serializer;

namespace StudentskaSluzbaGUI.Controller
{
    class NepolozeniController
    {
            private NepolozeniDAO _nepolozeni;

            public List<Model.NepolozeniPredmet> GetAllNepolozeni()
            {
                return _nepolozeni.GetAll();
            }


            public void Subscribe(IObserver observer)
            {
                _nepolozeni.Subscribe(observer);
            }

            private List<NepolozeniPredmet> nepolozeni;
            private Serializer<NepolozeniPredmet> serializer;

            private readonly string fileName = "nepolozeni.txt";

            public NepolozeniController()
            {
                _nepolozeni = new NepolozeniDAO();
                serializer = new Serializer<NepolozeniPredmet>();
                UcitajNepolozene();
            }



            public void UcitajNepolozene()
            {
                nepolozeni = serializer.FromCSV(fileName);
            }

            public void SacuvajNepolozene()
            {
                serializer.ToCSV(fileName, nepolozeni);
            }



            public NepolozeniPredmet DodajNepolozen(NepolozeniPredmet nepolozen)
            {
                
                nepolozeni.Add(nepolozen);
                SacuvajNepolozene();
                return nepolozen;
            }


            public NepolozeniPredmet UkloniNepolozene(string idp, string ids)
            {
                NepolozeniPredmet nepolozen= VratiNepolozenPoId(idp, ids);
                if (nepolozen == null) return null;

                nepolozeni.Remove(nepolozen);
                SacuvajNepolozene();
                return nepolozen;
            }

        public NepolozeniPredmet VratiNepolozenPoId(string idp, string ids)
        {
            List<NepolozeniPredmet> pomocnaLista = new List<NepolozeniPredmet>();
            foreach (NepolozeniPredmet np in nepolozeni)
            {
                if (nepolozeni.Find(p => p.idPredmeta == idp) != null)
                {
                    pomocnaLista.Add(np);

                }

                foreach (NepolozeniPredmet pl in pomocnaLista)
                {
                    if (nepolozeni.Find(p => p.idStudenta == ids) != null)
                    {

                        return nepolozeni.Find(p => p.idStudenta == ids);

                    }
                }

                
            }
            return null;
        }

            public List<NepolozeniPredmet> VratiSveNepolozene()
            {
                return nepolozeni;
            }
        }


    }

