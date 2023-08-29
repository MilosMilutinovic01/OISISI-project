using System.Collections.Generic;
using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Serializer;
using StudentskaSluzbaGUI.View;

namespace StudentskaSluzbaGUI.Storage
{
    class PredmetStorage
    {
        private string StoragePath = ".." + MainWindow.sep + ".." + MainWindow.sep + "Data" + MainWindow.sep + "predmeti.txt";

        private Serializer<Predmet> _serializer;


        public PredmetStorage()
        {
            _serializer = new Serializer<Predmet>();
        }

        public List<Predmet> Ucitaj()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Sacuvaj(List<Predmet> predmeti)
        {
            _serializer.ToCSV(StoragePath, predmeti);
        }
    }
}
