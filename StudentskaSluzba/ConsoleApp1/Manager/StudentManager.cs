using ConsoleApp1.Manager.Serialization;
using System.Collections.Generic;
using System;
using ConsoleApp1.Model;

namespace ConsoleApp1.Manager
{
    public class StudentManager
    {
        private List<Student> studenti;
        private Serializer<Student> serializer;

        private readonly string fileName = "studenti.txt";

        public StudentManager()
        {
            serializer = new Serializer<Student>();
            UcitajStudente();
        }
        
        public void UcitajStudente()
        {
            studenti = serializer.FromCSV(fileName);
        }

        public void SacuvajStudente()
        {
            serializer.ToCSV(fileName, studenti);
        }

        /*private int GenerisiId()
        {
            if (studenti.Count == 0) return 0;
            return Convert.ToInt32(studenti[studenti.Count - 1].brojIndeksa) + 1;
        }*/

        public Student DodajStudenta(Student student)
        {
            studenti.Add(student);
            SacuvajStudente();
            return student;
        }

        public Student AzurirajStudenta(Student student)
        {
            Student stariStudent = VratiStudentaPoId(student.BrojIndeksa);
            if (stariStudent == null) return null;

            stariStudent.adresaStanovanja = student.adresaStanovanja;
            stariStudent.datumRodjenja = student.datumRodjenja;
            stariStudent.GodinaUpisa = student.GodinaUpisa;
            stariStudent.Ime = student.Ime;
            stariStudent.kontaktTelefon = student.kontaktTelefon;
            stariStudent.mail = student.mail;
            stariStudent.Prezime = student.Prezime;
            stariStudent.ProsecnaOcena = student.ProsecnaOcena;
            stariStudent.spisakNepolozenih = student.spisakNepolozenih;
            stariStudent.spisakPolozenih = student.spisakPolozenih;
            stariStudent.Status = student.Status;
            stariStudent.trenutnaGodinaStudija = student.trenutnaGodinaStudija;

            SacuvajStudente();
            return stariStudent;
        }

        public Student UkloniStudenta(string brojIndeksa)
        {
            Student student = VratiStudentaPoId(brojIndeksa);
            if (student == null) return null;

            studenti.Remove(student);
            SacuvajStudente();
            return student;
        }

        public Student VratiStudentaPoId(string brojIndeksa)
        {
            return studenti.Find(s => s.BrojIndeksa == brojIndeksa);
        }

        public List<Student> VratiSveStudente()
        {
            return studenti;
        }

        public void UcitajOcene()//ovo pozvati da bi se sredio spisak polozenih i naknadno treba napraviti racunanje prosecne ocene
        {
            List<Student> studenti = new List<Student>();
            string fileName = "studenti.txt";
            Serializer<Student> serializer = new Serializer<Student>();
            studenti = serializer.FromCSV(fileName);
            StudentManager ms = new StudentManager();

            List<Ocena> ocene = new List<Ocena>();
            string fileName1 = "ocene.txt";
            Serializer<Ocena> serializer1 = new Serializer<Ocena>();
            ocene = serializer1.FromCSV(fileName1);

            List<NepolozeniPredmeti> np = new List<NepolozeniPredmeti>();
            string fileName2 = "nepolozeniPredmeti.txt";
            Serializer<NepolozeniPredmeti> serializer2 = new Serializer<NepolozeniPredmeti>();
            np = serializer2.FromCSV(fileName2);

            List<Predmet> predmeti = new List<Predmet>();
            string fileName3 = "predmeti.txt";
            Serializer<Predmet> serializer3 = new Serializer<Predmet>();
            predmeti = serializer3.FromCSV(fileName3);

            foreach(Ocena o in ocene)
            {
                foreach(NepolozeniPredmeti x in np)
                {
                    if (x.indeks.Equals(o.studentKojiJePolozio))
                    {
                        Student s = new Student();
                        s = ms.VratiStudentaPoId(x.indeks);
                        s.spisakPolozenih.Add(o);
                        np.Remove(x);
                    }
                }
            }
        }
    }
}
