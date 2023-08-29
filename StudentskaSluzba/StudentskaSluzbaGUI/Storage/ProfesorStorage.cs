using System.Collections.Generic;
using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Serializer;
using StudentskaSluzbaGUI.View;

namespace StudentskaSluzbaGUI.Storage
{
    class ProfesorStorage
    {
        private string StoragePath = ".." + MainWindow.sep + ".." + MainWindow.sep + "Data" + MainWindow.sep + "profesori.txt";

        private Serializer<Profesor> _serializer;


        public ProfesorStorage()
        {
            _serializer = new Serializer<Profesor>();
        }

        public List<Profesor> Ucitaj()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Sacuvaj(List<Profesor> profesori)
        {
            _serializer.ToCSV(StoragePath, profesori);
        }
    }
}
