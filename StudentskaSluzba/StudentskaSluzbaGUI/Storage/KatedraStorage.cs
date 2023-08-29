using System.Collections.Generic;
using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Serializer;
using StudentskaSluzbaGUI.View;

namespace StudentskaSluzbaGUI.Storage
{
    class KatedraStorage
    {
        private string StoragePath = ".." + MainWindow.sep + ".." + MainWindow.sep + "Data" + MainWindow.sep + "katedra.txt";

        private Serializer<Katedra> _serializer;


        public KatedraStorage()
        {
            _serializer = new Serializer<Katedra>();
        }

        public List<Katedra> Ucitaj()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Sacuvaj(List<Katedra> katedre)
        {
            _serializer.ToCSV(StoragePath, katedre);
        }
    }
}
