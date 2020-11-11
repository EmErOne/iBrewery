/// <summary>
/// Beschreiben eine Basis Zutat die der Nutzer in den Eigenschaften hinterlegen kann.
/// Diese werden dann beim Erstellen eineses Rezeptes eingefügt.
/// </summary>
namespace IBrewery.Client.Models
{
    public class FoundationIngrediet
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
    }
}
