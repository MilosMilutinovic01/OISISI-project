using StudentskaSluzbaGUI.Controller;
using StudentskaSluzbaGUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace StudentskaSluzbaGUI.View
{
    /// <summary>
    /// Interaction logic for DodavanjeSefaKatedre.xaml
    /// </summary>
    public partial class DodavanjeSefaKatedre : Window
    {
        KatedraController managerKatedra = new KatedraController();
        Katedra IzabranaKatedra;
        List<Profesor> profesori;

        public DodavanjeSefaKatedre(Katedra izabranaKatedra)
        {
            IzabranaKatedra = izabranaKatedra;
            InitializeComponent();
            profesori = izabranaKatedra.spisakProfesora;
            foreach (Profesor p in profesori)
            {
                ListProfesora.Items.Add(p.Ime + ' ' + p.Prezime);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ListProfesora.Items.Count; i++)
            {
                if (ListProfesora.SelectedIndex == i)
                {
                    foreach (Profesor p in profesori)
                    {
                        if (p.Id == i)
                            IzabranaKatedra.SefKatedre = i;
                    }
                }
            }
            managerKatedra.DodajSefa(IzabranaKatedra.SefKatedre, IzabranaKatedra.SifraKatedre);
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
