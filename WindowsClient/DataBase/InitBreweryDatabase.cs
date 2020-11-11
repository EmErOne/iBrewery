using IBrewery.Client.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IBrewery.Client.DataBase
{
    public static class InitBreweryDatabase
    {
        public static void Init()
        {
            using BreweryContext breweryContext = new BreweryContext();

            #region Recipe 
            List<PumpInterval> pumpIntervals = new List<PumpInterval>();
            List<Ingredient> ingredients = new List<Ingredient>();
            List<Phase> phases = new List<Phase>();

            pumpIntervals.Add(new PumpInterval { Name = "AUS", RestTimeInSeconds = 0, RunningTimeInSeconds = 0 });
            pumpIntervals.Add(new PumpInterval { Name = "Langsam", RestTimeInSeconds = 60, RunningTimeInSeconds = 10 });
            pumpIntervals.Add(new PumpInterval { Name = "Schnell", RestTimeInSeconds = 120, RunningTimeInSeconds = 20 });

            ingredients.Add(new Ingredient { Name = "Gerste", Amount = 10, Typ = IngredientTyp.Kilogram });
            ingredients.Add(new Ingredient { Name = "Hopfen", Amount = 0.7, Typ = IngredientTyp.Kilogram });
            ingredients.Add(new Ingredient { Name = "Wasser", Amount = 86, Typ = IngredientTyp.Liter });

            phases.Add(new Phase { Position = 0, Name = "Aufwärmen", PeriodOfMinutes = 60, TargetTemperature = 50, Typ = PhaseTyp.HeatUp, PumpInterval = pumpIntervals[0] });
            phases.Add(new Phase { Position = 1, Name = "Halten", PeriodOfMinutes = 60, TargetTemperature = 50, Typ = PhaseTyp.KeepHeat, PumpInterval = pumpIntervals[2] });
            phases.Add(new Phase { Position = 2, Name = "Aufwärmen 2", PeriodOfMinutes = 60, TargetTemperature = 70, Typ = PhaseTyp.HeatUp, PumpInterval = pumpIntervals[2] });
            phases.Add(new Phase { Position = 3, Name = "Rast 2", PeriodOfMinutes = 60, TargetTemperature = 70, Typ = PhaseTyp.KeepHeat, PumpInterval = pumpIntervals[2] });


            Recipe recipe1 = new Recipe()
            {
                ID = 0,
                Description = "Das ist ein seher seher leckeres Bier welches hier gebraut werden soll.",
                Name = "Keller Pilz",
                Ingredients = ingredients,
                Phases = phases,
                IsFavorite = false
            };

            breweryContext.Recipes.Add(recipe1);
            breweryContext.SaveChanges();

            phases.Add(new Phase { Position = 4, Name = "Aufwärmen 3", PeriodOfMinutes = 60, TargetTemperature = 100, Typ = PhaseTyp.HeatUp, PumpInterval = pumpIntervals[2] });
            phases.Add(new Phase { Position = 5, Name = "Kochen", PeriodOfMinutes = 160, TargetTemperature = 100, Typ = PhaseTyp.KeepHeat, PumpInterval = pumpIntervals[2] });
            ingredients.Add(new Ingredient { Name = "Hefe", Amount = 0.08, Typ = IngredientTyp.Kilogram });

            Recipe recipe2 = new Recipe()
            {
                ID = 0,
                Description = "Das ist ein seher seher leckeres Helles welches hier auf dem Dach gebraut wird.",
                Name = "Dach Helles",
                Ingredients = ingredients,
                Phases = phases,
                IsFavorite = false
            };

            breweryContext.Recipes.Add(recipe2);
            breweryContext.SaveChanges();
            #endregion

            #region Settings
            #region FoundationIngrediet
            List<FoundationIngrediet> foundationIngrediets = new List<FoundationIngrediet>
                {
                    new FoundationIngrediet { Name = "Gerste", Amount = 40, Typ = IngredientTyp.Kilogram },
                    new FoundationIngrediet { Name = "Malz", Amount = 41, Typ = IngredientTyp.Kilogram },
                    new FoundationIngrediet { Name = "Wasser", Amount = 70, Typ = IngredientTyp.Liter },
                    new FoundationIngrediet { Name = "Hopfen", Amount = 1.5, Typ = IngredientTyp.Kilogram },
                    new FoundationIngrediet { Name = "Hefe", Amount = 0.7, Typ = IngredientTyp.Kilogram }
                };

            breweryContext.FoundationIngrediets.AddRange(foundationIngrediets);
            breweryContext.SaveChanges();
            #endregion

            #region FoundationCriteria
            List<FoundationCriteria> foundationCriterias = new List<FoundationCriteria>
                {
                    new FoundationCriteria { Description = "Geschmack" },
                    new FoundationCriteria { Description = "Farbe" },
                    new FoundationCriteria { Description = "Geruch" },
                    new FoundationCriteria { Description = "Schaum" }
                };

            breweryContext.FoundationCriterias.AddRange(foundationCriterias);
            breweryContext.SaveChanges();
            #endregion

            #region PumpInterval
            List<PumpInterval> pumpIntervals1 = new List<PumpInterval>
                {
                    new PumpInterval { RestTimeInSeconds = 160, RunningTimeInSeconds = 20, Name = "Normal" },
                    new PumpInterval { RestTimeInSeconds = 300, RunningTimeInSeconds = 120, Name = "Langsam" },
                    new PumpInterval { RestTimeInSeconds = 60, RunningTimeInSeconds = 10, Name = "Schnell" }
                };

            breweryContext.PumpIntervals.AddRange(pumpIntervals1);
            breweryContext.SaveChanges();
            #endregion

            #region EMailAddress
            List<EMailAddress> eMailAddresses = new List<EMailAddress>
                {
                    new EMailAddress { Address = "me@web.de" },
                    new EMailAddress { Address = "me2me@web.de" }
                };

            breweryContext.EMailAddresses.AddRange(eMailAddresses);
            breweryContext.SaveChanges();
            #endregion
            #endregion


            #region Evaluations
            Recipe recipe = breweryContext.Recipes.First();
            List<Criteria> criterias = new List<Criteria>
                {
                    new Criteria { Description = "Geschmack", Comment = "Passt", Ranking = 5 },
                    new Criteria { Description = "Farbe", Comment = "Passt", Ranking = 4 },
                    new Criteria { Description = "Geruch", Comment = "Passt nicht", Ranking = 1 },
                    new Criteria { Description = "Schaum", Comment = "ekelhaft", Ranking = 2 }
                };

            Evaluation evaluation = new Evaluation
            {
                CreationTime = DateTime.Now,
                Assessment = "Schmeckt super",
                Criterias = criterias,
                Recipe = recipe,
                RecipeID = recipe.ID
            };

            breweryContext.Evaluations.Add(evaluation);
            breweryContext.SaveChanges();
            #endregion

            #region BrewProcesses
            recipe = breweryContext.Recipes.First();
            BrewProcess brewProcess = new BrewProcess
            {
                IsCompleted = true,
                Time = DateTime.Now,
                Recipe = recipe,
                RecipeID = recipe.ID
            };
            breweryContext.BrewProcesses.Add(brewProcess);
            breweryContext.SaveChanges();
            #endregion
        }
    }
}
