using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using uwpIntentoNuevo.BT;
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
        public bool IsFugaChecked { get; set; }

        public bool IsPuestaChecked { get; set; }

        public Ensayos()
        {
            DataContext = IsPuestaChecked;
            this.InitializeComponent();
            bt = new BtConnection();
        }


        public void NavigationHome(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(MainPage));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool hola = IsFugaChecked;
            bool hola2 = IsPuestaChecked;
            try
            {
                bt.Connect();
                bt.SendData(DataToSend.data.DG);
                string respuesta = bt.ReadData();
            }
            catch (SystemException)
            {
            }

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
