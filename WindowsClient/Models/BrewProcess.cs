using System;

namespace IBrewery.Client.Models
{
    /// <summary>
    /// Beschreibt einen Brauvorgang
    /// </summary>
    public class BrewProcess
    {
        /// <summary>
        /// Eindeutige ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Datum der Durchführung
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Wurde der Vorgang abgeschlossen
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// RezeptID des Vorgangs
        /// </summary>
        public int RecipeID { get; set; }

        /// <summary>
        /// Rezept des Vorganges
        /// </summary>
        public Recipe Recipe { get; set; }
    }
}
