using StudentskaSluzbaGUI.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentskaSluzbaGUI.Model
{
    public class Adresa : ISerializable
    {
        public Adresa(int id, string ulica, int adresniBroj, string grad, string drzava)
        {
            this.id = id;
            this.ulica = ulica;
            this.adresniBroj = adresniBroj;
            this.grad = grad;
            this.drzava = drzava;
        }

        public Adresa(string ulica, int adresniBroj, string grad, string drzava)
        {
            this.ulica = ulica;
            this.adresniBroj = adresniBroj;
            this.grad = grad;
            this.drzava = drzava;
        }

        public Adresa() { }

        public int id { get; set; }
        public string ulica { get; set; }
        public int adresniBroj { get; set; }
        public string grad { get; set; }
        public string drzava { get; set; }

        public override string ToString()
        {
            return string.Format(ulica + "," + Convert.ToString(adresniBroj) + "," + grad + "," + drzava);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                ulica,
                adresniBroj.ToString(),
                grad,
                drzava,

            };
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            ulica = values[1];
            adresniBroj = int.Parse(values[2]);
            grad = values[3];
            drzava = values[4];

        }

    }
}
