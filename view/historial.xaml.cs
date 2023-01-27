using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using uwpIntentoNuevo.DB;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace uwpIntentoNuevo.view
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class historial : Page
    {
        DB.coneccion coneccion;

        EnsayosDBModel[] Prueba = new EnsayosDBModel[10];
        public historial()
        {
            this.InitializeComponent();

            

            coneccion = new DB.coneccion();
            var data = coneccion.getData();

            DataContext= data;
        }


    }
}
