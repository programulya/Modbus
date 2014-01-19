using System.IO.Ports;

namespace Modbus
{
    public class Connection
    {
        public SerialPort Port { get; set; }

        private bool PortIsOpen
        {
            get { return Port != null && Port.IsOpen; }
        }

        public void OpenPort()
        {
            ClosePort();
            Modbus.OpenPort(Port);
        }

        public void ClosePort()
        {
            if (PortIsOpen)
            {
                Modbus.ClosePort(Port);
            }
        }
    }
}