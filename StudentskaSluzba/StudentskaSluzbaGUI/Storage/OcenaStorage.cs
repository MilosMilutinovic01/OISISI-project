using System.Collections.Generic;
using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Serializer;
using StudentskaSluzbaGUI.View;

namespace StudentskaSluzbaGUI.Storage
{
    class OcenaStorage
    {
        private string StoragePath = ".." + MainWindow.sep + ".." + MainWindow.sep + "Data" + MainWindow.sep + "ocena.txt";

        private Serializer<Ocena> _serializer;


        public  OcenaStorage()
        {
            _serializer = new Serializer<Ocena>();
        }

        public List<Ocena> Ucitaj()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Sacuvaj(List<Ocena> ocene)
        {
            _serializer.ToCSV(StoragePath, ocene);
        }


    }
}
