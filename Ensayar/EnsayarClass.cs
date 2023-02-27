using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uwpIntentoNuevo.BT;
using uwpIntentoNuevo.Enums;
using Windows.Foundation;

namespace uwpIntentoNuevo.Ensayar
{
    internal class EnsayarClass
    {
        public BtConnection bt;

        public EnsayarClass()
        {
            bt = new BtConnection();
        }

        public async Task<string[]> Ensayar(int ensayoType)
        {
            string respuesta = "";
            string respuesta2 = "";

            if (ensayoType == 1)
            {

                while (respuesta.Length < 10)
                {
                    respuesta = await bt.SendData(DataToSend.data.E1);
                };

            }
            else
            {
                while (respuesta2.Length < 10)
                {
                    respuesta2 = await bt.SendData(DataToSend.data.E2);
                }
            }

            string[] resp = new string[2] { respuesta, respuesta2 };
            return resp;
        }

    }
}
