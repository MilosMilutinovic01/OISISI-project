using System;
using System.Collections.Generic;
using StudentskaSluzbaGUI.Controller;
using StudentskaSluzbaGUI.Serializer;

namespace StudentskaSluzbaGUI.Model
{
    public class Katedra : ISerializable
    {
        private string sifraKatedre;
        public string SifraKatedre
        {
            get => sifraKatedre;
            set
            {
                if (value != sifraKatedre)
                {
                    sifraKatedre = value;
                }
            }
        }
        private string nazivKatedre;
        public string NazivKatedre
        {
            get { return nazivKatedre; }
            set { nazivKatedre = value; }
        }
        private int sefKatedre;
        public int SefKatedre
        {
            get { return sefKatedre; }
            set { sefKatedre = value; }
        }
        private string sefKat;
        public string SefKat
        {
            get
            {
                PorfesorController managerProfesor = new PorfesorController();
                List<Profesor> profesori = managerProfesor.VratiSveProfesore();
                foreach (Profesor p in profesori)
                {
                    if (p.Id == sefKatedre)
                        sefKat = p.Ime + " " + p.Prezime;
                    else if (sefKatedre == -1)
                        sefKat = "Nema!";
                }

                return sefKat;
            }
            set { sefKat = value; }
        }
        public List<Profesor> spisakProfesora;

        public Katedra(string sifraKatedre, string nazivKatedre, int sefKatedre)
        {
            this.sifraKatedre = sifraKatedre;
            this.nazivKatedre = nazivKatedre;
            this.sefKatedre = sefKatedre;
        }

        public Katedra()
        {
            spisakProfesora = new List<Profesor>();
        }

        public string[] ToCSV()
        {
            string spisakProf = "";
            foreach (Profesor p in spisakProfesora)
            {
                spisakProf += p.Id.ToString() + ' ';
            }
            string[] csvValues =
            {
                sifraKatedre,
                nazivKatedre,
                sefKatedre.ToString(),
                spisakProf
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            string spisak;
            sifraKatedre = values[0];
            nazivKatedre = values[1];
            if (values[2] == "")
                sefKatedre = -1;
            else
                sefKatedre = Convert.ToInt32(values[2]);
            PorfesorController managerProfesor = new PorfesorController();
            List<Profesor> profesori = managerProfesor.VratiSveProfesore();
            spisak = values[3];
            string[] deloviSpiska = spisak.TrimEnd().Split(' ');
            foreach (String s in deloviSpiska)
            {
                spisakProfesora.Add(managerProfesor.VratiProfesoraPoId(Convert.ToInt32(s)));
            }
        }
    }
}
