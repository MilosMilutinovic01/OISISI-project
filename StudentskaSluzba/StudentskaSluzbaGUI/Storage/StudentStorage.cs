using System.Collections.Generic;
using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Serializer;
using StudentskaSluzbaGUI.View;

namespace StudentskaSluzbaGUI.Storage
{
    class StudentStorage
    {
        private string StoragePath = ".." + MainWindow.sep + ".." + MainWindow.sep + "Data" + MainWindow.sep + "studenti.txt";

        private Serializer<Student> _serializer;


        public StudentStorage()
        {
            _serializer = new Serializer<Student>();
        }

        public List<Student> Ucitaj()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Sacuvaj(List<Student> studenti)
        {
            _serializer.ToCSV(StoragePath, studenti);
        }
    }
}
