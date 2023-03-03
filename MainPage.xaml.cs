using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using uwpIntentoNuevo.DB;
using uwpIntentoNuevo.view;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace uwpIntentoNuevo
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DB.ConecctionSqLite sqLite;

        public MainPage()
        {
            this.InitializeComponent();
            sqLite = new DB.ConecctionSqLite();
        }
        private void NavigationButton1(object sender, RoutedEventArgs e)
        {
            //DB.ConecctionSqLite.addRecord("pasancosas11111@gmail.com", "Alvaro");
            //DB.ConecctionSqLite.addRecord("pasancosas22222@gmail.com", "Alvaro");
            //DB.ConecctionSqLite.addRecord("pasancosas33333@gmail.com", "Alvaro");

            Collection<EnsayosDBModel> data = DB.ConecctionSqLite.GetRecords();

            //frame.Navigate(typeof(Config));
        }

        private void NavigationButton2(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(historial));
        }

        private void NavigationButtonEnsayo(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(Ensayos));
        }

    }
}
