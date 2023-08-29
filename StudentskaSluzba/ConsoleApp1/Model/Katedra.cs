using ConsoleApp1.Manager.Serialization;
using ConsoleApp1.Manager;
using ConsoleApp1.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class Katedra : Serializable
    {
        public string sifraKatedre;
        public string nazivKatedre;
        public int sefKatedre;
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
            /*
            ProfesorManager m = new ProfesorManager();
            Profesor p1 = m.VratiProfesoraPoId(0);
            Profesor p2 = m.VratiProfesoraPoId(1);
            Profesor p3 = m.VratiProfesoraPoId(2);
            spisakProfesora.Add(p1);
            spisakProfesora.Add(p2);
            spisakProfesora.Add(p3);
            */
        }

        public override string ToString()
        {
            if(sefKatedre < 0)
            {
                return "\tSifra katedre: " + sifraKatedre + "\n\tNaziv katedre: " + nazivKatedre + "\n\tSef katedre: nema";
            }
            else
            {
                return "\tSifra katedre: " + sifraKatedre + "\n\tNaziv katedre: " + nazivKatedre + "\n\tSef katedre: ";
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                sifraKatedre,
                nazivKatedre,
                sefKatedre.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            sifraKatedre = values[0];
            nazivKatedre = values[1];
            sefKatedre = Convert.ToInt32(values[2]);
        }
    }
}
