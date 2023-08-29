using System;
using System.Collections.Generic;
using System.Windows;
using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Controller;
using StudentskaSluzbaGUI.Model.DAO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace StudentskaSluzbaGUI.View
{
    /// <summary>
    /// Interaction logic for Student_izmena.xaml
    /// </summary>
    public partial class Student_izmena : Window, INotifyPropertyChanged
    {
        StudentController managerStudent = new StudentController();
        public Student izabran { get; set; }
        public ObservableCollection<Predmet> Nepolozeni { get; }
        public ObservableCollection<Predmet> Polozeni { get; }

        public string espb;
        public string pocena;

        public ObservableCollection<Ocena> PolozeniOcene { get; }

        public Predmet IzabranNepolozen { get; set; }
        public static int active = 0;
        public Predmet IzabranPolozen { get; set; }


        public Student_izmena(Student izabranStudent)
        {
            InitializeComponent();
            DataContext = this;
            izabran = izabranStudent;
            active = 1;
            combobox1.SelectedIndex = izabran.TrenutnaGodinaStudija - 1;
            if (izabran.Status == Status.B)
                combobox2.SelectedIndex = 0;
            else
                combobox2.SelectedIndex = 1;
            Nepolozeni = new ObservableCollection<Predmet>();
            Polozeni= new ObservableCollection<Predmet>();
            PolozeniOcene = new ObservableCollection<Ocena>();
            espb = "none";
            pocena = "none";

            PredmetController managerPredmet = new PredmetController();
            List<Predmet> predmeti = managerPredmet.VratiSvePredmete();

            NepolozeniController managerNepolozenih = new NepolozeniController();
            List<NepolozeniPredmet> nepolozeni = managerNepolozenih.VratiSveNepolozene();

            OcenaController managerOcena = new OcenaController();
            List<Ocena> ocene = managerOcena.VratiSveOcene();




            foreach (NepolozeniPredmet np in nepolozeni)
            {

                if (np.idStudenta == izabran.BrojIndeksa)
                {

                    Predmet nepolozenPredmet = managerPredmet.VratiPredmetPoId(np.idPredmeta);
                    Nepolozeni.Add(nepolozenPredmet);
                }

            }

            foreach (Ocena o in ocene)
            {

                if (o.studentKojiJePolozio == izabran.BrojIndeksa)
                {

                    Predmet predmet = managerPredmet.VratiPredmetPoId(o.predmet);
                    o.brojESPB = predmet.BrojESPB;
                    o.NazivPredmeta = predmet.NazivPredmeta;
                    o.sifraPredmeta = predmet.SifraPredmeta;
                    PolozeniOcene.Add(o);
                    Polozeni.Add(predmet);
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
            managerStudent.UkloniStudenta(izabran.BrojIndeksa);
            if (item11.IsSelected == true)
                izabran.TrenutnaGodinaStudija = 1;
            if (item12.IsSelected == true)
                izabran.TrenutnaGodinaStudija = 2;
            if (item13.IsSelected == true)
                izabran.TrenutnaGodinaStudija = 3;
            if (item14.IsSelected == true)
                izabran.TrenutnaGodinaStudija = 4;
            if (item21.IsSelected == true)
                izabran.Status = Status.B;
            if (item22.IsSelected == true)
                izabran.Status = Status.S;
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
            izabran.Mail = text6.Text;
            izabran.GodinaUpisa = Convert.ToInt32(text8.Text);
            if (provera)
            {
                managerStudent.DodajStudenta(izabran);
                this.Close();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Dodavanje_predmeta dpd = new Dodavanje_predmeta(izabran.TrenutnaGodinaStudija);
            dpd.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Unos_ocene uod = new Unos_ocene();
            uod.Show();
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
