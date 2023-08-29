using StudentskaSluzbaGUI.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzbaGUI.Storage;

namespace StudentskaSluzbaGUI.Model.DAO
{
    class KatedraDAO : ISubject
    {
        private List<IObserver> _observers;

        private KatedraStorage _storage;
        private List<Katedra> katedre;

        public KatedraDAO()
        {
            _storage = new KatedraStorage();
            katedre = _storage.Ucitaj();
            _observers = new List<IObserver>();
        }

        public void Save(List<Katedra> katedreee)
        {
            _storage.Sacuvaj(katedreee);
            NotifyObservers();
        }

        public void Add(Katedra katedra)
        {
            katedre.Add(katedra);
            _storage.Sacuvaj(katedre);
            NotifyObservers();
        }

        public void Remove(Katedra katedra)
        {
            katedre.Remove(katedra);
            _storage.Sacuvaj(katedre);
            NotifyObservers();
        }

        public List<Katedra> GetAll()
        {
            return katedre;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

        public void DodajSefa(int id, string sifra)
        {
            foreach (Katedra k in katedre)
            {
                foreach (Profesor p in k.spisakProfesora)
                {
                    if (p.Id == id && k.SifraKatedre == sifra)
                    {
                        k.SefKat = p.Ime + ' ' + p.Prezime;
                        k.SefKatedre = id;
                    }
                }
            }
            _storage.Sacuvaj(katedre);
        }
    }
}
