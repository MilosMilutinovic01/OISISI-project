using System.Collections.Generic;
using System.Linq;
using StudentskaSluzbaGUI.Observer;
using StudentskaSluzbaGUI.Storage;

namespace StudentskaSluzbaGUI.Model.DAO
{
    class PredmetDAO: ISubject
    {
        private List<IObserver> _observers;

        private PredmetStorage _storage;
        private List<Predmet> _predmeti;

        public PredmetDAO()
        {
            _storage = new PredmetStorage();
            _predmeti = _storage.Ucitaj();
            _observers = new List<IObserver>();
        }

        public void Add(Predmet predmet)
        {
            _predmeti.Add(predmet);
            _storage.Sacuvaj(_predmeti);
            NotifyObservers();
        }

        public void Remove(Predmet predmet)
        {
            _predmeti.Remove(predmet);
            _storage.Sacuvaj(_predmeti);
            NotifyObservers();
        }

        public List<Predmet> GetAll()
        {
            return _predmeti;
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
    }
}
