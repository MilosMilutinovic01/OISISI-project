using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Controller;
using System.Windows.Input;
using System.Text.RegularExpressions;
using ConsoleApp1.Model;
using Ocena = StudentskaSluzbaGUI.Model.Ocena;
using Profesor = StudentskaSluzbaGUI.Model.Profesor;
using Predmet = StudentskaSluzbaGUI.Model.Predmet;
using Katedra = StudentskaSluzbaGUI.Model.Katedra;
using Student = StudentskaSluzbaGUI.Model.Student;

namespace StudentskaSluzbaGUI.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string sep = System.IO.Path.DirectorySeparatorChar.ToString();
        public static App app;
        public const string SRB = "sr-Latn-RS";
        public const string ENG = "en-US";
        public string StatusBarString;
        public static string lang;

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Glavni.Width = Prozor.Width * 0.8;
            Glavni.Height = Prozor.Height * 0.7;
            DataGridStudenti.Width = Glavni.Width * 0.8;
            DataGridStudenti.Height = Glavni.Height * 0.7;
            DataGridProfesori.Width = Glavni.Width * 0.8;
            DataGridProfesori.Height = Glavni.Height * 0.7;
            DataGridPredmeti.Width = Glavni.Width * 0.8;
            DataGridPredmeti.Height = Glavni.Height * 0.7;
            DataGridKatedre.Width = Glavni.Width * 0.8;
            DataGridKatedre.Height = Glavni.Height * 0.7;
            red11.Width = DataGridStudenti.Width * 1 / 6;
            red12.Width = DataGridStudenti.Width * 1 / 6;
            red13.Width = DataGridStudenti.Width * 1 / 6;
            red14.Width = DataGridStudenti.Width * 1 / 6;
            red15.Width = DataGridStudenti.Width * 1 / 6;
            red16.Width = DataGridStudenti.Width * 1 / 6;
            red21.Width = DataGridProfesori.Width * 1 / 4;
            red22.Width = DataGridProfesori.Width * 1 / 4;
            red23.Width = DataGridProfesori.Width * 1 / 4;
            red24.Width = DataGridProfesori.Width * 1 / 4;
            red31.Width = DataGridPredmeti.Width * 1 / 5;
            red32.Width = DataGridPredmeti.Width * 1 / 5;
            red33.Width = DataGridPredmeti.Width * 1 / 5;
            red34.Width = DataGridPredmeti.Width * 1 / 5;
            red35.Width = DataGridPredmeti.Width * 1 / 5;
            red41.Width = DataGridKatedre.Width * 1 / 3;
            red42.Width = DataGridKatedre.Width * 1 / 3;
            red43.Width = DataGridKatedre.Width * 1 / 3;
            DispatcherTimer timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, (object s, EventArgs ev) =>
            {
                this.myDateTime.Text = DateTime.Now.ToString("hh:mm dd/MM/yyyy");
            }, this.Dispatcher);
            timer.Start();
            if (Tab_student.IsSelected == true)
                Sluzba.Text = "Studentska služba - studenti";
            else if (Tab_profesor.IsSelected == true)
                Sluzba.Text = "Studentska služba - profesori";
            else if (Tab_predmet.IsSelected == true)
                Sluzba.Text = "Studentska služba - predmeti";
        }

        public ObservableCollection<Student> Studenti { get; }
        public ObservableCollection<Profesor> Profesori { get; }
        public ObservableCollection<Predmet> Predmeti { get; }
        public ObservableCollection<Katedra> Katedre { get; }
        public ObservableCollection<Ocena> Ocene { get; }
        public Student IzabranStudent { get; set; }
        public Profesor IzabranProfesor { get; set; }
        public Predmet IzabranPredmet { get; set; }
        public Katedra IzabranaKatedra { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth * 0.75;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight * 0.75;
            app = (App)Application.Current;
            app.ChangeLanguage(SRB);
            lang = SRB;
            Studenti = new ObservableCollection<Student>();
            Profesori = new ObservableCollection<Profesor>();
            Predmeti = new ObservableCollection<Predmet>();
            Katedre = new ObservableCollection<Katedra>();
            Ocene = new ObservableCollection<Ocena>();
            StudentController managerStudent = new StudentController();
            PorfesorController managerProfesor = new PorfesorController();
            PredmetController managerPredmet = new PredmetController();
            KatedraController managerKatedra = new KatedraController();
            OcenaController managerOcene = new OcenaController();
            List<Student> studenti = managerStudent.VratiSveStudente();
            List<Profesor> profesori = managerProfesor.VratiSveProfesore();
            List<Predmet> predmeti = managerPredmet.VratiSvePredmete();
            List<Katedra> katedre = managerKatedra.VratiSveKatedre();
            List<Ocena> ocene = managerOcene.VratiSveOcene();
            foreach (Student s in studenti) { 
            int brojac = 0;
            double prosek = 0;
                foreach (Ocena o in ocene)
                {
                    if (o.studentKojiJePolozio == s.BrojIndeksa)
                    {
                        prosek+=o.ocenaIspita;
                        brojac++;

                    }
                

                }
                if (brojac != 0)
                {
                    prosek = prosek / brojac;
                }
                s.ProsecnaOcena = prosek;
                Studenti.Add(s);
            }
                    
            foreach (Profesor p in profesori)
                Profesori.Add(p);
            foreach (Predmet pr in predmeti)
                Predmeti.Add(pr);
            foreach (Katedra k in katedre)
                Katedre.Add(k);
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (Tab_student.IsSelected == true)
                DodajStudenta();
            else if (Tab_profesor.IsSelected == true)
                DodajProfesora();
            else if (Tab_predmet.IsSelected == true)
                DodajPredmet();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (Tab_student.IsSelected == true)
                IzmeniStudenta();
            else if (Tab_profesor.IsSelected == true)
                IzmeniProfesora();
            else if (Tab_predmet.IsSelected == true)
                IzmeniPredmet();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Glavni.Width = Prozor.Width * 0.8;
            Glavni.Height = Prozor.Height * 0.7;
            DataGridStudenti.Width = Glavni.Width * 0.8;
            DataGridStudenti.Height = Glavni.Height * 0.7;
            DataGridProfesori.Width = Glavni.Width * 0.8;
            DataGridProfesori.Height = Glavni.Height * 0.7;
            DataGridPredmeti.Width = Glavni.Width * 0.8;
            DataGridPredmeti.Height = Glavni.Height * 0.7;
            DataGridKatedre.Width = Glavni.Width * 0.8;
            DataGridKatedre.Height = Glavni.Height * 0.7;
            red11.Width = DataGridStudenti.Width * 1 / 6;
            red12.Width = DataGridStudenti.Width * 1 / 6;
            red13.Width = DataGridStudenti.Width * 1 / 6;
            red14.Width = DataGridStudenti.Width * 1 / 6;
            red15.Width = DataGridStudenti.Width * 1 / 6;
            red16.Width = DataGridStudenti.Width * 1 / 6;
            red21.Width = DataGridProfesori.Width * 1 / 4;
            red22.Width = DataGridProfesori.Width * 1 / 4;
            red23.Width = DataGridProfesori.Width * 1 / 4;
            red24.Width = DataGridProfesori.Width * 1 / 4;
            red31.Width = DataGridPredmeti.Width * 1 / 5;
            red32.Width = DataGridPredmeti.Width * 1 / 5;
            red33.Width = DataGridPredmeti.Width * 1 / 5;
            red34.Width = DataGridPredmeti.Width * 1 / 5;
            red35.Width = DataGridPredmeti.Width * 1 / 5;
            red41.Width = DataGridKatedre.Width * 1 / 3;
            red42.Width = DataGridKatedre.Width * 1 / 3;
            red43.Width = DataGridKatedre.Width * 1 / 3;
        }

        private void Glavni_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lang == SRB)
            {
                if (Tab_student.IsSelected == true)
                {
                    Sluzba.Text = "Studentska služba - studenti";
                    DodavanjeSefa.Visibility = Visibility.Hidden;
                    Add.IsEnabled = true;
                    Update.IsEnabled = true;
                    Delete.IsEnabled = true;
                }
                else if (Tab_profesor.IsSelected == true)
                {
                    Sluzba.Text = "Studentska služba - profesori";
                    DodavanjeSefa.Visibility = Visibility.Hidden;
                    Add.IsEnabled = true;
                    Update.IsEnabled = true;
                    Delete.IsEnabled = true;
                }
                else if (Tab_predmet.IsSelected == true)
                {
                    Sluzba.Text = "Studentska služba - predmeti";
                    DodavanjeSefa.Visibility = Visibility.Hidden;
                    Add.IsEnabled = true;
                    Update.IsEnabled = true;
                    Delete.IsEnabled = true;
                }
                else if (Tab_katedra.IsSelected == true)
                {
                    Sluzba.Text = "Studentska služba - katedre";
                    DodavanjeSefa.Visibility = Visibility.Visible;
                    Add.IsEnabled = false;
                    Update.IsEnabled = false;
                    Delete.IsEnabled = false;
                }
            }
            else if (lang == ENG)
            {
                if (Tab_student.IsSelected == true)
                {
                    Sluzba.Text = "Student service - students";
                    DodavanjeSefa.Visibility = Visibility.Hidden;
                    Add.IsEnabled = true;
                    Update.IsEnabled = true;
                    Delete.IsEnabled = true;
                }
                else if (Tab_profesor.IsSelected == true)
                {
                    Sluzba.Text = "Student service - professors";
                    DodavanjeSefa.Visibility = Visibility.Hidden;
                    Add.IsEnabled = true;
                    Update.IsEnabled = true;
                    Delete.IsEnabled = true;
                }
                else if (Tab_predmet.IsSelected == true)
                {
                    Sluzba.Text = "Student service - subjects";
                    DodavanjeSefa.Visibility = Visibility.Hidden;
                    Add.IsEnabled = true;
                    Update.IsEnabled = true;
                    Delete.IsEnabled = true;
                }
                else if (Tab_katedra.IsSelected == true)
                {
                    Sluzba.Text = "Student service - departments";
                    DodavanjeSefa.Visibility = Visibility.Visible;
                    Add.IsEnabled = false;
                    Update.IsEnabled = false;
                    Delete.IsEnabled = false;
                }
            }
        }

        private void MenuItem_Click_Edit(object sender, RoutedEventArgs e)
        {
            if (Tab_student.IsSelected == true)
                IzmeniStudenta();
            else if (Tab_profesor.IsSelected == true)
                IzmeniProfesora();
            else if (Tab_predmet.IsSelected == true)
                IzmeniPredmet();
        }

        private void MenuItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (Tab_student.IsSelected == true)
                ObrisiStudenta();
            else if (Tab_profesor.IsSelected == true)
                ObrisiProfesora();
            else if (Tab_predmet.IsSelected == true)
                ObrisiPredmet();
        }

        private void MenuItem_Click_New(object sender, RoutedEventArgs e)
        {
            if (Tab_student.IsSelected == true)
                DodajStudenta();
            else if (Tab_profesor.IsSelected == true)
                DodajProfesora();
            else if (Tab_predmet.IsSelected == true)
                DodajPredmet();
        }

        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_Open(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_Close(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Autori:\n\tMatija Maksimović\n\tMiloš Milutinović\n\nZa sva pitanja i dodatne informacije javiti se na:\n milutinovic.ra129.2020@uns.ac.rs ili maksimovic.ra132.2020.@uns.ac.rs", "O aplikaciji");
        }

        /*private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Tab_student.IsSelected == true)
                DodajStudenta();
            else if (Tab_profesor.IsSelected == true)
                DodajProfesora();
            else if (Tab_predmet.IsSelected == true)
                DodajPredmet();
        }*/

        private void Prozor_Activated_1(object sender, EventArgs e)
        {
            Studenti.Clear();
            Profesori.Clear();
            Predmeti.Clear();
            Katedre.Clear();
            InitializeComponent();
            this.DataContext = this;
            StudentController managerStudent = new StudentController();
            PorfesorController managerProfesor = new PorfesorController();
            PredmetController managerPredmet = new PredmetController();
            KatedraController managerKatedra = new KatedraController();
            OcenaController managerOcene = new OcenaController();
            List<Student> studenti = managerStudent.VratiSveStudente();
            List<Profesor> profesori = managerProfesor.VratiSveProfesore();
            List<Predmet> predmeti = managerPredmet.VratiSvePredmete();
            List<Katedra> katedre = managerKatedra.VratiSveKatedre();
            List<Ocena> ocene = managerOcene.VratiSveOcene();

            foreach (Student s in studenti)
            {
                int brojac = 0;
                double prosek = 0;
                foreach (Ocena o in ocene)
                {
                    if (o.studentKojiJePolozio == s.BrojIndeksa)
                    {
                        prosek += o.ocenaIspita;
                        brojac++;

                    }


                }
                if (brojac != 0)
                {
                    prosek = prosek / brojac;
                }
                s.ProsecnaOcena = prosek;
                Studenti.Add(s);
            }


            foreach (Profesor p in profesori)
                Profesori.Add(p);
            foreach (Predmet pr in predmeti)
                Predmeti.Add(pr);
            foreach (Katedra k in katedre)
                Katedre.Add(k);
        }

        private void Prozor_KeyDown(object sender, KeyEventArgs e)
        {
            if (Tab_student.IsSelected == true)
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.N))
                    DodajStudenta();
                else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D))
                    ObrisiStudenta();
                else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E))
                    IzmeniStudenta();
            }
            else if (Tab_profesor.IsSelected == true)
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.N))
                    DodajProfesora();
                else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D))
                    ObrisiProfesora();
                else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E))
                    IzmeniProfesora();
            }
            else if (Tab_predmet.IsSelected == true)
            {
                if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.N))
                    DodajPredmet();
                else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D))
                    ObrisiPredmet();
                else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E))
                    IzmeniPredmet();
            }
        }

        public void DodajStudenta()
        {
            Student_dodavanje dialogs = new Student_dodavanje();
            dialogs.Show();
        }

        public void ObrisiStudenta()
        {
            if (IzabranStudent == null)
            {
                if (lang == SRB)
                    MessageBox.Show("Morate izabrati studenta!");
                else if (lang == ENG)
                    MessageBox.Show("You should select student!");
            }
            else
            {
                StudentController managerStudent = new StudentController();
                List<Student> studenti = managerStudent.VratiSveStudente();
                foreach (Student s in studenti)
                    if (s.BrojIndeksa == IzabranStudent.BrojIndeksa)
                    {
                        managerStudent.UkloniStudenta(IzabranStudent.BrojIndeksa);
                        break;
                    }
                Studenti.Remove(IzabranStudent);
            }
        }

        public void IzmeniStudenta()
        {
            if (IzabranStudent == null)
            {
                if (lang == SRB)
                    MessageBox.Show("Morate izabrati studenta!");
                else if (lang == ENG)
                    MessageBox.Show("You should select student!");
            }
            else
            {
                Student_izmena sid_dialog = new Student_izmena(IzabranStudent);
                sid_dialog.Show();
            }
        }

        public void DodajProfesora()
        {
            Profesor_dodavanje dialogp = new Profesor_dodavanje();
            dialogp.Show();
        }

        public void ObrisiProfesora()
        {
            if (IzabranProfesor == null)
            {
                if (lang == SRB)
                    MessageBox.Show("Morate izabrati profesora!");
                else if (lang == ENG)
                    MessageBox.Show("You should select professor!");
            }
            else
            {
                KatedraController managerKatedra = new KatedraController();
                List<Katedra> katedre = managerKatedra.VratiSveKatedre();
                int i;
                foreach (Katedra k in katedre)
                {
                    if (k.SefKatedre == IzabranProfesor.Id)
                    {
                        k.SefKatedre = -1;
                    }
                    for (i = 0; i < k.spisakProfesora.Count; i++)
                        if (k.spisakProfesora[i].Id == IzabranProfesor.Id)
                            break;
                    k.spisakProfesora.RemoveAt(i);
                }

                PorfesorController managerProfesor = new PorfesorController();
                List<Profesor> profesori = managerProfesor.VratiSveProfesore();
                foreach (Profesor p in profesori)
                    if (p.Id == IzabranProfesor.Id)
                    {
                        managerProfesor.UkloniSProfesora(IzabranProfesor.Id);
                        break;
                    }
                managerKatedra.Save(katedre);
                Profesori.Remove(IzabranProfesor);

                Katedre.Clear();
                katedre = managerKatedra.VratiSveKatedre();
                foreach (Katedra k in katedre)
                    Katedre.Add(k);
            }
        }

        public void IzmeniProfesora()
        {
            if (IzabranProfesor == null)
            {
                if (lang == SRB)
                    MessageBox.Show("Morate izabrati profesora!");
                else if (lang == ENG)
                    MessageBox.Show("You should select professor!");
            }
            else
            {
                Profesor_izmena pid_dialog = new Profesor_izmena(IzabranProfesor);
                pid_dialog.Show();
            }
        }

        public void DodajPredmet()
        {
            Predmet_dodavanje dialogp = new Predmet_dodavanje();
            dialogp.Show();
        }

        public void ObrisiPredmet()
        {
            if (IzabranPredmet == null)
            {
                if (lang == SRB)
                    MessageBox.Show("Morate izabrati predmet!");
                else if (lang == ENG)
                    MessageBox.Show("You should select subject!");
            }
            else
            {
                PredmetController managerPredmet = new PredmetController();
                List<Predmet> predmeti = managerPredmet.VratiSvePredmete();
                foreach (Predmet p in predmeti)
                    if (p.SifraPredmeta == IzabranPredmet.SifraPredmeta)
                    {
                        managerPredmet.UkloniPredmet(IzabranPredmet.SifraPredmeta);
                        break;
                    }
                Predmeti.Remove(IzabranPredmet);
            }
        }

        public void IzmeniPredmet()
        {
            if (IzabranPredmet == null)
            {
                if (lang == SRB)
                    MessageBox.Show("Morate izabrati predmet!");
                else if (lang == ENG)
                    MessageBox.Show("You should select subject!");
            }
            else
            {
                Predmet_izmena pid_dialog = new Predmet_izmena(IzabranPredmet);
                pid_dialog.Show();
            }
        }

        /*private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Tab_student.IsSelected == true)
                ObrisiStudenta();
            else if (Tab_profesor.IsSelected == true)
                ObrisiProfesora();
            else if (Tab_predmet.IsSelected == true)
                ObrisiPredmet();
        }*/

        private void MenuItem_Click_Serbian(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(SRB);
            lang = SRB;
        }

        private void MenuItem_Click_English(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(ENG);
            lang = ENG;
        }

        private void Prozor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                this.Close();
            }
            else if (e.Key == Key.A && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                MessageBox.Show("Autori:\n\tMatija Maksimović\n\tMiloš Milutinović\n\nZa sva pitanja i dodatne informacije javiti se na:\n milutinovic.ra129.2020@uns.ac.rs ili maksimovic.ra132.2020.@uns.ac.rs", "O aplikaciji");
            else if (e.SystemKey == Key.E && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                app.ChangeLanguage(ENG);
            }
            else if (e.SystemKey == Key.S && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                app.ChangeLanguage(SRB);
            }
        }

        private void Prozor_LayoutUpdated(object sender, EventArgs e)
        {
            if (lang == SRB)
            {
                if (Tab_student.IsSelected == true)
                    Sluzba.Text = "Studentska služba - studenti";
                else if (Tab_profesor.IsSelected == true)
                    Sluzba.Text = "Studentska služba - profesori";
                else if (Tab_predmet.IsSelected == true)
                    Sluzba.Text = "Studentska služba - predmeti";
                else if (Tab_katedra.IsSelected == true)
                    Sluzba.Text = "Studentska služba - katedre";
            }
            else if (lang == ENG)
            {
                if (Tab_student.IsSelected == true)
                    Sluzba.Text = "Student service - students";
                else if (Tab_profesor.IsSelected == true)
                    Sluzba.Text = "Student service - professors";
                else if (Tab_predmet.IsSelected == true)
                    Sluzba.Text = "Student service - subjects";
                else if (Tab_katedra.IsSelected == true)
                    Sluzba.Text = "Student service - departments";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Studenti.Clear();
            StudentController managerStudent = new StudentController();
            List<Student> studenti = managerStudent.VratiSveStudente();
            Profesori.Clear();
            PorfesorController managerProfesor = new PorfesorController();
            List<Profesor> profesori = managerProfesor.VratiSveProfesore();
            Predmeti.Clear();
            PredmetController managerPredmet = new PredmetController();
            List<Predmet> predmeti = managerPredmet.VratiSvePredmete();
            Katedre.Clear();
            KatedraController managerKatedra = new KatedraController();
            List<Katedra> katedre = managerKatedra.VratiSveKatedre();
            string[] delovi = Search.Text.Split(',');
            if(Tab_student.IsSelected == true)
            {
                if (delovi.Length == 1)
                {
                    foreach (Student s in studenti)
                    {
                        if (s.Prezime.ToLower().Contains(delovi[0].ToLower().TrimStart().TrimEnd()))
                            Studenti.Add(s);
                    }
                }
                else if (delovi.Length == 2)
                {
                    foreach (Student s in studenti)
                    {
                        if (s.Prezime.ToLower().Contains(delovi[0].ToLower().TrimStart().TrimEnd()) && s.Ime.ToLower().Contains(delovi[1].ToLower().TrimStart().TrimEnd()))
                            Studenti.Add(s);
                    }
                }
                else if (delovi.Length == 3)
                {
                    foreach (Student s in studenti)
                    {
                        if (s.BrojIndeksa.ToLower().Contains(delovi[0].ToLower().TrimStart().TrimEnd()) && s.Prezime.ToLower().Contains(delovi[2].ToLower().TrimStart().TrimEnd()) && s.Ime.ToLower().Contains(delovi[1].ToLower().TrimStart().TrimEnd()))
                            Studenti.Add(s);
                    }
                }
            }
            if (Tab_profesor.IsSelected == true)
            {
                if (delovi.Length == 1)
                {
                    foreach (Profesor p in profesori)
                    {
                        if (p.Prezime.ToLower().Contains(delovi[0].ToLower().TrimStart().TrimEnd()))
                            Profesori.Add(p);
                    }
                }
                else if (delovi.Length == 2)
                {
                    foreach (Profesor p in profesori)
                    {
                        if (p.Prezime.ToLower().Contains(delovi[0].ToLower().TrimStart().TrimEnd()) && p.Ime.ToLower().Contains(delovi[1].ToLower().TrimStart().TrimEnd()))
                            Profesori.Add(p);
                    }
                }
            }
            if (Tab_predmet.IsSelected == true)
            {
                if (delovi.Length == 1)
                {
                    delovi[0] = Regex.Replace(delovi[0], @"\s+", " ").TrimStart().TrimEnd();
                    string[] ugnezdjeniDeloviUnos = delovi[0].Split(' ');
                    foreach (Predmet p in predmeti)
                    {
                        string[] ugnezdjeniDeloviFajl = p.NazivPredmeta.Split(' ');
                        if (ugnezdjeniDeloviFajl[0].ToLower().Contains(ugnezdjeniDeloviUnos[0].ToLower()) && ugnezdjeniDeloviFajl[1].ToLower().Contains(ugnezdjeniDeloviUnos[1].ToLower()))
                            Predmeti.Add(p);
                    }
                }
                else if (delovi.Length == 2)
                {
                    foreach (Predmet p in predmeti)
                    {
                        if (p.SifraPredmeta.ToLower().Contains(delovi[0].ToLower().TrimStart().TrimEnd()) && p.NazivPredmeta.ToLower().Contains(delovi[1].ToLower().TrimStart().TrimEnd()))
                            Predmeti.Add(p);
                    }
                }
            }
            if (Tab_katedra.IsSelected == true)
            {
                if (delovi.Length == 1)
                {
                    delovi[0] = Regex.Replace(delovi[0], @"\s+", " ").TrimStart().TrimEnd();
                    //string[] ugnezdjeniDeloviUnos = delovi[0].Split(' ');
                    foreach (Katedra k in katedre)
                    {
                        //string[] ugnezdjeniDeloviFajl = k.NazivKatedre.Split(' ');
                        if (k.NazivKatedre.ToLower().Contains(delovi[0]))
                            Katedre.Add(k);
                    }
                }
                else if (delovi.Length == 2)
                {
                    foreach (Katedra k in katedre)
                    {
                        if (k.SifraKatedre.ToLower().Contains(delovi[0].ToLower().TrimStart().TrimEnd()) && k.NazivKatedre.ToLower().Contains(delovi[1].ToLower().TrimStart().TrimEnd()))
                            Katedre.Add(k);
                    }
                }
            }
        }

        private void DodavanjeSefa_Click(object sender, RoutedEventArgs e)
        {
            if (IzabranaKatedra == null)
            {
                if (lang == SRB)
                    MessageBox.Show("Morate izabrati katedru!");
                else if (lang == ENG)
                    MessageBox.Show("You should select department!");
            }
            else
            {
                DodavanjeSefaKatedre dsk = new DodavanjeSefaKatedre(IzabranaKatedra);
                dsk.Show();
            }
        }

        private void Add_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Tab_student.IsSelected == true)
                DodajStudenta();
            else if (Tab_profesor.IsSelected == true)
                DodajProfesora();
            else if (Tab_predmet.IsSelected == true)
                DodajPredmet();
        }

        private void Update_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Tab_student.IsSelected == true)
                IzmeniStudenta();
            else if (Tab_profesor.IsSelected == true)
                IzmeniProfesora();
            else if (Tab_predmet.IsSelected == true)
                IzmeniPredmet();
        }

        private void Delete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Tab_student.IsSelected == true)
                ObrisiStudenta();
            else if (Tab_profesor.IsSelected == true)
                ObrisiProfesora();
            else if (Tab_predmet.IsSelected == true)
                ObrisiPredmet();
        }
    }
}

