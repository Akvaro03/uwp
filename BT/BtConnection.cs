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
using uwpIntentoNuevo.Enums;
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
        private DeviceWatcher deviceWatcher;
        private BluetoothClient bluetoothClient;
        private string nameBt;
        private ulong direction;
        NetworkStream stream;
        public BtConnection() : this("HC-06")
        {
        }
        public BtConnection(string NameBt) 
        {
            nameBt = NameBt;

            deviceWatcher = null;
            string[] requestedProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected" };

            deviceWatcher =
                        DeviceInformation.CreateWatcher(
                                BluetoothLEDevice.GetDeviceSelectorFromPairingState(false),
                                requestedProperties,
                                DeviceInformationKind.AssociationEndpoint);

            deviceWatcher.Added += DeviceWatcher_Added;
            deviceWatcher.Updated += DeviceWatcher_Updated;
            deviceWatcher.Removed += DeviceWatcher_Removed;

            // EnumerationCompleted and Stopped are optional to implement.
            deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;
            deviceWatcher.Stopped += DeviceWatcher_Stopped;

            SearchDirection();
        }

        public void SearchDirection()
        {

            
            deviceWatcher.Start();

            while (stop)
            {
                if(Device == null)
                {
                }
                else if (Device.Name == nameBt)
                {
                    GetAddress();
                    stop = false;
                    break;
                }
            }
        }

        private async void GetAddress()
        {
            connection = await BluetoothLEDevice.FromIdAsync(Device.Id);
            var Direccion = connection.BluetoothAddress;
            connection.Dispose();
            deviceWatcher.Stop();

            direction = Direccion;
        }

        /// <summary>
        /// Se conecta al dispositivo bluetooth
        /// </summary>
        /// <param name="direccion">
        /// Direccion para conectarse al dispositivo bluetooth
        /// </param>
        /// <returns></returns>
        public  state.State Connect()
        {
            bluetoothClient = new BluetoothClient();

            var port = BluetoothService.SerialPort;

            var EndPoint = new BluetoothEndPoint(direction, port);

            bluetoothClient.Connect(EndPoint);

            stream = bluetoothClient.GetStream();
            return state.State.succes;
        }

        /// <summary>
        /// Mandar datos a travez del bluetooth
        /// </summary>
        public state.State SendData(DataToSend.data type,string dataToSend)
        {
            stream.Flush();
            string space = "\r\n";


            switch (type)
            {
                case DataToSend.data.DG:
                    dataToSend = "{DG}" + space;
                    break;
            }

            byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(dataToSend);
            stream.Write(dataBytes, 0, dataBytes.Length);
            return state.State.succes;
       }

        public state.State SendData(DataToSend.data type)
        {
            return SendData(type, null);
        }

        public string ReadData() 
        {
            stream.Flush();

            byte[] dataReceived = new byte[100000];

            Thread.Sleep(3000);

            var bytesRead = stream.Read(dataReceived, 0, dataReceived.Length);

            string receivedMessage = System.Text.Encoding.UTF8.GetString(dataReceived, 0, bytesRead);
            string cleaned = receivedMessage.Replace("\n", "").Replace("\r", "").Replace("DG{", "").Replace("}", "");

            return cleaned;
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
            if(args.Name == nameBt)
            {
                Device = args;
            }
        }
    }
}