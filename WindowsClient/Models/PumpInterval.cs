namespace IBrewery.Client.Models
{
    /// <summary>
    /// Beschreibt ein Pumpintervall, welches beim Brauen genutzt werden kann.
    /// </summary>
    public class PumpInterval
    {
        /// <summary>
        /// Eindeutige ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Bezeichner des Intervalls
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Laufzeit der Pumpe
        /// </summary>
        public int RunningTimeInSeconds { get; set; }

        /// <summary>
        /// Zeit zwischen den Pumplaufzeiten
        /// </summary>
        public int RestTimeInSeconds { get; set; }

        /// <summary>
        /// Ausgabe durch ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {           
            if (RunningTimeInSeconds == 0)
            {
                return "aus";
            }
            else if(RestTimeInSeconds == 0 || RunningTimeInSeconds> RestTimeInSeconds)
            {
                return "ein";
            }
            else
            {
                if (Name != null)
                {
                    return Name;
                }

                return $"alle {RestTimeInSeconds}Sek für {RunningTimeInSeconds}Sek";
            }
        }

        public string ToolTip
        {
            get
            {
                return $"alle {RestTimeInSeconds}Sek für {RunningTimeInSeconds}Sek";
            }
        }
    }
}
