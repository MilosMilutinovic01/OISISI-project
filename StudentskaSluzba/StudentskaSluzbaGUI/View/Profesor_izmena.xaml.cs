using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StudentskaSluzbaGUI.Controller;
using StudentskaSluzbaGUI.Model;

namespace StudentskaSluzbaGUI.View
{
    /// <summary>
    /// Interaction logic for Profesor_Izmena.xaml
    /// </summary>
    public partial class Profesor_izmena : Window, INotifyPropertyChanged
    {
        PorfesorController managerProfesor = new PorfesorController();
        public Profesor izabran { get; set; }
        public static int active = 0;
        public ObservableCollection<Predmet> Predaje { get; }
        public Predmet IzabranPredmet { get; set; }
        public Profesor_izmena(Profesor izabranProfesor)
        {
            InitializeComponent();
            DataContext = this;
            active = 1;
            izabran = izabranProfesor;
            Predaje= new ObservableCollection<Predmet>();

            PredmetController managerPredmet = new PredmetController();
            List<Predmet> predmeti = managerPredmet.VratiSvePredmete();


            foreach (Predmet p in predmeti)
            {

                if (p.ProfesorId == izabran.Id)
                {

                    
                   Predaje.Add(p);
                }

            }







        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            active = 0;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            izabran.Ime = text1.Text;
            izabran.Prezime = text2.Text;
            managerProfesor.UkloniSProfesora(izabran.Id);
            bool provera = true;
            try
            {
                izabran.DatumRodjenja = System.Convert.ToDateTime(text3.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                provera = false;
            }
            string[] deloviAdrese = text4.Text.Split(',');
            string ulica = deloviAdrese[0];
            int adresniBroj = System.Convert.ToInt32(deloviAdrese[1]);
            string grad = deloviAdrese[2];
            string drzava = deloviAdrese[3];
            izabran.AdresaStanovanja = new Adresa(ulica, adresniBroj, grad, drzava);
            izabran.KontaktTelefon = text5.Text;
            izabran.EmailAdresa = text6.Text;
            deloviAdrese = text7.Text.Split(',');
            ulica = deloviAdrese[0];
            adresniBroj = System.Convert.ToInt32(deloviAdrese[1]);
            grad = deloviAdrese[2];
            drzava = deloviAdrese[3];
            izabran.AdresaKancelarije = new Adresa(ulica, adresniBroj, grad, drzava);
            izabran.BrojLicneKarte = text8.Text;
            izabran.Zvanje = text9.Text;
            izabran.GodineStaza = Convert.ToInt32(text10.Text);
            if (provera)
            {
                managerProfesor.DodajProfesora(izabran);
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
    }
}