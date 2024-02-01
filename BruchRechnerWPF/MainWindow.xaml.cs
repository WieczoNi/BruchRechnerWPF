using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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


namespace BruchRechnerWPF

{
    

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        float dezimal1;
        float dezimal2;
        double ergebnis;
        public int nE= 0;
        public int zE = 0;
        public int gZ = 0;
        
        public struct Fraction
        {
            public Fraction(int n, int d)
            {
                N = n;
                D = d;
            }

            public int N { get; private set; }
            public int D { get; private set; }
        }

        public MainWindow()
        {
            InitializeComponent();
           


        }
        public int bn;
        void dezimalrechner()
        {
            try
            {
                dezimal1 = float.Parse(zähler1.Text) / float.Parse(nenner1.Text);
                dezimal2 = float.Parse(zähler2.Text) / float.Parse(nenner2.Text);
            }
            catch
            {
                dezimal1 = 0;
                dezimal2 = 0;
            }
        }
        void update()
        {
            while (nE <= zE)
            {
                zE = zE - nE;
                gZ = gZ + 1;
            }
            while ((nE - (2 * nE)) >= zE)
            {
                zE = zE + nE;
                gZ = gZ - 1;
            }
            bn = nE;
           if (gZ != 0)
            {
                ganzZahl.Visibility = Visibility.Visible;
            }
            else { ganzZahl.Visibility = Visibility.Hidden; }
            nennerErgebnis.Text = nE.ToString();
            zählerErgebnis.Text = zE.ToString();
            ganzZahl.Content = gZ.ToString();
            gZ = 0;

        }

    
    
        public Fraction RealToFraction(double value, double accuracy)
        {
            if (accuracy <= 0.0 || accuracy >= 1.0)
            {
                throw new ArgumentOutOfRangeException("accuracy", "Must be > 0 and < 1.");
            }

            int sign = Math.Sign(value);

            if (sign == -1)
            {
                value = Math.Abs(value);
            }


            double maxError = sign == 0 ? accuracy : value * accuracy;

            int n = (int)Math.Floor(value);
            value -= n;

            if (value < maxError)
            {
                return new Fraction(sign * n, 1);
            }

            if (1 - maxError < value)
            {
                return new Fraction(sign * (n + 1), 1);
            }

            double z = value;
            int previousDenominator = 0;
            int denominator = 1;
            int numerator;

            do
            {
                z = 1.0 / (z - (int)z);
                int temp = denominator;
                denominator = denominator * (int)z + previousDenominator;
                previousDenominator = temp;
                numerator = Convert.ToInt32(value * denominator);
            }
            while (Math.Abs(value - (double)numerator / denominator) > maxError && z != (int)z);



            return new Fraction((n * denominator + numerator) * sign, denominator);
        }

        private void zähler1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void nenner1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if(plus.IsSelected == true)
            {
                dezimalrechner();
                ergebnis = dezimal1 + dezimal2;
                nE = RealToFraction(ergebnis, 0.00001).D;
                zE = RealToFraction(ergebnis, 0.00001).N;
                update();
            }
            else if (minus.IsSelected == true)
            {
                dezimalrechner();
                ergebnis = dezimal1 - dezimal2;
                nE = RealToFraction(ergebnis, 0.00001).D;
                zE = RealToFraction(ergebnis, 0.00001).N;
                update();
            }
            else if (mal.IsSelected == true)
            {
                dezimalrechner();
                ergebnis = dezimal1 * dezimal2;
                nE = RealToFraction(ergebnis, 0.00001).D;
                zE = RealToFraction(ergebnis, 0.00001).N;
                update();
            }
            else if (geteilt.IsSelected == true)
            {
                dezimalrechner();
                ergebnis = dezimal1 / dezimal2;
                nE = RealToFraction(ergebnis, 0.00001).D;
                zE = RealToFraction(ergebnis, 0.00001).N;
                update();
            }
        }
    }
}
