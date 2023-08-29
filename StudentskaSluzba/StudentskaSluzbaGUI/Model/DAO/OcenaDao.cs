using System.Collections.Generic;
using System.Linq;
using StudentskaSluzbaGUI.Observer;
using StudentskaSluzbaGUI.Storage;
using StudentskaSluzbaGUI.View;

namespace StudentskaSluzbaGUI.Model.DAO
{
    class OcenaDAO : ISubject
    {
        private List<IObserver> _observers;

        private OcenaStorage _storage;
        private List<Ocena> _ocene;

        public OcenaDAO()
        {
            _storage = new OcenaStorage();
            _ocene = _storage.Ucitaj();
            _observers = new List<IObserver>();
        }

        public void Add(Ocena ocena)
        {
            _ocene.Add(ocena);
            _storage.Sacuvaj(_ocene);
            NotifyObservers();
        }

        public void Remove(Ocena ocena)
        {
            _ocene.Remove(ocena);
            _storage.Sacuvaj(_ocene);
            NotifyObservers();
        }

        public List<Ocena> GetAll()
        {
            return _ocene;
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
