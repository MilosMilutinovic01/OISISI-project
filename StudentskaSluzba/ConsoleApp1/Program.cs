using ConsoleApp1.Manager;
using ConsoleApp1.Console;
using ConsoleApp1.Manager.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Model;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            MainConsoleView glavni = new MainConsoleView();

            glavni.PokreniMeni();

            List<Profesor> profesori = new List<Profesor>();
            Serializer<Profesor> profesorSerializer = new Serializer<Profesor>();
            profesori = profesorSerializer.FromCSV("profesori.txt");

            List<Student> studenti = new List<Student>();
            Serializer<Student> studentSerializer = new Serializer<Student>();
            studenti = studentSerializer.FromCSV("studenti.txt");

            List<Predmet> predmeti = new List<Predmet>();
            Serializer<Predmet> predmetSerializer = new Serializer<Predmet>();
            predmeti = predmetSerializer.FromCSV("predmeti.txt");

            List<StudentPredmet> studentPredmet = new List<StudentPredmet>();
            foreach(Predmet predmet in predmeti)
            {
                foreach(Student student in studenti)
                {
                    //if(student.)
                }
            }
            Serializer<StudentPredmet> studentPredmetSerializer = new Serializer<StudentPredmet>();
            studentPredmet = studentPredmetSerializer.FromCSV("studentPredmet.txt");

            foreach (Predmet predmet in predmeti)
            {
                Profesor profesor = profesori.Find(p => p.id == predmet.ProfesorId);
                profesor.predmeti.Add(predmet);
                predmet.Profesor = profesor;
            }

            foreach (StudentPredmet studentpredmet in studentPredmet)
            {
                Student student = studenti.Find(s => s.BrojIndeksa == studentpredmet.StudentId);
                Predmet predmet = predmeti.Find(s => s.sifraPredmeta == studentpredmet.PredmetId);
                //student.spisakNepolozenih.Add(predmet);
                predmet.Studenti.Add(student);
            }
        }
    }
}