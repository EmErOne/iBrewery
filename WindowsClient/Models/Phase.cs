using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IBrewery.Client.Models
{
    /// <summary>
    /// Beschreibt eine Phase zur Zubereitung 
    /// </summary>
    public class Phase : INotifyPropertyChanged
    {
        /// <summary>
        /// ID der Phase
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Name der Phase
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Position im Rezept
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Typ der Phase
        /// </summary>
        public PhaseTyp Typ { get; set; }

        /// <summary>
        /// Dauer der Phase in Minuten
        /// </summary>
        public int PeriodOfMinutes { get; set; }

        /// <summary>
        /// Zieltemperatur der Phase in °C
        /// </summary>
        public int TargetTemperature { get; set; }

        /// <summary>
        /// Pumpintervall der Phase als ID
        /// </summary>
        public int PumpIntervalID { get; set; }

        /// <summary>
        /// Pumpintervall der Phase
        /// </summary>     
        private PumpInterval pumpInterval;
        public PumpInterval PumpInterval
        {
            get { return pumpInterval; }
            set
            {
                pumpInterval = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gibt an für welches RezeptID diese Zutat verwändet wird
        /// </summary>
        public int RecipeID { get; set; }

        /// <summary>
        /// Gibt an für welches RezeptID diese Zutat verwändet wird
        /// </summary>
        public Recipe Recipe { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
