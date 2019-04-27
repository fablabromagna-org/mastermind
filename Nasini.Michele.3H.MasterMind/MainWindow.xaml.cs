using System;
using System.Collections.Generic;
using System.IO;
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
        // Nome del file dove salvare...
        const string NOMEFILE_SEQUENZASEGRETA = "dati.txt";

        Brush[] coloriUsati = { Brushes.Red, Brushes.Green, Brushes.Yellow, Brushes.Magenta };
        SequenzaColori sequenzaSegreta { get; set; }
        Random rnd = new Random();
        Brush colore=Brushes.White;
        SequenzaColori[] prove = new SequenzaColori[8];

        int tentativi = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        void riempiAcaso(SequenzaColori _m)
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

        private void SceltaColore(object sender, MouseButtonEventArgs e)
        {
            Ellipse el = sender as Ellipse;
            el.Fill = colore;

            //MessageBox.Show(colore.ToString());
        }

        private void Colora(object sender, MouseEventArgs e)
        {
            Rectangle r = sender as Rectangle;
            colore = r.Fill;
        }

        private void BtnSalva_Click(object sender, RoutedEventArgs e)
        {
            if (tentativi < prove.Length)
            {
                string riga = $"{p1.Fill};{p2.Fill};{p3.Fill};{p4.Fill}";
                
                // Scrittura
                StreamWriter fileOut = new StreamWriter("dati.csv");
                fileOut.WriteLine( riga );
                fileOut.Close();

                // Lettura
                StreamReader fileIn = new StreamReader("dati.csv");
                string s = fileIn.ReadLine();
                fileIn.Close();

                MessageBox.Show(s);

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

        private SolidColorBrush getColore(string s )
        {
            // usare così!!!
            // SolidColorBrush b = getColore( "#FFFF0000" );

            var c = (Color)ColorConverter.ConvertFromString(s);
            var b = new SolidColorBrush(c);
            return b;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // dove noi teniamo la sequenza segreta
            sequenzaSegreta = new SequenzaColori();

            try
            {
                // Apriamo il file per leggere la sequenza segreta
                StreamReader fin = new StreamReader(NOMEFILE_SEQUENZASEGRETA);
                string riga = fin.ReadLine();
                string[] colonne = riga.Split(';');

                // La riga deve essere di quattro elementi divisi dal carattere ;
                if ( colonne.Count() == 4 )
                {
                    int idx;
                    int.TryParse(colonne[0], out idx);
                    sequenzaSegreta.colore1 = coloriUsati[idx];

                    int.TryParse(colonne[1], out idx);
                    sequenzaSegreta.colore2 = coloriUsati[idx];

                    int.TryParse(colonne[2], out idx);
                    sequenzaSegreta.colore3 = coloriUsati[idx];

                    int.TryParse(colonne[3], out idx);
                    sequenzaSegreta.colore4 = coloriUsati[idx];
                }
                else
                    // Se il file non è corretto... genera la sequenza casuale.
                    riempiAcaso(sequenzaSegreta);
            }
            catch
            {
                // se il file non esiste... genera la sequenza casuale.
                riempiAcaso(sequenzaSegreta);
            }

            // Visualizza i colori...
            s1.Fill = sequenzaSegreta.colore1;
            s2.Fill = sequenzaSegreta.colore2;
            s3.Fill = sequenzaSegreta.colore3;
            s4.Fill = sequenzaSegreta.colore4;

            for (int i = 0; i < prove.Length; i++)
            {
                prove[i] = new SequenzaColori();
                prove[i].colore1 = Brushes.White;
                prove[i].colore2 = Brushes.White;
                prove[i].colore3 = Brushes.White;
                prove[i].colore4 = Brushes.White;
                prove[i].colorePos = 0;
                prove[i].soloColore = 0;
            }

            dgDati.ItemsSource = prove;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Prima di uscire dal programma, viene chiamato questo evento.

            // prepara la stringa da salvare
            string riga = $"{indexcolore(sequenzaSegreta.colore1)};{indexcolore(sequenzaSegreta.colore2)};{indexcolore(sequenzaSegreta.colore3)};{indexcolore(sequenzaSegreta.colore4)}";
            
            // Salva la stringa nel file segreto...
            StreamWriter fout = new StreamWriter(NOMEFILE_SEQUENZASEGRETA);
            fout.WriteLine(riga);
            fout.Close();

        }

        public int indexcolore(Brush _B) {
            for (int i = 0; i < coloriUsati.Length; i++) {
                if (_B == coloriUsati[i]) {
                    return i;
                }
            }
            return 0;
        }

    }
}
