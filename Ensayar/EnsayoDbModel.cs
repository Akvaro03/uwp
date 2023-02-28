using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uwpIntentoNuevo.DB;

namespace uwpIntentoNuevo.Ensayar
{
    public class EnsayoDbModel
    {
        public string NombreEnnsayo { get; set; }
        public float ValorEnsayo { get; set; }
        public int id { get; set; }
        public string EstadoEnsayo { get; set; }
        public DateTime FechaEnsayo { get; set; }
        public string VerificacionKey { get; set; }

        public EnsayoDbModel(string NombreEnsayo, float Value, string state, DateTime dateTime, string Verification)
        {
            this.NombreEnnsayo = NombreEnsayo;
            this.ValorEnsayo = Value;
            this.EstadoEnsayo = state;
            this.FechaEnsayo = dateTime;
            this.VerificacionKey = Verification;
        }

    }
}
