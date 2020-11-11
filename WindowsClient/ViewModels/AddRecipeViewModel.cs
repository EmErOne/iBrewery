using IBrewery.Client.Commands;
using IBrewery.Client.DataBase;
using IBrewery.Client.Models;
using IBrewery.Client.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace IBrewery.Client.ViewModels
{
    public class AddRecipeViewModel : ViewModelBase
    {
        private readonly int recipeId = 0;

        private string recipeName;

        public string RecipeName
        {
            get { return recipeName; }
            set
            {
                recipeName = value;
                OnPropertyChanged();
            }
        }

        private string recipeDescription;

        public string RecipeDescription
        {
            get { return recipeDescription; }
            set
            {
                recipeDescription = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<Phase> recipePhases = new ObservableCollection<Phase>();
        public ObservableCollection<Phase> RecipePhases
        {
            get { return recipePhases; }
            set
            {
                recipePhases = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Ingredient> recipeIngredients = new ObservableCollection<Ingredient>();
        public ObservableCollection<Ingredient> RecipeIngredients
        {
            get { return recipeIngredients; }
            set
            {
                recipeIngredients = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PumpInterval> pumpIntervals = new ObservableCollection<PumpInterval>();
        public ObservableCollection<PumpInterval> PumpIntervals
        {
            get { return pumpIntervals; }
            set
            {
                pumpIntervals = value;
                OnPropertyChanged();
            }
        }

        #region Def Phase - Commands
        public ICommand AddPhaseCommand { get; }
        public ICommand DuplicatePhaseCommand { get; }
        public ICommand DeletePhaseCommand { get; }
        public ICommand PositionUpPhaseCommand { get; }
        public ICommand PositionDownPhaseCommand { get; }
        public ICommand AddPumpIntervalCommand { get; }
        #endregion

        #region Def Ingredient - Commands
        public ICommand AddIngredientCommand { get; }
        public ICommand DuplicateIngredientCommand { get; }
        public ICommand DeleteIngredientCommand { get; }
        #endregion

        public ICommand SaveRecipeCommand { get; }

        public AddRecipeViewModel(int recipeID)
        {
            #region Init Commands
            #region Init Phase - Commands
            AddPhaseCommand = new RelayCommand(AddPhaseExecute);
            DuplicatePhaseCommand = new RelayCommand(DuplicatePhaseExecute);
            DeletePhaseCommand = new RelayCommand(DeletePhaseExecute);
            PositionUpPhaseCommand = new RelayCommand(PositionUpPhaseExecute, PositionUpPhaseCanExecute);
            PositionDownPhaseCommand = new RelayCommand(PositionDownPhaseExecute, PositionDownPhaseCanExecute);
            AddPumpIntervalCommand = new RelayCommand(AddPumpIntervalExecute);
            #endregion

            #region Init Ingredient - Commands
            AddIngredientCommand = new RelayCommand(AddIngredientExecute);
            DuplicateIngredientCommand = new RelayCommand(DuplicateIngredientExecute);
            DeleteIngredientCommand = new RelayCommand(DeleteIngredientExecute);
            #endregion

            SaveRecipeCommand = new RelayCommand(SaveRecipeExecute, SaveRecipeCanExecute);
            #endregion

            using var db = new BreweryContext();
            PumpIntervals = new ObservableCollection<PumpInterval>(db.PumpIntervals.ToList());

            var recipe = db.Recipes.FirstOrDefault(r => r.ID == recipeID);

            if (recipe != null)
            {
                RecipeName = recipe.Name;
                RecipeDescription = recipe.Description;
                recipeId = recipe.ID;

                List<Phase> phases = new List<Phase>();
                foreach (var phase in db.Phases)
                {
                    if (phase.RecipeID == recipeID)
                    {
                        phases.Add(phase);
                    }
                }

                List<Ingredient> ingredients = new List<Ingredient>();
                foreach (var ingredient in db.Ingredients)
                {
                    if (ingredient.RecipeID == recipeID)
                    {
                        ingredients.Add(ingredient);
                    }
                }

                RecipeIngredients = new ObservableCollection<Ingredient>(ingredients);
                RecipePhases = new ObservableCollection<Phase>(phases.OrderBy(p => p.Position));
            }
        }

        public AddRecipeViewModel()
        {
            #region Init Commands
            #region Init Phase - Commands
            AddPhaseCommand = new RelayCommand(AddPhaseExecute);
            DuplicatePhaseCommand = new RelayCommand(DuplicatePhaseExecute);
            DeletePhaseCommand = new RelayCommand(DeletePhaseExecute);
            PositionUpPhaseCommand = new RelayCommand(PositionUpPhaseExecute, PositionUpPhaseCanExecute);
            PositionDownPhaseCommand = new RelayCommand(PositionDownPhaseExecute, PositionDownPhaseCanExecute);
            AddPumpIntervalCommand = new RelayCommand(AddPumpIntervalExecute);
            #endregion

            #region Init Ingredient - Commands
            AddIngredientCommand = new RelayCommand(AddIngredientExecute);
            DuplicateIngredientCommand = new RelayCommand(DuplicateIngredientExecute);
            DeleteIngredientCommand = new RelayCommand(DeleteIngredientExecute);
            #endregion

            SaveRecipeCommand = new RelayCommand(SaveRecipeExecute, SaveRecipeCanExecute);
            #endregion

            using var db = new BreweryContext();
            PumpIntervals = new ObservableCollection<PumpInterval>(db.PumpIntervals.ToList());

            foreach (var ingredient in db.FoundationIngrediets.ToList())
            {
                RecipeIngredients.Add(new Ingredient { Name = ingredient.Name, Amount = ingredient.Amount, Typ = ingredient.Typ });
            }

            RecipePhases.Add(new Phase { Position = 0, Name = "Aufwärmen", PeriodOfMinutes = 60, TargetTemperature = 50, Typ = PhaseTyp.HeatUp, PumpInterval = PumpIntervals[0] });
            RecipePhases.Add(new Phase { Position = 1, Name = "Halten", PeriodOfMinutes = 60, TargetTemperature = 50, Typ = PhaseTyp.KeepHeat, PumpInterval = PumpIntervals[2] });
            RecipePhases.Add(new Phase { Position = 2, Name = "Aufwärmen 2", PeriodOfMinutes = 60, TargetTemperature = 70, Typ = PhaseTyp.HeatUp, PumpInterval = PumpIntervals[2] });
            RecipePhases.Add(new Phase { Position = 3, Name = "Rast 2", PeriodOfMinutes = 60, TargetTemperature = 70, Typ = PhaseTyp.KeepHeat, PumpInterval = PumpIntervals[2] });
        }

        private void SaveRecipeExecute(object obj)
        {
            using var db = new BreweryContext();
            //Rezept neu

            if (recipeId == 0)
            {
                foreach (var phase in RecipePhases)
                {
                    var pumpIntervall = db.PumpIntervals.FirstOrDefault(p => p.ID == phase.PumpInterval.ID);
                    if (pumpIntervall != null)
                    {
                        phase.PumpInterval = pumpIntervall;
                        phase.PumpIntervalID = pumpIntervall.ID;
                    }
                }

                Recipe recipe = new Recipe()
                {
                    Description = RecipeDescription,
                    Name = RecipeName,
                    Ingredients = new List<Ingredient>(RecipeIngredients),
                    Phases = new List<Phase>(RecipePhases),
                    IsFavorite = false
                };

                db.Recipes.Add(recipe);
                db.SaveChanges();
            }
            else
            {
                //Rezept schon vorhanden

                var recipe = db.Recipes.FirstOrDefault(r => r.ID == recipeId);

                if (recipe != null)
                {
                    foreach (var phase in RecipePhases)
                    {
                        var pumpIntervall = db.PumpIntervals.FirstOrDefault(p => p.ID == phase.PumpInterval.ID);
                        if (pumpIntervall != null)
                        {
                            phase.PumpInterval = pumpIntervall;
                            phase.PumpIntervalID = pumpIntervall.ID;
                        }
                    }

                    recipe.Description = RecipeDescription;
                    recipe.Name = RecipeName;
                    recipe.Ingredients = new List<Ingredient>(RecipeIngredients);
                    recipe.Phases = new List<Phase>(RecipePhases);
                    recipe.IsFavorite = false;

                    db.Entry(recipe).State = EntityState.Modified;
                    db.SaveChanges();

                    //Löschen der nicht mehr genutzten Ingredients in der DB
                    var dbRecipeIngredients = db.Ingredients.Where(i => i.RecipeID == recipe.ID);
                    foreach (var dbRecipeIngredient in dbRecipeIngredients)
                    {
                        var item = RecipeIngredients.FirstOrDefault(i => i.ID == dbRecipeIngredient.ID);
                        if (item == null)
                        {
                            db.Ingredients.Remove(dbRecipeIngredient);
                            db.SaveChanges();
                        }
                    }

                    //Löschen der nicht mehr genutzten Phases in der DB
                    var dbRecipePhases = db.Phases.Where(i => i.RecipeID == recipe.ID);
                    foreach (var dbRecipePhase in dbRecipePhases)
                    {
                        var item = RecipePhases.FirstOrDefault(i => i.ID == dbRecipePhase.ID);
                        if (item == null)
                        {
                            db.Phases.Remove(dbRecipePhase);
                            db.SaveChanges();
                        }
                    }
                }
            }
        }


        private bool SaveRecipeCanExecute(object obj)
        {
            bool output = false;
            if (RecipeName?.Length > 0)
            {
                output = true;
            }

            return output;
        }

        private void AddPumpIntervalExecute(object obj)
        {
            AddPumpInteralView addPumpInteral = new AddPumpInteralView();
            addPumpInteral.ShowDialog();

            RecipePhases[0].Name = "Affe";
            OnPropertyChanged(nameof(RecipePhases));


            using var db = new BreweryContext();
            ObservableCollection<PumpInterval> pump = new ObservableCollection<PumpInterval>(db.PumpIntervals.ToList());

            if (PumpIntervals.Count != pump.Count)
            {
                var last = pump.Last();
                PumpIntervals.Add(last);

                if (obj is Phase phase)
                {
                    phase.PumpInterval = last;
                }
            }
        }

        private void PositionDownPhaseExecute(object obj)
        {
            if (obj is Phase phase)
            {
                var downerPhase = RecipePhases.ElementAt(phase.Position - 1);

                downerPhase.Position = phase.Position;
                phase.Position -= 1;

                RecipePhases = new ObservableCollection<Phase>(RecipePhases.OrderBy(_ => _.Position));
            }
        }

        private bool PositionDownPhaseCanExecute(object obj)
        {
            if (obj is Phase phase)
            {
                return phase.Position != 0;
            }

            return false;
        }

        private void PositionUpPhaseExecute(object obj)
        {
            if (obj is Phase phase)
            {
                var upperPhase = RecipePhases.ElementAt(phase.Position + 1);

                upperPhase.Position = phase.Position;
                phase.Position += 1;

                RecipePhases = new ObservableCollection<Phase>(RecipePhases.OrderBy(_ => _.Position));
            }
        }
        private bool PositionUpPhaseCanExecute(object obj)
        {
            if (obj is Phase phase)
            {
                return phase.Position != RecipePhases.Count - 1;
            }

            return false;
        }

        private void DeletePhaseExecute(object obj)
        {
            if (obj is Phase phase)
            {
                RecipePhases.Remove(phase);
            }
        }

        private void DuplicatePhaseExecute(object obj)
        {
            if (obj is Phase phase)
            {
                AddPhaseExecute(phase);
            }
        }

        private void AddPhaseExecute(object obj)
        {
            if (obj is Phase phase)
            {
                Phase newPhase = new Phase { Position = RecipePhases.Count, Name = phase.Name, PeriodOfMinutes = phase.PeriodOfMinutes, TargetTemperature = phase.TargetTemperature, Typ = phase.Typ, PumpInterval = phase.PumpInterval };

                RecipePhases.Add(newPhase);
            }
            else
            {
                int targetTemperature = 0;
                if (RecipePhases.Count > 0)
                {
                    targetTemperature = RecipePhases[RecipePhases.Count - 1].TargetTemperature;
                }
                RecipePhases.Add(new Phase { Position = RecipePhases.Count, Name = "", PeriodOfMinutes = 0, TargetTemperature = targetTemperature, Typ = PhaseTyp.KeepHeat, PumpInterval = PumpIntervals[0] });
            }
        }

        private void AddIngredientExecute(object obj)
        {

            if (obj is Ingredient ingredient)
            {
                RecipeIngredients.Add(new Ingredient { Name = ingredient.Name, Amount = ingredient.Amount, Typ = ingredient.Typ });
            }
            else
            {
                RecipeIngredients.Add(new Ingredient { Name = "", Amount = 0, Typ = IngredientTyp.Kilogram });
            }
        }

        private void DeleteIngredientExecute(object obj)
        {
            if (obj is Ingredient ingredient)
            {
                RecipeIngredients.Remove(ingredient);
            }
        }

        private void DuplicateIngredientExecute(object obj)
        {
            if (obj is Ingredient ingredient)
            {
                AddIngredientExecute(ingredient);
            }
        }
    }
}
