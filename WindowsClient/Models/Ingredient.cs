namespace IBrewery.Client.Models
{
    /// <summary>
    /// Beschreibt ein Bestandteil, Zutat
    /// </summary>
    public class Ingredient
    {
        /// <summary>
        /// Eindeutige ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Bezeichnung der Zutat
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Menge der Zutat
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Typ der Zutat (kg, l)
        /// </summary>
        public IngredientTyp Typ { get; set; }

        /// <summary>
        /// Gibt an für welches RezeptID diese Zutat verwändet wird
        /// </summary>
        public int RecipeID { get; set; }

        /// <summary>
        /// Gibt an für welches RezeptID diese Zutat verwändet wird
        /// </summary>
        public Recipe Recipe { get; set; }
    }
}
