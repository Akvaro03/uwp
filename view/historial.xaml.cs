using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using uwpIntentoNuevo.DB;
using uwpIntentoNuevo.Types;
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

        public Collection<EnsayosDBModel> ensayos { get; set; }

        public string NombreBuscador { get; set; }

        public historial()
        {
            this.InitializeComponent();

            this.NombreBuscador = "";

            coneccion = new DB.coneccion();
            Collection<EnsayosDBModel> Data = coneccion.getData();
            ensayos = Data;
            Collection<GroupEnsayos> ordenEnsayos = OrderCollection(Data);
            this.DataContext = this;
        }

        private Collection<GroupEnsayos> OrderCollection(Collection<EnsayosDBModel> ensayos)
        {
            Collection<GroupEnsayos> groupEnsayos = new Collection<GroupEnsayos>();

            for (int i = 1; i < ensayos.Count; i++)
            {
                EnsayosDBModel actualEnsayo = ensayos[i];
                EnsayosDBModel anteriorEnsayo = ensayos[i - 1];
                GroupEnsayos group;
                if (anteriorEnsayo.VerificacionKey == actualEnsayo.VerificacionKey)
                {

                    if (actualEnsayo.NombreEnsayo == "CFP")
                        group = new GroupEnsayos(actualEnsayo.FechaEnsayo, anteriorEnsayo.EstadoEnsayo, anteriorEnsayo.ValorEnsayo, actualEnsayo.EstadoEnsayo, actualEnsayo.ValorEnsayo);
                    else
                        group = new GroupEnsayos(actualEnsayo.FechaEnsayo, actualEnsayo.EstadoEnsayo, actualEnsayo.ValorEnsayo, anteriorEnsayo.EstadoEnsayo, anteriorEnsayo.ValorEnsayo);

                    groupEnsayos.Add(group);
                }
            }

            return groupEnsayos;
        }

        public void NavigationHome(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(MainPage));
        }


    }
}
