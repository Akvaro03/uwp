using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uwpIntentoNuevo.DB
{
    public class EnsayosDBModel
    {

        public string NombreEnsayo { get; set; }
        public Double ValorEnsayo { get; set; }
        public int id { get; set; }
        public string EstadoEnsayo { get; set; }
        public DateTime FechaEnsayo { get; set; }
        public string VerificacionKey { get; set; }
        public EnsayosDBModel()
        {
        }

        public EnsayosDBModel(object[] args) : this()
        {
            this.NombreEnsayo = (string)args.ElementAt(0);
            this.EstadoEnsayo = (string)args.ElementAt(1);
            this.FechaEnsayo = Convert.ToDateTime(args.ElementAt(2));
            this.ValorEnsayo = (Double)args.ElementAt(3);
            this.id = Convert.ToInt32(args.ElementAt(4));
            this.VerificacionKey = (string)args.ElementAt(5);
            
        }

    }


}
