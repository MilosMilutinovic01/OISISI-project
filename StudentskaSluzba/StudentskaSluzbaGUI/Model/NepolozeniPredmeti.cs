using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using StudentskaSluzbaGUI.Serializer;

namespace StudentskaSluzbaGUI.Model
{
    public class NepolozeniPredmet : ISerializable
    {
        public string idStudenta { get; set; }

        public string idPredmeta { get; set; } 


        public NepolozeniPredmet (string ids, string idp)
        {
            this.idStudenta = ids;
            this.idPredmeta = idp;
        }

        public NepolozeniPredmet() { }




        public string[] ToCSV()
        {
            string[] csvValues =
            {
                idStudenta.ToString(),
                idPredmeta.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            idStudenta = values[0];
            idPredmeta = values[1];

        }
    }
}
