using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
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

        BluetoothLEDevice connection;

        private DeviceWatcher deviceWatcher;

        public string HEART_RATE_SERVICE_ID = "180D";

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


            while (true)
            {
                if(Device == null)
                {
                }
                else if(Device.Name == "HC-06")
                {
                    Connect();
                    break;
                }
            }

        }

        public async void Connect()
        {
            connection = await BluetoothLEDevice.FromIdAsync(Device.Id);
            var adrres = connection.BluetoothAddress;
            //readData();
        }

        public async void readData()
        {
            GattDeviceServicesResult result = await connection.GetGattServicesAsync();

            if (result.Status == GattCommunicationStatus.Success)
            {
                var services = result.Services;
                foreach(var service in services)
                {
                    if(service.Uuid.ToString("N").Substring(4,4) == HEART_RATE_SERVICE_ID)
                    {
                        var encontte = 2322;
                    }
                }

                //var service = services.First();

                //GattCharacteristicsResult result2 = await service.GetCharacteristicsAsync();

                //if (result.Status == GattCommunicationStatus.Success)
                //{
                //    var characteristics = result2.Characteristics;

                //    var characteristic = characteristics.First();
                //    GattCharacteristicProperties properties = characteristic.CharacteristicProperties;

                //    GattReadResult result2323 = await characteristic.ReadValueAsync();

                //    var reader = DataReader.FromBuffer(result2323.Value);

                //    byte[] input = new byte[reader.UnconsumedBufferLength];

                //    reader.ReadBytes(input);


                //}

            }

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
            var name = args.Name;
            //throw new NotImplementedException();
            if(args.Name == "HC-06")
                Device = args;
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
