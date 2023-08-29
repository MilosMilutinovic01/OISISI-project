using ConsoleApp1.Manager.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public enum Semestar { L, Z };

    public class Predmet : Serializable
    {
        public string sifraPredmeta { get; set; }
        public string nazivPredmeta { get; set; }
        public Semestar semestar { get; set; }
        public int godinaStudija { get; set; }
        //public string predmetniProfesor { get; set; }
        public int brojESPB { get; set; }
        public List<Student> polozili { get; set; }
        public List<Student> nisuPolozili { get; set; }
        public int ProfesorId { get; set; } //moramo imati id profesora
        public Profesor Profesor { get; set; }
        public List<Student> Studenti { get; set; }

        public Predmet(string sifraPredmeta, string nazivPredmeta, Semestar semestar, int godinaStudija, string predmetniProfesor, int brojESPB, int profesorId)
        {
            this.sifraPredmeta = sifraPredmeta;
            this.nazivPredmeta = nazivPredmeta;
            this.semestar = semestar;
            this.godinaStudija = godinaStudija;
            //this.predmetniProfesor = predmetniProfesor;
            this.brojESPB = brojESPB;
            this.polozili = polozili;
            this.nisuPolozili = nisuPolozili;
            this.ProfesorId = profesorId;
            Studenti = new List<Student>();

            polozili = new List<Student>();
            nisuPolozili = new List<Student>(); 
        }

        public Predmet()
        {
            Studenti = new List<Student>();
            polozili = new List<Student>();
            nisuPolozili = new List<Student>();
        }

        public override string ToString()
        {

            if (Profesor == null)
            {
                if (semestar == Semestar.L)
                {
                    return "\tSifra predmeta: " + sifraPredmeta + "\n\tNaziv predmeta: " + nazivPredmeta
                    + "\n\tSemestar: letnji\n\tGodina studija: " + Convert.ToString(godinaStudija)
                    + /*"\n\tPredmetni profesor: " + predmetniProfesor +*/ "\n\tBroj ESPB bodova: " + Convert.ToString(brojESPB);
                }
                else
                {
                    return "\tSifra predmeta: " + sifraPredmeta + "\n\tNaziv predmeta: " + nazivPredmeta
                    + "\n\tSemestar: zimski\n\tGodina studija: " + Convert.ToString(godinaStudija)
                    + /*"\n\tPredmetni profesor: " + predmetniProfesor +*/ "\n\tBroj ESPB bodova: " + Convert.ToString(brojESPB);
                }
            }
            else
            {
                if (semestar == Semestar.L)
                {
                    return "\tSifra predmeta: " + sifraPredmeta + "\n\tNaziv predmeta: " + nazivPredmeta
                    + "\n\tSemestar: letnji\n\tGodina studija: " + Convert.ToString(godinaStudija)
                    + "\n\tPredmetni profesor: " + Profesor.ime + " " + Profesor.prezime + "\n\tBroj ESPB bodova: " + Convert.ToString(brojESPB);
                }
                else
                {
                    return "\tSifra predmeta: " + sifraPredmeta + "\n\tNaziv predmeta: " + nazivPredmeta
                    + "\n\tSemestar: zimski\n\tGodina studija: " + Convert.ToString(godinaStudija)
                    + "\n\tPredmetni profesor: " + Profesor.ime + " " + Profesor.prezime + "\n\tBroj ESPB bodova: " + Convert.ToString(brojESPB);
                }

            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                sifraPredmeta,
                nazivPredmeta,
                semestar.ToString(),
                godinaStudija.ToString(),
                //predmetniProfesor,
                brojESPB.ToString(),
                ProfesorId.ToString()//profesor id
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            sifraPredmeta = values[0];
            nazivPredmeta = values[1];
            if (values[2].Equals("L"))
                semestar = Semestar.L;
            else
                semestar = Semestar.Z;
            godinaStudija = int.Parse(values[3]);
            //predmetniProfesor = values[4];
            brojESPB = int.Parse(values[4]);
            ProfesorId = Convert.ToInt32(values[5]);//profesor id
        }
    }
}
