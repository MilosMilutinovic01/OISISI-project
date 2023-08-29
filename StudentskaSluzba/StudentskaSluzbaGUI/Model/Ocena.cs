using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using StudentskaSluzbaGUI.Serializer;
using StudentskaSluzbaGUI.View;

namespace StudentskaSluzbaGUI.Model
{
    public class Ocena : ISerializable
    {
        public int id { get; set; }
        public string studentKojiJePolozio { get; set; }   //indeks studenta
        public string predmet { get; set; }    //sifra predmeta
        public int ocenaIspita { get; set; }
        public string datumPolaganjaIspita { get; set; }

        public string sifraPredmeta { get; set; }
        public string nazivPredmeta { get; set; }
        public int brojESPB { get; set; }



        public Ocena(int id, string studentKojiJePolozio, string predmet, int ocenaIspita, string datumPolaganjaIspita)
        {
            this.id = id;
            this.studentKojiJePolozio = studentKojiJePolozio;
            this.predmet = predmet;
            this.ocenaIspita = ocenaIspita;
            this.datumPolaganjaIspita = datumPolaganjaIspita;
            this.brojESPB = 0;
            this.nazivPredmeta = "";
            this.SifraPredmeta = "";
        }

        public int OcenaIspita
        {
            get => ocenaIspita;
            set
            {
                if (value != ocenaIspita)
                {
                    ocenaIspita = value;
                    
                }
            }
        }

        public string DatumPolaganjaIspita
        {
            get => datumPolaganjaIspita;
            set
            {
                if (value != datumPolaganjaIspita)
                {
                    datumPolaganjaIspita = value;
            
                }
            }
        }

        public string NazivPredmeta
        {
            get => nazivPredmeta;
            set
            {
                if (value != nazivPredmeta)
                {
                    nazivPredmeta = value;

                }
            }
        }

        public string SifraPredmeta
        {
            get => sifraPredmeta;
            set
            {
                if (value != sifraPredmeta)
                {
                    sifraPredmeta = value;

                }
            }
        }


        public int BrojESPB
        {
            get => brojESPB;
            set
            {
                if (value != brojESPB)
                {
                    brojESPB = value;

                }
            }
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
            id = Convert.ToInt32(values[0]);
            studentKojiJePolozio = values[1];
            predmet = values[2];
            ocenaIspita = int.Parse(values[3]);
            datumPolaganjaIspita = values[4];
        }





    }
}

