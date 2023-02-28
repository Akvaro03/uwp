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
using uwpIntentoNuevo.DB;
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
        private readonly EnsayarClass Ensayar;
        DB.coneccion coneccion;


        public Ensayos()
        {
            this.InitializeComponent();
            Ensayar = new EnsayarClass();
            coneccion = new DB.coneccion();
        }


        public void NavigationHome(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(MainPage));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentFuga.Background = new SolidColorBrush(Windows.UI.Colors.BlueViolet);
            ContentPuesta.Background = new SolidColorBrush(Windows.UI.Colors.BlueViolet);

            Random rnd = new Random();
            int VerificationKey = rnd.Next(1, 100000);


            bool isFugaChecked = (bool)FugaChecked.IsChecked;
            bool isPuestaChecked = (bool)PuestaChecked.IsChecked;
            if (isFugaChecked && isPuestaChecked)
            {
                ContentFuga.Background = new SolidColorBrush(Windows.UI.Colors.Yellow);
                ContentPuesta.Background = new SolidColorBrush(Windows.UI.Colors.Yellow);
                ManageDatosFuga(VerificationKey, true);
            }
            else if (isFugaChecked)
            {
                ContentFuga.Background = new SolidColorBrush(Windows.UI.Colors.Yellow);
                _ = ManageDatosFuga(VerificationKey);
            }
            else if (isPuestaChecked)
            {
                ContentPuesta.Background = new SolidColorBrush(Windows.UI.Colors.Yellow);
                ManageDatosPuesta(VerificationKey);
            }

        }

        private async Task ManageDatosFuga(int VerificationKey)
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

            CreateEnsayoAndSend("PAT", pass.ToString(), DateTime.Today, float.Parse(value), VerificationKey.ToString());

        }
        private async void ManageDatosFuga(int VerificationKey, bool dato)
        {
            Task Task = ManageDatosFuga(VerificationKey);
            await Task;
            ManageDatosPuesta(VerificationKey);
        }
        private async void ManageDatosPuesta(int VerificationKey)
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

            CreateEnsayoAndSend("CFP", pass.ToString(), DateTime.Today, float.Parse(value), VerificationKey.ToString());

        }

        private void CreateEnsayoAndSend(string nombreEnsayo, string state, DateTime dateTime, float value, string VerificationKey)
        {
            EnsayoDbModel data = new EnsayoDbModel(nombreEnsayo, value, state, dateTime, VerificationKey);
            coneccion.sendData(data);

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
