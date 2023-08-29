using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentskaSluzbaGUI.Controller
{
    class AdresaController
    {
        private AdresaDAO adresaDAO;

        public AdresaController()
        {
            adresaDAO = new AdresaDAO();
        }

        public List<Adresa> VratiSveKatedre()
        {
            return adresaDAO.GetAll();
        }

        public void Create(Adresa Adresa)
        {

            adresaDAO.Add(Adresa);
        }

        public void Delete(Adresa Adresa)
        {
            adresaDAO.Remove(Adresa);
        }
    }
}
