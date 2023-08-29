using System.Collections.Generic;
using System.Linq;
using StudentskaSluzbaGUI.Observer;
using StudentskaSluzbaGUI.Storage;

namespace StudentskaSluzbaGUI.Model.DAO
{
    class StudentDAO : ISubject
    {
        private List<IObserver> _observers;

        private StudentStorage _storage;
        private List<Student> _students;

        public StudentDAO()
        {
            _storage = new StudentStorage();
            _students = _storage.Ucitaj();
            _observers = new List<IObserver>();
        }

        public void Add(Student student)
        {
            _students.Add(student);
            _storage.Sacuvaj(_students);
            NotifyObservers();
        }

        public void Remove(Student student)
        {
            _students.Remove(student);
            _storage.Sacuvaj(_students);
            NotifyObservers();
        }

        public List<Student> GetAll()
        {
            return _students;
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
