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
    /// Interaction logic for Predmet_izmena.xaml
    /// </summary>
    public partial class Predmet_izmena : Window, INotifyPropertyChanged
    {
        PredmetController managerPredmet = new PredmetController();
        public Predmet izabran { get; set; }
        public static int active = 0;

        public Predmet_izmena(Predmet izabranPredmet)
        {
            InitializeComponent();
            DataContext = this;
            izabran = izabranPredmet;
            active = 1;
            /*text1.Text = izabran.SifraPredmeta;
            text2.Text = izabran.NazivPredmeta;
            text3.Text = izabran.GodinaStudija.ToString();
            text4.Text = izabran.BrojESPB.ToString();*/
            if (izabran.Semestar == Semestar.L)
                combobox1.SelectedIndex = 0;
            else
                combobox1.SelectedIndex = 1;
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
            izabran.SifraPredmeta = text1.Text;
            izabran.NazivPredmeta = text2.Text;
            managerPredmet.UkloniPredmet(izabran.SifraPredmeta);
            if (Item1.IsSelected == true)
                izabran.Semestar = Semestar.L;
            if (Item2.IsSelected == true)
                izabran.Semestar = Semestar.Z;
            izabran.GodinaStudija = Convert.ToInt32(text3.Text);
            izabran.BrojESPB = Convert.ToInt32(text4.Text);
            managerPredmet.DodajPredmet(izabran);
            this.Close();
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
