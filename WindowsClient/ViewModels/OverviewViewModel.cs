using IBrewery.Client.Commands;
using IBrewery.Client.DataBase;
using IBrewery.Client.Models;
using IBrewery.Client.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace IBrewery.Client.ViewModels
{
    public class OverviewViewModel : ViewModelBase
    {
        public ObservableCollection<OverviewModel> recipesOverviewModels;
        private readonly IMainWindow mainWindow;

        public ObservableCollection<OverviewModel> RecipesOverviewModels
        {
            get { return recipesOverviewModels; }
            set
            {
                recipesOverviewModels = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand { get; }
        public ICommand DuplicateCommand { get; }
        public ICommand BrewCommand { get; }

        public OverviewViewModel()
        {
            DeleteCommand = new RelayCommand(DeleteExecute);
            DuplicateCommand = new RelayCommand(DublicateExecute);
            BrewCommand = new RelayCommand(BrewExecute);
            InitModel();
        }

        public OverviewViewModel(IMainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            DeleteCommand = new RelayCommand(DeleteExecute);
            DuplicateCommand = new RelayCommand(DublicateExecute);
            BrewCommand = new RelayCommand(BrewExecute);
            InitModel();
        }

        private void InitModel()
        {
            List<Recipe> recipes = new List<Recipe>();
            List<Evaluation> evaluations = new List<Evaluation>();

            using (var db = new BreweryContext())
            {
                recipes = db.Recipes.ToList();
                evaluations = db.Evaluations.ToList();
            }

            RecipesOverviewModels = new ObservableCollection<OverviewModel>();

            foreach (var recipe in recipes)
            {
                Evaluation evaluation = evaluations.FirstOrDefault(e => e.RecipeID == recipe.ID);
                int ranking = -1;

                if (evaluation != null)
                {
                    ranking = evaluation.GetAverage();
                }

                RecipesOverviewModels.Add(new OverviewModel { RecipeName = recipe.Name, RecipeID = recipe.ID, Ranking = ranking });
            }

            RecipesOverviewModels = new ObservableCollection<OverviewModel>(RecipesOverviewModels.OrderByDescending(n => n.Ranking));
        }

        private void DeleteExecute(object obj)
        {
            if (obj is OverviewModel overview)
            {
                using var db = new BreweryContext();
                var recipe = db.Recipes.FirstOrDefault(r => r.ID == overview.RecipeID);
                if (recipe != null)
                {
                    db.Entry(recipe).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    db.Recipes.Remove(recipe);
                    db.SaveChanges();
                    InitModel();
                }
            }
        }

        private void DublicateExecute(object obj)
        {
            if (obj is OverviewModel overview)
            {
                using var db = new BreweryContext();
                var dbRecipe = db.Recipes.FirstOrDefault(r => r.ID == overview.RecipeID);
                if (dbRecipe != null)
                {
                    List<Ingredient> ingredients = new List<Ingredient>();

                    var inList = db.Ingredients.Where(i => i.RecipeID == dbRecipe.ID);
                    foreach (var ingr in inList)
                    {
                        ingredients.Add(new Ingredient
                        {
                            Name = ingr.Name,
                            Amount = ingr.Amount,
                            Typ = ingr.Typ
                        });
                    }

                    List<Phase> phases = new List<Phase>();
                    var phList = db.Phases.Where(p => p.RecipeID == dbRecipe.ID);
                    foreach (var phase in phList)
                    {
                        phases.Add(new Phase
                        {
                            Name = phase.Name,
                            Typ = phase.Typ,
                            PeriodOfMinutes = phase.PeriodOfMinutes,
                            Position = phase.Position,
                            PumpInterval = phase.PumpInterval,
                            PumpIntervalID = phase.PumpIntervalID,
                            TargetTemperature = phase.TargetTemperature
                        });
                    }

                    Recipe recipe = new Recipe
                    {
                        Name = dbRecipe.Name,
                        Description = dbRecipe.Description,
                        IsFavorite = dbRecipe.IsFavorite,
                        Ingredients = ingredients,
                        Phases = phases
                    };

                    db.Recipes.Add(recipe);
                    db.SaveChanges();
                    InitModel();
                }
            }
        }

        private void BrewExecute(object obj)
        {
            if (obj is OverviewModel overview)
            {
                using var db = new BreweryContext();
                var recipe = db.Recipes.FirstOrDefault(r => r.ID == overview.RecipeID);
                if(recipe != null)
                {
                    mainWindow.LoadBrewingControl(recipe);
                }
            }
        }
    }
}

