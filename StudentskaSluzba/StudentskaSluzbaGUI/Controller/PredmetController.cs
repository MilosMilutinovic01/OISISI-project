using System.Collections.Generic;
using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Model.DAO;
using StudentskaSluzbaGUI.Observer;
using StudentskaSluzbaGUI.Serializer;
using StudentskaSluzbaGUI.Storage;

namespace StudentskaSluzbaGUI.Controller
{
    class PredmetController
    {
        /*private PredmetDAO _predmeti;

        public List<Predmet> GetAllStudents()
        {
            return _predmeti.GetAll();
        }
        */
        public void Create(Predmet predmet)
        {
            predmeti.Add(predmet);
        }
        
        public void Delete(Predmet predmet)
        {
            predmeti.Remove(predmet);
        }
        /*
        public void Subscribe(IObserver observer)
        {
            _predmeti.Subscribe(observer);
        }*/

        private List<Predmet> predmeti;
        //private Serializer<Predmet> serializer;
        private PredmetStorage ps;

        //private readonly string fileName = "predmeti.txt";

        public PredmetController()
        {
            //_predmeti = new PredmetDAO();
            //serializer = new Serializer<Predmet>();
            ps = new PredmetStorage();
            predmeti = ps.Ucitaj();
        }

        /*public void UcitajPredmete()
        {
            predmeti = serializer.FromCSV(fileName);
        }

        public void SacuvajPredmete()
        {
            serializer.ToCSV(fileName, predmeti);
        }*/

        /*private int GenerisiId()
        {
            if (studenti.Count == 0) return 0;
            return Convert.ToInt32(studenti[studenti.Count - 1].brojIndeksa) + 1;
        }*/

        public Predmet DodajPredmet(Predmet predmet)
        {
            predmeti.Add(predmet);
            //SacuvajPredmete();
            ps.Sacuvaj(predmeti);
            return predmet;
        }


        public Predmet UkloniPredmet(string sifraPredmeta)
        {
            Predmet predmet = VratiPredmetPoId(sifraPredmeta);
            if (predmet == null) return null;

            predmeti.Remove(predmet);
            //SacuvajPredmete();
            ps.Sacuvaj(predmeti);
            return predmet;
        }

        public Predmet VratiPredmetPoId(string sifraPredmeta)
        {
            return predmeti.Find(p => p.SifraPredmeta == sifraPredmeta);
        }

        public List<Predmet> VratiSvePredmete()
        {
            return predmeti;
        }
    }
}
