using IBrewery.Client.DataBase;
using IBrewery.Client.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IBrewery.Client.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
         private readonly AddRecipeControl addRecipeControl;
        private BrewingControl brewingControl = new BrewingControl();

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                using (var db = new BreweryContext())
                {
                    if (db.Database.EnsureCreated())
                    {
                        InitBreweryDatabase.Init();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden der Datenbank. " + ex.Message);
            }            

            ContentArea.Content = new DashboardControl();
            addRecipeControl = new AddRecipeControl(this);
        }

        private void Dashboard_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new DashboardControl();
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = addRecipeControl;
        }

        private void EvaluationOverview_Click(object sender, RoutedEventArgs e)
        {           
            ContentArea.Content = new EvaluationOverviewControl(this);
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new SettingsControl();
        }

        private void RecipeOverview_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new RecipesOverviewControl(this);
        }

        public void ChangeContentControl(ContentControl control)
        {
            ContentArea.Content = control;
        }

        private void Brewing_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = brewingControl;
        }

        public void LoadBrewingControl(Recipe recipe)
        {
            if(brewingControl.IsRunnig)
            {
                MessageBox.Show("Sie müssen erst den laufenden Brauvorgang beenden oder abbrechen, um ein neues Rezept zu laden.", "iBrewery", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                brewingControl.Dispose();
                brewingControl = new BrewingControl(recipe);
                ContentArea.Content = brewingControl;
            }           
        }
    }
}
