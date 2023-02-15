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
            deviceWatcher = null;
            //// Query for extra properties you want returned
            string[] requestedProperties = { "System.Devices.Aep.DeviceAddress", "System.Devices.Aep.IsConnected" };

            deviceWatcher =
                        DeviceInformation.CreateWatcher(
                                BluetoothLEDevice.GetDeviceSelectorFromPairingState(false),
                                requestedProperties,
                                DeviceInformationKind.AssociationEndpoint);

            // Register event handlers before starting the watcher.
            // Added, Updated and Removed are required to get all nearby devices
            deviceWatcher.Added += DeviceWatcher_Added;
            deviceWatcher.Updated += DeviceWatcher_Updated;
            deviceWatcher.Removed += DeviceWatcher_Removed;

            // EnumerationCompleted and Stopped are optional to implement.
            deviceWatcher.EnumerationCompleted += DeviceWatcher_EnumerationCompleted;
            deviceWatcher.Stopped += DeviceWatcher_Stopped;
        }

        public void ShowDat()
        {
            //bluetoothClient = new BluetoothClient();

            //IReadOnlyCollection<BluetoothDeviceInfo> datos = bluetoothClient.DiscoverDevices(5);


            deviceWatcher.Start();
            //BluetoothClient intent = new BluetoothClient();
            //intent.DiscoverDevicesInRangeTimeOut = TimeSpan.FromSeconds(5);

            //IReadOnlyCollection<BluetoothDeviceInfo> devices = intent.DiscoverDevices(2);

            //foreach (BluetoothDeviceInfo item in devices)
            //{
            //    var i = item.DeviceAddress;
            //}

            while (stop)
            {
                if (Device == null)
                {
                }
                else if (Device.Name == "HC-06")
                {
                    ManageBL();
                    break;
                }
            }

        }

        public async void ManageBL()
        {
            ulong direccion = await GetAddress();
            string respuesta = await Connect(direccion);

            Thread.Sleep(15000);

            SendData();
        }
        public async Task<ulong> GetAddress()
        {
            connection = await BluetoothLEDevice.FromIdAsync(Device.Id);
            var direccion = connection.BluetoothAddress;
            connection.Dispose();

            return direccion;
        }

        public async Task<string> Connect(ulong direccion)
        {
            bluetoothClient = new BluetoothClient();

            IReadOnlyCollection<BluetoothDeviceInfo> datos =   bluetoothClient.DiscoverDevices();

            var port = BluetoothService.SerialPort;

            var EndPoint = new BluetoothEndPoint(direccion, port);

            bluetoothClient.Connect(EndPoint);

            return "funciono";
         
        }

        private async void SendData()
        {
            NetworkStream stream = bluetoothClient.GetStream();

            string dataToSend = "{DG}\r\n";

            byte[] dataBytes = System.Text.Encoding.ASCII.GetBytes(dataToSend);
            stream.Write(dataBytes, 0, dataBytes.Length);

            byte[] dataReceived = new byte[10000];
            var bytesRead = stream.Read(dataReceived, 0, dataReceived.Length);

            string receivedMessage = System.Text.Encoding.UTF8.GetString(dataReceived, 0, bytesRead);

            //int bytesRead = 0

            //byte[] dataReceived = new byte[5024];

            //byte[] dataToSend = System.Text.Encoding.ASCII.GetBytes("{DG}\r\n");
            //stream.Write(dataToSend, 0, dataToSend.Length);

            //bytesRead = stream.Read(dataReceived, 0, dataReceived.Length);
        }

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




//public async void Connect()
//{
//    connection = await BluetoothLEDevice.FromIdAsync(Device.Id);

//    //readDataNatural();

//    //readDataSocket();
//}

//private void readDataSocket()
//{
//    BluetoothClient bluetoothClient = new BluetoothClient();
//    long adrees = (long)Convert.ToDouble(Device.Id);
//    //bluetoothClient.Connect(new BluetoothEndPoint(blue))
//}

//public async void readDataNatural()
//{
//    GattDeviceServicesResult result = await connection.GetGattServicesAsync();

//    if (result.Status == GattCommunicationStatus.Success)
//    {
//        var services = result.Services;
//        foreach (var service in services)
//        {
//            if (service.Uuid.ToString("N").Substring(4, 4) == HEART_RATE_SERVICE_ID)
//            {
//                var encontte = 2322;
//            }
//        }

//        //var service = services.First();

//        //GattCharacteristicsResult result2 = await service.GetCharacteristicsAsync();

//        //if (result.Status == GattCommunicationStatus.Success)
//        //{
//        //    var characteristics = result2.Characteristics;

//        //    var characteristic = characteristics.First();
//        //    GattCharacteristicProperties properties = characteristic.CharacteristicProperties;

//        //    GattReadResult result2323 = await characteristic.ReadValueAsync();

//        //    var reader = DataReader.FromBuffer(result2323.Value);

//        //    byte[] input = new byte[reader.UnconsumedBufferLength];

//        //    reader.ReadBytes(input);


//        //}

//    }

//}

//private void DeviceWatcher_Stopped(DeviceWatcher sender, object args)
//{
//    //throw new NotImplementedException();
//}

//private void DeviceWatcher_EnumerationCompleted(DeviceWatcher sender, object args)
//{
//    //throw new NotImplementedException();
//}

//private void DeviceWatcher_Removed(DeviceWatcher sender, DeviceInformationUpdate args)
//{
//    //throw new NotImplementedException();
//}

//private void DeviceWatcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
//{
//    //throw new NotImplementedException();
//}

//private void DeviceWatcher_Added(DeviceWatcher sender, DeviceInformation args)
//{
//    var name = args.Name;
//    //throw new NotImplementedException();
//    if (args.Name == "HC-06")
//        Device = args;
//}
//    }
//}
