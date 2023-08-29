using ConsoleApp1.Manager.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class StudentPredmet : Serializable

    {
        public string StudentId { get; set; }

        public string PredmetId { get; set; }

        public StudentPredmet() { }

        public StudentPredmet(string studentId, string subjectId)
        {
            StudentId = studentId;
            PredmetId = subjectId;
        }

        public void FromCSV(string[] values)
        {
            StudentId = values[0];
            PredmetId = values[1];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                StudentId,
                PredmetId
            };
            return csvValues;
        }
    }
}