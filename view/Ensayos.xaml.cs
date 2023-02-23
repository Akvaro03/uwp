using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

            string resp = await Ensayar.Ensayar(isFugaChecked, isPuestaChecked);


        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged_1(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
