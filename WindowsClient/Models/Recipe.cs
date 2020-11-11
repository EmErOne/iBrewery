using System.Collections.Generic;

namespace IBrewery.Client.Models
{
    /// <summary>
    /// Beschreibt ein Rezept
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// Eindeutige ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name des Rezepts
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Beschreibung des Rezepts
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gibt an ob das Rezept ein Favorit ist
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// Liste der Zutaten
        /// </summary>
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        /// <summary>
        /// Liste der Phasen
        /// </summary>
        public List<Phase> Phases { get; set; } = new List<Phase>();
    }
}
