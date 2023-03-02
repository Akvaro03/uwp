using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        state.State stateEnsayoCFP;
        state.State stateEnsayoPAT;


        public Ensayos()
        {
            this.InitializeComponent();
            stateEnsayoCFP = state.State.none;
            stateEnsayoPAT = state.State.none;
            Ensayar = new EnsayarClass();
            coneccion = new DB.coneccion();
        }


        public void NavigationHome(object sender, RoutedEventArgs e)
        {
            if (stateEnsayoCFP != state.State.inProcess && stateEnsayoPAT != state.State.inProcess)
            {
                Ensayar.CloseBt();
                frame.Navigate(typeof(MainPage));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentFuga.Background = new SolidColorBrush(Windows.UI.Colors.BlueViolet);
            ContentPuesta.Background = new SolidColorBrush(Windows.UI.Colors.BlueViolet);
            stateEnsayoCFP = state.State.inProcess;
            stateEnsayoPAT = state.State.inProcess;

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

            stateEnsayoPAT = state.State.succes;
            CreateEnsayoAndSend("PAT", pass.ToString(), float.Parse(value, CultureInfo.InvariantCulture.NumberFormat), VerificationKey.ToString());

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

            stateEnsayoCFP = state.State.succes;
            CreateEnsayoAndSend("CFP", pass.ToString(), float.Parse(value, CultureInfo.InvariantCulture.NumberFormat), VerificationKey.ToString());

        }

        private void CreateEnsayoAndSend(string nombreEnsayo, string state, float value, string VerificationKey)
        {
            EnsayoDbModel data = new EnsayoDbModel(nombreEnsayo, value, state, DateTime.Today.Date, VerificationKey);
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
