using ConsoleApp1.Manager.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class Ocena : Serializable
    {
        public int id { get; set; }
        public string studentKojiJePolozio { get; set; }   //indeks studenta
        public string predmet { get; set; }    //sifra predmeta
        public int ocenaIspita { get; set; }
        public string datumPolaganjaIspita { get; set; }

        public Ocena(int id, string studentKojiJePolozio, string predmet, int ocenaIspita, string datumPolaganjaIspita)
        {
            this.id = id;
            this.studentKojiJePolozio = studentKojiJePolozio;
            this.predmet = predmet;
            this.ocenaIspita = ocenaIspita;
            this.datumPolaganjaIspita = datumPolaganjaIspita;
        }

        public Ocena() { }
        
        public override string ToString()
        {
            return string.Format("\n\tID: " + id + "\n\tStudent: " + studentKojiJePolozio + "\n\tPredmet: " + predmet +
                "\n\tOcena: " + Convert.ToString(ocenaIspita) + "\n\tDatum polaganja ispita: " + datumPolaganjaIspita);
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                studentKojiJePolozio,
                predmet,
                ocenaIspita.ToString(),
                datumPolaganjaIspita

            };
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            studentKojiJePolozio = values[1];
            predmet = values[2];
            ocenaIspita = int.Parse(values[3]);
            datumPolaganjaIspita = values[4];

        }








    }
}
