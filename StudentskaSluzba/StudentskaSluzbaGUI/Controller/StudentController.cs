using System;
using System.Collections.Generic;
using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Model.DAO;
using StudentskaSluzbaGUI.Observer;
using StudentskaSluzbaGUI.Serializer;
using StudentskaSluzbaGUI.Storage;

namespace StudentskaSluzbaGUI.Controller
{
    public class StudentController
    {
        //private StudentDAO _students;

        /*public List<Student> GetAllStudents()
        {
            return _students.GetAll();
        }

        public void Create(Student student)
        {
            _students.Add(student);
        }

        public void Delete(Student student)
        {
            _students.Remove(student);
        }

        public void Subscribe(IObserver observer)
        {
            _students.Subscribe(observer);
        }*/

        private List<Student> studenti;
        //private Serializer<Student> serializer;
        private StudentStorage ss;
        
        //private readonly string fileName = "../../Data/studenti.txt";

        public StudentController()
        {
            //_students = new StudentDAO();
            /*serializer = new Serializer<Student>();
            UcitajStudente();*/
            ss = new StudentStorage();
            studenti = ss.Ucitaj();
        }

        /*public void UcitajStudente()
        {
            studenti = serializer.FromCSV(fileName);
        }

        public void SacuvajStudente()
        {
            serializer.ToCSV(fileName, studenti);
        }*/

        /*private int GenerisiId()
        {
            if (studenti.Count == 0) return 0;
            return Convert.ToInt32(studenti[studenti.Count - 1].brojIndeksa) + 1;
        }*/

        public Student DodajStudenta(Student student)
        {
            studenti.Add(student);
            //SacuvajStudente();
            ss.Sacuvaj(studenti);
            return student;
        }
        

        public Student UkloniStudenta(string brojIndeksa)
        {
            Student student = VratiStudentaPoId(brojIndeksa);
            if (student == null) return null;

            studenti.Remove(student);
            ss.Sacuvaj(studenti);
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
    }
}
