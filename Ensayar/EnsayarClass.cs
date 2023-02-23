using System;
using System.Collections.Generic;
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


        public async Task<string> Ensayar(params bool[] args)
        {
            try
            {
                bt.Connect();
                string respuesta = "";

                while (respuesta.Length < 7)
                {
                    respuesta = await bt.SendData(DataToSend.data.E1);
                };

                string respuesta2 = "";
                while (respuesta2.Length < 10)
                {
                    respuesta2 = await bt.SendData(DataToSend.data.E2);
                }

            }
            catch (SystemException)
            {
            }


            return "hola";
        }
    }
}
