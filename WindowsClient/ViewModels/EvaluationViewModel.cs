using IBrewery.Client.Commands;
using IBrewery.Client.DataBase;
using IBrewery.Client.Models;
using IBrewery.Client.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace IBrewery.Client.ViewModels
{
    public class EvaluationViewModel : ViewModelBase
    {
        public Action CloseAction;

        private readonly int recipeId = 0;
        private Evaluation evaluation = new Evaluation();
        public Evaluation Evaluation
        {
            get { return evaluation; }
            set
            {
                evaluation = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand AbortCommand { get; }

        public EvaluationViewModel(int recipeID)
        {
            SaveCommand = new RelayCommand(SaveExecute);
            AbortCommand = new RelayCommand(AbortExecute);

            recipeId = recipeID;

            List<FoundationCriteria> foundationCriterias = new List<FoundationCriteria>();
            List<Criteria> criterias = new List<Criteria>();
            Recipe recipe = null;
            Evaluation evaluation = null;

            using (var db = new BreweryContext())
            {
                foundationCriterias = db.FoundationCriterias.ToList();
                recipe = db.Recipes.FirstOrDefault(r => r.ID == recipeId);
                evaluation = db.Evaluations.FirstOrDefault(e => e.RecipeID == recipeId);
            }

            foreach (var criteria in foundationCriterias)
            {
                criterias.Add(new Criteria
                {
                    Description = criteria.Description
                });
            }

            if (evaluation == null)
            {
                Evaluation = new Evaluation
                {
                    Criterias = criterias,
                    Recipe = recipe
                };
            }
            else
            {
                Evaluation = evaluation;

                using (var db = new BreweryContext())
                {
                    criterias = db.Criterias.Where(c => c.EvaluationID == Evaluation.ID).ToList();
                    Evaluation.Criterias = criterias;
                }
            }
        }

        private void AbortExecute(object obj)
        {
            CloseAction();
        }

        private void SaveExecute(object obj)
        {
            using (var db = new BreweryContext())
            {
                if (Evaluation.ID == 0)
                {
                    Recipe recipe = db.Recipes.FirstOrDefault(r => r.ID == recipeId);
                    Evaluation.Recipe = recipe;
                    Evaluation.RecipeID = recipe.ID;

                    db.Evaluations.Add(Evaluation);
                    db.SaveChanges();
                }
                else
                {
                    foreach(var criteria in Evaluation.Criterias)
                    {
                        db.Entry(criteria).State = EntityState.Modified;
                        db.SaveChanges();
                    }


                    db.Entry(Evaluation).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
    }
}
