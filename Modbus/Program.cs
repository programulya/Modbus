using System;
using System.Configuration;
using System.IO.Ports;

namespace Modbus
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                // Create connection using port parameters
                var connection = new Connection
                    {
                        Port = new SerialPort
                            {
                                PortName = ConfigurationSettings.AppSettings["PortName"],
                                BaudRate = Convert.ToInt32(ConfigurationSettings.AppSettings["BaudRate"]),
                                DataBits = Convert.ToInt32(ConfigurationSettings.AppSettings["DataBits"]),
                                ReadTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["ReadTimeout"]),
                                WriteTimeout = Convert.ToInt32(ConfigurationSettings.AppSettings["WriteTimeout"]),
                                Parity = Parity.None,
                                StopBits = StopBits.One
                            }
                    };

                var slaveAddress = Convert.ToByte(ConfigurationSettings.AppSettings["SlaveAddress"]);

                try
                {
                    // Open connection
                    connection.OpenPort();

                    // Write into registers
                    Modbus.WriteRegisters(connection.Port, slaveAddress, (ushort) ModbusMap.ADR_STAT, 1);
                    Modbus.WriteRegisters(connection.Port, slaveAddress, (ushort) ModbusMap.ADR_ADC_STATUS, 1);
                    Console.WriteLine(Modbus.WriteErrors);

                    // Read multiply registers
                    var registers = Modbus.ReadRegisters(connection.Port, slaveAddress, (ushort) ModbusMap.ADR_STAT, 2);

                    // Check results and do with registers what you want
                    Console.WriteLine(registers);
                    Console.WriteLine(Modbus.ReadErrors);
                }
                finally
                {
                    connection.ClosePort();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}