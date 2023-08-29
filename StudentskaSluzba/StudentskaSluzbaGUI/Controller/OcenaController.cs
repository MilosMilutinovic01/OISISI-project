using System.Collections.Generic;
using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Model.DAO;
using StudentskaSluzbaGUI.Observer;
using StudentskaSluzbaGUI.Serializer;
using StudentskaSluzbaGUI.Storage;

namespace StudentskaSluzbaGUI.Controller
{
    class OcenaController
    {
        private OcenaDAO _ocene;

        public List<Ocena> GetAllOcene()
        {
            return _ocene.GetAll();
        }
        
        public void Create(Ocena ocena)
        {
            _ocene.Add(ocena);
        }

        public void Delete(Ocena ocena)
        {
            _ocene.Remove(ocena);
        }

        public void Subscribe(IObserver observer)
        {
            _ocene.Subscribe(observer);
        }

        private List<Ocena> ocene;
        private Serializer<Ocena> serializer;
        

        private readonly string fileName = "ocena.txt";

        public OcenaController()
        {
            _ocene = new OcenaDAO();
            serializer = new Serializer<Ocena>();
            UcitajOcene();
            // ps = new PredmetStorage();
            // predmeti = ps.Ucitaj();

        }
        public void UcitajOcene()
        {
            ocene = serializer.FromCSV(fileName);
        }

        public void SacuvajOcene()
        {
            serializer.ToCSV(fileName, ocene);
        }

        private int GenerisiId()
        {
            if (ocene.Count == 0) return 0;
            return System.Convert.ToInt32(ocene[ocene.Count - 1].id) + 1;
        }

        public Ocena DodajOcenu(Ocena ocena)
        {
            ocene.Add(ocena);
            SacuvajOcene();
            
            return ocena;
        }


        public Ocena UkloniOcenu(int idOcene)
        {
            Ocena ocena = VratiOcenuPoId(idOcene);
            if (ocena == null) return null;

            ocene.Remove(ocena);
            SacuvajOcene();
            return ocena;
        }

        public Ocena VratiOcenuPoId(int idOcene)
        {
            return ocene.Find(p => p.id == idOcene);
        }

        public List<Ocena> VratiSveOcene()
        {
            return ocene;
        }
    }
}
