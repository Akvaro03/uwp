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

        public async void GetAddress()
        {
            connection = await BluetoothLEDevice.FromIdAsync(Device.Id);
            address = connection.BluetoothAddress;
            connection.Dispose();
        }

        public void Connect()
        {
            bluetoothClient = new BluetoothClient();

            var EndPoint = new BluetoothEndPoint(address, BluetoothService.SerialPort);

            bluetoothClient.Connect(EndPoint);

            //GattDeviceServicesResult result = await connection.GetGattServicesAsync();

            //if (result.Status == GattCommunicationStatus.Success)
            //{
            //    var services = result.Services;
            //    foreach(var service in services)
            //    {
            //        if(service.Uuid.ToString("N").Substring(4,4) == HEART_RATE_SERVICE_ID)
            //        {
            //            var encontte = 2322;
            //        }
            //    }

            //    //var service = services.First();

            //    //GattCharacteristicsResult result2 = await service.GetCharacteristicsAsync();

            //    //if (result.Status == GattCommunicationStatus.Success)
            //    //{
            //    //    var characteristics = result2.Characteristics;

            //    //    var characteristic = characteristics.First();
            //    //    GattCharacteristicProperties properties = characteristic.CharacteristicProperties;

            //    //    GattReadResult result2323 = await characteristic.ReadValueAsync();

            //    //    var reader = DataReader.FromBuffer(result2323.Value);

            //    //    byte[] input = new byte[reader.UnconsumedBufferLength];

            //    //    reader.ReadBytes(input);


            //    //}

            //}

        }

        public void ManageBL()
        {
            GetAddress();
            Connect();
            SendData();
        }

        private void SendData() 
        {
            NetworkStream stream = bluetoothClient.GetStream();

            byte[] message = System.Text.Encoding.ASCII.GetBytes("{DG}");
            stream.Write(message,0,message.Length);

            byte[] response = new byte[1024];

            int byteReceived = stream.Read(response, 0, response.Length);

            var respuesta = Encoding.ASCII.GetString(response, 0, byteReceived);




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
