using StudentskaSluzbaGUI.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzbaGUI.Storage;

namespace StudentskaSluzbaGUI.Model.DAO
{
    class AdresaDAO
    {
        private List<IObserver> _observers;

        private AdresaStorage _storage;
        private List<Adresa> _adrese;

        public AdresaDAO()
        {
            _storage = new AdresaStorage();
            _adrese = _storage.Ucitaj();
            _observers = new List<IObserver>();
        }

        public int GenerisiId()
        {
            if (_adrese.Count == 0) return 0;
            return Convert.ToInt32(_adrese[_adrese.Count - 1].id) + 1;
        }

        public void Add(Adresa adresa)
        {
            adresa.id = GenerisiId();
            _adrese.Add(adresa);
            _storage.Sacuvaj(_adrese);
            NotifyObservers();
        }

        public void Remove(Adresa adresa)
        {
            _adrese.Remove(adresa);
            _storage.Sacuvaj(_adrese);
            NotifyObservers();
        }

        public List<Adresa> GetAll()
        {
            return _adrese;
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
