using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzbaGUI.Observer;
using StudentskaSluzbaGUI.Storage;

namespace StudentskaSluzbaGUI.Model.DAO
{
    class ProfesorDAO : ISubject
    {
        private List<IObserver> _observers;

        private ProfesorStorage _storage;
        private List<Profesor> _profesors;

        public ProfesorDAO()
        {
            _storage = new ProfesorStorage();
            _profesors = _storage.Ucitaj();
            _observers = new List<IObserver>();
        }

        public void Add(Profesor profesor)
        {
            _profesors.Add(profesor);
            _storage.Sacuvaj(_profesors);
            NotifyObservers();
        }

        public void Remove(Profesor profesor)
        {
            _profesors.Remove(profesor);
            _storage.Sacuvaj(_profesors);
            NotifyObservers();
        }

        public List<Profesor> GetAll()
        {
            return _profesors;
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
