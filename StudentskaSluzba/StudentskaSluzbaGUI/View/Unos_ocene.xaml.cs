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
    /// Interaction logic for Unos_ocene.xaml
    /// </summary>
    public partial class Unos_ocene : Window
    {
        public Unos_ocene()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                if (MainWindow.lang == MainWindow.SRB)
                {
                    MainWindow.app.ChangeLanguage(MainWindow.ENG);
                    MainWindow.lang = MainWindow.ENG;
                }
                else if (MainWindow.lang == MainWindow.ENG)
                {
                    MainWindow.app.ChangeLanguage(MainWindow.SRB);
                    MainWindow.lang = MainWindow.SRB;
                }
            }
        }
    }
}
