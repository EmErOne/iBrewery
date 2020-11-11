using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using IBrewery.Client.Models;

namespace IBrewery.Client.DataBase
{
    public class BreweryContext : DbContext
    {
        /// <summary>
        /// Prozesse
        /// </summary>
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<BrewProcess> BrewProcesses { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<Criteria> Criterias { get; set; }

        /// <summary>
        /// Settings
        /// </summary>
        public DbSet<FoundationCriteria> FoundationCriterias { get; set; }
        public DbSet<FoundationIngrediet> FoundationIngrediets { get; set; }
        public DbSet<EMailAddress> EMailAddresses { get; set; }
        public DbSet<PumpInterval> PumpIntervals { get; set; }       


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=iBrewery.db");
    }
}
