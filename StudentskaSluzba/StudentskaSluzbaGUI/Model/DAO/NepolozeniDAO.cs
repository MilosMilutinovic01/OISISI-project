using System.Collections.Generic;
using System.Linq;
using StudentskaSluzbaGUI.Observer;
using StudentskaSluzbaGUI.Storage;


namespace StudentskaSluzbaGUI.Model.DAO
{
    class NepolozeniDAO : ISubject
    {
        private List<IObserver> _observers;

        private NepolozeniStorage _storage;
        private List<NepolozeniPredmet> _np;

        public NepolozeniDAO()
        {
            _storage = new NepolozeniStorage();
            _np= _storage.Ucitaj();
            _observers = new List<IObserver>();
        }

        public void Add(NepolozeniPredmet nep)
        {
            _np.Add(nep);
            _storage.Sacuvaj(_np);
            NotifyObservers();
        }

        public void Remove(NepolozeniPredmet nep)
        {
            _np.Remove(nep);
            _storage.Sacuvaj(_np);
            NotifyObservers();
        }

        public List<NepolozeniPredmet> GetAll()
        {
            return _np;
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

