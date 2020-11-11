using System;
using System.Collections.Generic;
using System.Text;

namespace IBrewery.Client.Models
{
    /// <summary>
    /// Beschreibt ein Basiskriterium
    /// </summary>
    public class FoundationCriteria
    {
        /// <summary>
        /// Eindeutige ID der Auswertung
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Beschreibung der Auswertung
        /// </summary>
        public string Description { get; set; }
    }
}
