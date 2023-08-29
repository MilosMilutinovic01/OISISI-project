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

namespace StudentskaSluzbaGUI.View
{
    /// <summary>
    /// Interaction logic for Predmet_dodavanje.xaml
    /// </summary>
    public partial class Predmet_dodavanje : Window, INotifyPropertyChanged
    {
        private PredmetController _controller;

        public Predmet Predmet { get; set; }

        public Predmet_dodavanje()
        {
            InitializeComponent();
            DataContext = this;
            Predmet = new Predmet();

            _controller = new PredmetController();
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
            if (Item1.IsSelected == true)
                Predmet.Semestar = Semestar.L;
            else if (Item2.IsSelected == true)
                Predmet.Semestar = Semestar.Z;
            PredmetController controlerPredmet = new PredmetController();
            List<Predmet> predmeti = controlerPredmet.VratiSvePredmete();
            bool provera = true;
            foreach (Predmet p in predmeti)
                if (p.SifraPredmeta == Predmet.SifraPredmeta)
                {
                    MessageBox.Show("Morate uneti sifru koja ne postoji!");
                    provera = false;
                    break;
                }
                else
                    provera = true;

            if (provera)
            {
                _controller.DodajPredmet(Predmet);
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
            if (Predmet.IsValid)
                DugmePotvrdi.IsEnabled = true;
            else
                DugmePotvrdi.IsEnabled = false;
        }
    }
}
