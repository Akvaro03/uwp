using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using uwpIntentoNuevo.BT;
using uwpIntentoNuevo.Ensayar;
using uwpIntentoNuevo.Enums;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace uwpIntentoNuevo.view
{
    public sealed partial class Ensayos : Page
    {
        private readonly BtConnection bt;
        private readonly EnsayarClass Ensayar;


        public Ensayos()
        {
            this.InitializeComponent();
            Ensayar = new EnsayarClass();
        }


        public void NavigationHome(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(MainPage));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            bool isFugaChecked = (bool)FugaChecked.IsChecked;
            if (isFugaChecked)
            {
                ContentFuga.Background = new SolidColorBrush(Windows.UI.Colors.Yellow);
                ManageDatosFuga();
            }

            //Stopwatch timeMeasure = new Stopwatch();
            //timeMeasure.Start();
            //timeMeasure.Stop();
            //double tempo = timeMeasure.Elapsed.TotalMilliseconds;

            bool isPuestaChecked = (bool)PuestaChecked.IsChecked;
            if (isPuestaChecked)
            {
                ContentPuesta.Background = new SolidColorBrush(Windows.UI.Colors.Yellow);
                ManageDatosPuesta();
            }

        }

        private async void ManageDatosFuga()
        {
            string prueba = await Task.Run(async () =>
            {
                string[] resp = await Ensayar.Ensayar(1);
                return resp[0];
            });

            bool pass = IsPassed(prueba);
            string value = GetValueFromString(prueba);

            ValueCfp.Text = value;
            if (pass)
                ContentFuga.Background = new SolidColorBrush(Windows.UI.Colors.DarkGreen);
            else
                ContentFuga.Background = new SolidColorBrush(Windows.UI.Colors.Red);

        }
        private async void ManageDatosPuesta()
        {
            string prueba = await Task.Run(async () =>
            {
                string[] resp = await Ensayar.Ensayar(2);
                return resp[1];




            });

            bool pass = IsPassed(prueba);
            string value = GetValueFromString(prueba);

            ValuePat.Text = value;
            if (pass)
                ContentPuesta.Background = new SolidColorBrush(Windows.UI.Colors.DarkGreen);
            else
                ContentPuesta.Background = new SolidColorBrush(Windows.UI.Colors.Red);

        }


        private bool IsPassed(string stringToVerificate)
        {
            if (stringToVerificate != "")
            {
                string recort = stringToVerificate.ToString().Substring((stringToVerificate.Length - 15), 15);
                return Regex.IsMatch(recort, ";");
            }
            else
            {
                return false;
            }
        }
        private string GetValueFromString(string stringToValue)
        {
            if (stringToValue != "")
            {
                string[] value = stringToValue.Split(";");
                return value[7];
            }
            return "";
        }

    }
}
