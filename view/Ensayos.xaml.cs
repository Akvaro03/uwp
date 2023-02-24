using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading;
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
            bt = new BtConnection();
            Ensayar = new EnsayarClass();
        }


        public void NavigationHome(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(MainPage));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            bool isFugaChecked = (bool)FugaChecked.IsChecked;
            bool isPuestaChecked = (bool)PuestaChecked.IsChecked;

            ContentFuga.Background = new SolidColorBrush(Windows.UI.Colors.Yellow);
            ContentPuesta.Background = new SolidColorBrush(Windows.UI.Colors.Yellow);
            Thread.Sleep(5000);
            string[] resp = await Ensayar.Ensayar(isFugaChecked, isPuestaChecked);

            bool pass1 = IsPassed(resp[0]);
            bool pass2 = IsPassed(resp[1]);


        }

        private bool IsPassed(string stringToVerificate)
        {
            string recort = stringToVerificate.ToString().Substring((stringToVerificate.Length - 15), 15);
            return Regex.IsMatch(recort, ";");
        }

    }
}
