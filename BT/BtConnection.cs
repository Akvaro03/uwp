using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Xamarin.Forms;

namespace uwpIntentoNuevo.BT
{
    internal class BtConnection
    {
        public DeviceInformation Device = null;
        private bool stop = true;
        BluetoothLEDevice connection;
        private ulong address;
        private DeviceWatcher deviceWatcher;
        private BluetoothClient bluetoothClient;

        public BtConnection()
        {
        }

        public void ShowDat()
        {

            //deviceWatcher = null;
            ////// Query for extra properties you want returned
            //string[] requestedProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected" };

            //deviceWatcher =
            //            DeviceInformation.CreateWatcher(
            //                    BluetoothLEDevice.GetDeviceSelectorFromPairingState(false),
            //                    requestedProperties,
            //                    DeviceInformationKind.AssociationEndpoint);

            //// Register event handlers before starting the watcher.
            //// Added, Updated and Removed are required to get all nearby devices
            //deviceWatcher.Added += DeviceWatcher_Added;
            //deviceWatcher.Updated += DeviceWatcher_Updated;
            //deviceWatcher.Removed += DeviceWatcher_Removed;

            //// EnumerationCompleted and Stopped are optional to implement.
            //deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;
            //deviceWatcher.Stopped += DeviceWatcher_Stopped;
            ////bluetoothClient = new BluetoothClient();

            ////IReadOnlyCollection<BluetoothDeviceInfo> datos = bluetoothClient.DiscoverDevices(5);


            //deviceWatcher.Start();
            ////BluetoothClient intent = new BluetoothClient();
            ////intent.DiscoverDevicesInRangeTimeOut = TimeSpan.FromSeconds(5);

            ////IReadOnlyCollection<BluetoothDeviceInfo> devices = intent.DiscoverDevices(2);

            ////foreach (BluetoothDeviceInfo item in devices)
            ////{
            ////    var i = item.DeviceAddress;
            ////}

            //while (stop)
            //{
            //    if (Device == null)
            //    {
            //    }
            //    else if (Device.Name == "HC-06")
            //    {
            //        ManageBL();
            //        break;
            //    }
            //}
            ManageBL();
        }
        /// <summary>
        /// Maneja temporalmente el BL
        /// </summary>
        public void ManageBL()
        {
            //ulong direccion = await GetAddress();
            ulong direccion = 168063681168911;
            string respuesta = Connect(direccion);

            Thread.Sleep(5000);

            SendData("{DG}\r\n");
        }
        public async Task<ulong> GetAddress()
        {
            connection = await BluetoothLEDevice.FromIdAsync(Device.Id);
            var direccion = connection.BluetoothAddress;
            connection.Dispose();
            deviceWatcher.Stop();

            return direccion;
        }

        /// <summary>
        /// Se conecta al dispositivo bluetooth
        /// </summary>
        /// <param name="direccion">
        /// Direccion para conectarse al dispositivo bluetooth
        /// </param>
        /// <returns></returns>
        public  string Connect(ulong direccion)
        {
            bluetoothClient = new BluetoothClient();

            var port = BluetoothService.SerialPort;

            var EndPoint = new BluetoothEndPoint(direccion, port);

            bluetoothClient.Connect(EndPoint);

            return "funciono";
         
        }

        /// <summary>
        /// Mandar datos a travez del bluetooth
        /// </summary>
        private void SendData(string dataToSend)
        {
            NetworkStream stream = bluetoothClient.GetStream();

            byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(dataToSend);
            stream.Write(dataBytes, 0, dataBytes.Length);
        
            stream.Flush();

            byte[] dataReceived = new byte[100000];
            var bytesRead = stream.Read(dataReceived, 0, dataReceived.Length);

            string receivedMessage = System.Text.Encoding.UTF8.GetString(dataReceived, 0, bytesRead);
        }



        //Funciones por defecto para deviceWatcher
        private void DeviceWatcher_Stopped(DeviceWatcher sender, object args)
        {
            //throw new NotImplementedException();
        }

        private void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object args)
        {
            //throw new NotImplementedException();
        }

        private void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            //throw new NotImplementedException();
        }

        private void DeviceWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            //throw new NotImplementedException();
        }

        private void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
        {
            if(args.Name == "HC-06")
            {
                Device = args;
                stop = true;
            }
        }
    }
}