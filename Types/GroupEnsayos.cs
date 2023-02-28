using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uwpIntentoNuevo.Types
{
    public class GroupEnsayos
    {
        public DateTime FechaEnsayo { get; set; }
        public string statePat { get; set; }
        public double valuePat { get; set; }
        public string stateCfp { get; set; }
        public double valueCfp { get; set; }

        public GroupEnsayos(DateTime dateTime, string StatePat, double ValuePat, string StateCfp, double ValueCfp)
        {
            this.FechaEnsayo = dateTime;
            this.statePat = StatePat;
            this.valuePat = ValuePat;
            this.stateCfp = StateCfp;
            this.valueCfp = ValueCfp;
        }

    }
}
