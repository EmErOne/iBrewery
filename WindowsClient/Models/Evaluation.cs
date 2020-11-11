using IBrewery.Client.DataBase;
using System.Collections.Generic;
using System.Linq;

namespace IBrewery.Client.Models
{
    /// <summary>
    /// Beschreibt eine Auswertung eines Rezeptes
    /// </summary>
    public class Evaluation
    {
        /// <summary>
        /// Eindeutige ID der Auswertung
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Beschreibung der Auswertung
        /// </summary>
        public string Assessment { get; set; }

        /// <summary>
        /// Gibt an für welches RezeptID diese Zutat verwändet wird
        /// </summary>
        public int RecipeID { get; set; }

        /// <summary>
        /// Zeitpunkt der Erstellung
        /// </summary>
        public System.DateTime CreationTime { get; set; }

        /// <summary>
        /// Gibt an für welches RezeptID diese Zutat verwändet wird
        /// </summary>
        public Recipe Recipe { get; set; }

        /// <summary>
        /// Liste von Kriterien
        /// </summary>
        public List<Criteria> Criterias { get; set; } = new List<Criteria>();

        /// <summary>
        /// Gibt den Durchschnittswert aller Kriterien zurück
        /// </summary>
        /// <returns></returns>
        public int GetAverage()
        {
            int output = 0;
            int sum = 0;

            using (var db = new BreweryContext())
            {
                Criterias = db.Criterias.Where(c => c.EvaluationID == ID).ToList();           
            }            

            if (Criterias.Count > 0)
            {
                foreach (var criteria in Criterias)
                {
                    sum += criteria.Ranking;
                }

                output = sum / Criterias.Count;
            }

            return output;
        }
    }
}
