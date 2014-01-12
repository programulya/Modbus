using System.IO.Ports;

namespace Modbus
{
    public class Connection
    {
        private SerialPort _port;

        private bool PortIsOpen
        {
            get { return _port != null && _port.IsOpen; }
        }

        public SerialPort Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public void OpenPort()
        {
            ClosePort();
            Modbus.OpenPort(_port);
        }

        public void ClosePort()
        {
            if (PortIsOpen)
            {
                Modbus.ClosePort(_port);
            }
        }
    }
}