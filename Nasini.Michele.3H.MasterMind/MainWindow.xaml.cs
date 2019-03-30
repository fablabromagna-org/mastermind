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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Nasini.Michele._3H.MasterMind.models;
//
// Sviluppato nel 2019 da Nasini, Petrangolini, Conti - ITTS "Belluzzi - da Vinci", Rimini
//
namespace Nasini.Michele._3H.MasterMind
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Brush[] coloriUsati = { Brushes.Red, Brushes.Green, Brushes.Yellow, Brushes.Blue };
        Master sequenzaSegreta = new Master();
        Random rnd = new Random();
        Brush colore=Brushes.White;
        Master[] prove = new Master[8];

        int tentativi = 0;

        public MainWindow()
        {
            InitializeComponent();

            riempiAcaso(sequenzaSegreta);

            s1.Fill = sequenzaSegreta.colore1;
            s2.Fill = sequenzaSegreta.colore2;
            s3.Fill = sequenzaSegreta.colore3;
            s4.Fill = sequenzaSegreta.colore4;

            for (int i = 0; i < prove.Length; i++) {
                prove[i] = new Master();
                prove[i].colore1 = Brushes.White;
                prove[i].colore2 = Brushes.White;
                prove[i].colore3 = Brushes.White;
                prove[i].colore4 = Brushes.White;
                prove[i].colorePos=0;
                prove[i].soloColore=0;
            }

            dgDati.ItemsSource = prove;
        }

        void riempiAcaso(Master _m)
        {
            int indiceCaso = rnd.Next(0, 4);
            _m.colore1 = coloriUsati[indiceCaso];

            indiceCaso = rnd.Next(0, 4);
            _m.colore2 = coloriUsati[indiceCaso];

            indiceCaso = rnd.Next(0, 4);
            _m.colore3 = coloriUsati[indiceCaso];

            indiceCaso = rnd.Next(0, 4);
            _m.colore4 = coloriUsati[indiceCaso];
        }

        private void C1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle r = sender as Rectangle;
            colore = r.Fill;

            MessageBox.Show(colore.ToString());
        }

        private void P1_MouseEnter(object sender, MouseEventArgs e)
        {
            Ellipse el = sender as Ellipse;
            el.Fill = colore;
        }

        private void BtnSalva_Click(object sender, RoutedEventArgs e)
        {
            if (tentativi < prove.Length)
            {
                prove[tentativi].colore1 = p1.Fill;
                prove[tentativi].colore2 = p2.Fill;
                prove[tentativi].colore3 = p3.Fill;
                prove[tentativi].colore4 = p4.Fill;
                dgDati.Items.Refresh();

                tentativi++;
            }
            else
                MessageBox.Show("Hai perso!!!");
        }
    }
}
