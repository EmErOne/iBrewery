using IBrewery.Client.Views;

namespace IBrewery.Client.Models
{
    /// <summary>
    /// Beschreibt ein Kriterium und vergibt Sterne
    /// zb Geschmack 3 von 5 Sternen
    /// </summary>
    public class Criteria : IRankingModel
    {
        /// <summary>
        /// Eindeutige ID der Auswertung
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Beschreibung der Auswertung
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Beschreibt den Rang 3 von 5 Sternen
        /// </summary>
        public int Ranking { get; set; }

        /// <summary>
        /// Ein Kommentar zur Bewertung
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// AuswertungID eines Rezeptes
        /// </summary>
        public int EvaluationID { get; set; }

        /// <summary>
        /// Auswertung eines Rezeptes
        /// </summary>
        public Evaluation Evaluation { get; set; }
    }
}
