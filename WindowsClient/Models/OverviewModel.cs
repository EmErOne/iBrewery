using System;
using System.Collections.Generic;
using System.Text;

namespace IBrewery.Client.Models
{
    public class OverviewModel : IRankingModel
    {
        public int RecipeID { get; set; }
        public string RecipeName { get; set; }
        public int Ranking { get; set; }        
    }
}
