using ConsoleApp1.Manager.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class Adresa : Serializable
    {
        public Adresa(int id,string ulica, int adresniBroj, string grad, string drzava)
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
            return string.Format("\n\t-Ulica: " + ulica + "\n\t-Adresni broj:" + Convert.ToString(adresniBroj) + "\n\t-Grad: " + grad + "\n\t-Drzava:  " + drzava);
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
