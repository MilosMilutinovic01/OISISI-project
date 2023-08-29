using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Serializer;
using StudentskaSluzbaGUI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentskaSluzbaGUI.Storage
{
    class AdresaStorage
    {
        private string StoragePath = ".." + MainWindow.sep + ".." + MainWindow.sep + "Data" + MainWindow.sep + "adrese.txt";

        private Serializer<Adresa> _serializer;


        public AdresaStorage()
        {
            _serializer = new Serializer<Adresa>();
        }

        public List<Adresa> Ucitaj()
        {
            return _serializer.FromCSV(StoragePath);
        }

        public void Sacuvaj(List<Adresa> adrese)
        {
            _serializer.ToCSV(StoragePath, adrese);
        }
    }
}
