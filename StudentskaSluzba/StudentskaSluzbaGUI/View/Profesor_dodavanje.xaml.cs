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
    /// Interaction logic for Profesor_dodavanje.xaml
    /// </summary>
    public partial class Profesor_dodavanje : Window, INotifyPropertyChanged
    {
        private PorfesorController _controller;
        private AdresaController kontrolerAdresa;

        public Profesor Profesor { get; set; }

        public Profesor_dodavanje()
        {
            InitializeComponent();
            DataContext = this;
            Profesor = new Profesor();

            _controller = new PorfesorController();
            kontrolerAdresa = new AdresaController();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool provera = true;
            try
            {
                Profesor.DatumRodjenja = System.Convert.ToDateTime(Profesor.Dat);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                provera = false;
            }
            string[] deloviAdreseSta = Profesor.Adrsta.Split(',');
            string[] deloviAdreseKan = Profesor.Adrkan.Split(',');
            string ulica = deloviAdreseSta[0];
            int adresniBroj = System.Convert.ToInt32(deloviAdreseSta[1]);
            string grad = deloviAdreseSta[2];
            string drzava = deloviAdreseSta[3];
            Profesor.AdresaStanovanja = new Adresa(ulica, adresniBroj, grad, drzava);
            ulica = deloviAdreseKan[0];
            adresniBroj = System.Convert.ToInt32(deloviAdreseKan[1]);
            grad = deloviAdreseKan[2];
            drzava = deloviAdreseKan[3];
            Profesor.AdresaKancelarije = new Adresa(ulica, adresniBroj, grad, drzava);
            PorfesorController controlerProfesor = new PorfesorController();
            List<Profesor> profesori = controlerProfesor.VratiSveProfesore();
            if (provera)
            {
                _controller.DodajProfesora(Profesor);
                kontrolerAdresa.Create(Profesor.AdresaStanovanja);
                kontrolerAdresa.Create(Profesor.AdresaKancelarije);
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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

        private void Window_LayoutUpdated(object sender, System.EventArgs e)
        {
            if (Profesor.IsValid)
                DugmePotvrdi.IsEnabled = true;
            else
                DugmePotvrdi.IsEnabled = false;
        }
    }
}
