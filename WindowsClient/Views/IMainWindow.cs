using IBrewery.Client.Models;
using System.Windows.Controls;

namespace IBrewery.Client.Views
{
    public interface IMainWindow
    {
        /// <summary>
        /// Weist eine neue Control an 
        /// </summary>
        /// <param name="control"></param>
        void ChangeContentControl(ContentControl control);

        /// <summary>
        /// Läd ein Rezept zum Brauen
        /// </summary>
        /// <param name="recipe"></param>
        void LoadBrewingControl(Recipe recipe);
    }
}