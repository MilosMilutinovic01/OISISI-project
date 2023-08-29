using System;
using System.Collections.Generic;
using System.Windows;
using StudentskaSluzbaGUI.Model;
using StudentskaSluzbaGUI.Controller;
using StudentskaSluzbaGUI.Model.DAO;
using System.Collections.ObjectModel;

namespace StudentskaSluzbaGUI.View
{
    /// <summary>
    /// Interaction logic for Dodavanje_predmeta.xaml
    /// </summary>
    public partial class Dodavanje_predmeta : Window
    {
        public Dodavanje_predmeta(int godina)
        {
            InitializeComponent();
            this.DataContext = this;
            PredmetController managerPredmet = new PredmetController();
            List<Predmet> predmeti = managerPredmet.VratiSvePredmete();
            foreach(Predmet p in predmeti)
            {
                if (p.GodinaStudija == godina)
                    ListBox1.Items.Add(p.SifraPredmeta + " - " + p.NazivPredmeta);
            }
                
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
