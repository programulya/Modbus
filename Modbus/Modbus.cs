using System;
using System.Configuration;
using System.IO.Ports;
using Modbus.Device;

namespace Modbus
{
    public static class Modbus
    {
        // Parameters from App.config
        private static readonly int ReadTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["ReadTimeout"]);
        private static readonly int WriteTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["WriteTimeout"]);
        private static readonly int AttemptsModbus = Convert.ToInt32(ConfigurationSettings.AppSettings["AttemptsModbus"]);

        // Counters of errors (should be displayed on main window of program)
        public static int ReadErrors { get; private set; }
        public static int WriteErrors { get; private set; }

        /// <summary>
        /// Open COM-port.
        /// </summary>
        /// <param name="port">Serial port.</param>
        public static void OpenPort(SerialPort port)
        {
            port.Open();
        }

        /// <summary>
        /// Close COM-port.
        /// </summary>
        /// <param name="port">Serial port.</param>
        public static void ClosePort(SerialPort port)
        {
            port.Close();
        }

        /// <summary>
        /// Read registers using Modbus RTU.
        /// </summary>
        /// <param name="port">Serial port.</param>
        /// <param name="slaveAddress">Slave device address.</param>
        /// <param name="startAddress">Start adress for reading.</param>
        /// <param name="numRegisters">Counter of registers that should be read.</param>
        /// <returns>Values of registers.</returns>
        public static ushort[] ReadRegisters(SerialPort port, byte slaveAddress, ushort startAddress, ushort numRegisters)
        {
            var registers = new ushort[numRegisters];
            if (port.IsOpen)
            {
                try
                {
                    var master = ModbusSerialMaster.CreateRtu(port);
                    master.Transport.ReadTimeout = ReadTimeout;
                    master.Transport.Retries = AttemptsModbus;
                    registers = master.ReadHoldingRegisters(slaveAddress, startAddress, numRegisters);
                }
                catch
                {
                    ++ReadErrors;
                }
            }

            return registers;
        }

        /// <summary>
        /// Wrire registers using Modbus RTU.
        /// </summary>
        /// <param name="port">Serial port.</param>
        /// <param name="slaveAddress">Slave device address.</param>
        /// <param name="startAddress">Start adress for writing.</param>
        /// <param name="registers">Values of registers.</param>
        public static void WriteRegisters(SerialPort port, byte slaveAddress, ushort startAddress, params ushort[] registers)
        {
            if (port.IsOpen)
            {
                try
                {
                    var master = ModbusSerialMaster.CreateRtu(port);
                    master.Transport.WriteTimeout = WriteTimeout;
                    master.Transport.Retries = AttemptsModbus;
                    master.WriteMultipleRegisters(slaveAddress, startAddress, registers);
                }
                catch
                {
                    ++WriteErrors;
                }
            }
        }
    }
}