using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Model.DAO;
using StudentskaSluzbaGUI.Observer;
using StudentskaSluzbaGUI.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentskaSluzbaGUI.Controller
{
    class KatedraController
    {
        private KatedraDAO katedraDAO;

        public KatedraController()
        {
            katedraDAO = new KatedraDAO();
        }

        public List<Katedra> VratiSveKatedre()
        {
            return katedraDAO.GetAll();
        }

        public void Create(Katedra katedra)
        {
            katedraDAO.Add(katedra);
        }

        public void Delete(Katedra katedra)
        {
            katedraDAO.Remove(katedra);
        }

        public void Subscribe(IObserver observer)
        {
            katedraDAO.Subscribe(observer);
        }

        public void DodajSefa(int id, string sifra)
        {
            katedraDAO.DodajSefa(id, sifra);
        }

        public void Save(List<Katedra> katedre)
        {
            katedraDAO.Save(katedre);
        }
    }
}
