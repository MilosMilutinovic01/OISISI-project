using System.Collections.Generic;
using ConsoleApp1.Model;
using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Serializer;
using StudentskaSluzbaGUI.View;

namespace StudentskaSluzbaGUI.Storage
{
    class NepolozeniStorage
    {
        private string StoragePath = ".." + MainWindow.sep + ".." + MainWindow.sep + "Data" + MainWindow.sep + "nepolozeni.txt";

        private Serializer<NepolozeniPredmet> _serializer;


        public NepolozeniStorage()
        {
            _serializer = new Serializer<NepolozeniPredmet>();
        }

        public List<NepolozeniPredmet> Ucitaj()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Sacuvaj(List<NepolozeniPredmet> nepolozeni)
        {
            _serializer.ToCSV(StoragePath, nepolozeni);
        }


    }
}