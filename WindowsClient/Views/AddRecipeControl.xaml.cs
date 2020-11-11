using IBrewery.Client.Models;
using IBrewery.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaktionslogik für AddRecipeControl.xaml
    /// </summary>
    public partial class AddRecipeControl : UserControl
    {
        public IMainWindow mainWindow;

        public AddRecipeControl(IMainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = new AddRecipeViewModel();
            this.mainWindow = mainWindow;
        }

        public AddRecipeControl(int recipeID, IMainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = new AddRecipeViewModel(recipeID);
            this.mainWindow = mainWindow;
        }

        private void PreviewTextInput_NumberTextBoxInputCheck(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static readonly Regex _regex = new Regex("[^0-9]"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.ChangeContentControl(new RecipesOverviewControl(mainWindow));
        }
    }
}
