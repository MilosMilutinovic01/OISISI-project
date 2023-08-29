using ConsoleApp1.Manager.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class NepolozeniPredmeti : Serializable
    {
        public string indeks;
        public string sifraPredmeta;


        public NepolozeniPredmeti() { }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                indeks,
                sifraPredmeta
            };

            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            indeks = values[0];
            sifraPredmeta = values[1];
        }
    }
}
