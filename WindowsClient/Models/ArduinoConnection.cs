using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace IBrewery.Client.Models
{
    public class ArduinoConnection : IArduinoConnection
    {
        public int GetTemperature()
        {
            Trace.WriteLine("GetTemperature");
            return 80;
        }

        public void TurnHeaterOff()
        {
            Trace.WriteLine("TurnHeaterOff");
        }

        public void TurnHeaterOn()
        {
            Trace.WriteLine("TurnHeaterOn");
        }

        public void TurnPumpOff()
        {
            Trace.WriteLine("TurnPumpOff");
        }

        public void TurnPumpOn()
        {
            Trace.WriteLine("TurnPumpOn");
        }
    }
}
