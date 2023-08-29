using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using StudentskaSluzbaGUI.Controller;
using StudentskaSluzbaGUI.Model;
using ConsoleApp1.Manager;
using System.Collections.Generic;
using StudentskaSluzbaGUI.Serializer;
using System.Windows.Input;
using System;

namespace StudentskaSluzbaGUI.View
{
    /// <summary>
    /// Interaction logic for Student_dodavanje.xaml
    /// </summary>
    public partial class Student_dodavanje : Window, INotifyPropertyChanged
    {
        private StudentController _controller;
        private AdresaController kontrolerAdresa;

        public Student Student { get; set; }

        public Student_dodavanje()
        {
            InitializeComponent();
            DataContext = this;
            Student = new Student();

            _controller = new StudentController();
            kontrolerAdresa = new AdresaController();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Item11.IsSelected == true)
                Student.TrenutnaGodinaStudija = 1;
            if (Item12.IsSelected == true)
                Student.TrenutnaGodinaStudija = 2;
            if (Item13.IsSelected == true)
                Student.TrenutnaGodinaStudija = 3;
            if (Item14.IsSelected == true)
                Student.TrenutnaGodinaStudija = 4;
            if (Item21.IsSelected == true)
                Student.Status = Status.B;
            if (Item22.IsSelected == true)
                Student.Status = Status.S;
            bool provera = false;
            string[] deloviAdrese = Student.Adr.Split(',');
            string ulica = deloviAdrese[0];
            int adresniBroj = System.Convert.ToInt32(deloviAdrese[1]);
            string grad = deloviAdrese[2];
            string drzava = deloviAdrese[3];
            Student.AdresaStanovanja = new Adresa(ulica, adresniBroj, grad, drzava);
            StudentController controlerStudent = new StudentController();
            List<Student> studenti = controlerStudent.VratiSveStudente();
            foreach (Student s in studenti)
                if (s.BrojIndeksa == Student.BrojIndeksa)
                {
                    MessageBox.Show("Morate uneti indeks koji ne postoji!");
                    provera = false;
                    break;
                }
                else
                    provera = true;
            try
            {
                Student.DatumRodjenja = System.Convert.ToDateTime(Student.Dat);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                provera = false;
            }

            if (provera)
            {
                _controller.DodajStudenta(Student);
                kontrolerAdresa.Create(Student.AdresaStanovanja);
                this.Close();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                this.Close();
            }
            else if (e.Key == Key.A && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                MessageBox.Show("Autori:\n\tMatija Maksimović\n\tMiloš Milutinović\n\nZa sva pitanja i dodatne informacije javiti se na:\n milutinovic.ra129.2020@uns.ac.rs ili maksimovic.ra132.2020.@uns.ac.rs", "O aplikaciji");
            else if (e.SystemKey == Key.E && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                MainWindow.app.ChangeLanguage(MainWindow.ENG);
            }
            else if (e.SystemKey == Key.S && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                MainWindow.app.ChangeLanguage(MainWindow.SRB);
            }
        }

        private void Window_LayoutUpdated(object sender, System.EventArgs e)
        {
            if (Student.IsValid)
                DugmePotvrde.IsEnabled = true;
            else
                DugmePotvrde.IsEnabled = false;
        }
    }
}
