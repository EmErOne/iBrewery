namespace IBrewery.Client.Models
{
    public interface IArduinoConnection
    {
        int GetTemperature();

        #region Pump Commands
        void TurnPumpOn();
        void TurnPumpOff();
        #endregion

        #region Heater Commands
        void TurnHeaterOn();
        void TurnHeaterOff();
        #endregion
    }
}
