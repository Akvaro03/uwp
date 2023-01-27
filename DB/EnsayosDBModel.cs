using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uwpIntentoNuevo.DB
{
    public class EnsayosDBModel
    {
        public EnsayosDBModel()
        {

        }

        public EnsayosDBModel(object[] args):this()
        {
            //
        }

        public string NombreEnsayo { get; set; }
        public float ValorEnsayo { get; set; }
        public int id { get; set; }
        public string EstadoEnsayo { get; set; }
        public DateTime FechaEnsayo { get; set; }
        public string VerificacionKey { get; set; }
    }
}
