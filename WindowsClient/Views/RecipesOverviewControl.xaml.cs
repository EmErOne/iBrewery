using IBrewery.Client.Models;
using IBrewery.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaktionslogik für RecipesOverviewControl.xaml
    /// </summary>
    public partial class RecipesOverviewControl : UserControl
    {
        private readonly IMainWindow mainWindow;

        public RecipesOverviewControl(IMainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.DataContext = new OverviewViewModel(mainWindow);
        }

        private void Recipes_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            if (ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) is ListBoxItem item && item.Content is OverviewModel model)
            {
                mainWindow.ChangeContentControl(new AddRecipeControl(model.RecipeID, mainWindow));
            }
        }
               
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(sender is MenuItem item && item.DataContext is OverviewModel model)
            {
               if(DataContext is OverviewViewModel ov)
                {
                    ov.DeleteCommand.Execute(model);
                }
            }    
        }

        private void Duplicate_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem item && item.DataContext is OverviewModel model)
            {
                if (DataContext is OverviewViewModel ov)
                {
                    ov.DuplicateCommand.Execute(model);
                }
            }
        }

        private void Brew_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button item && item.DataContext is OverviewModel model)
            {
                if (DataContext is OverviewViewModel ov)
                {
                    ov.BrewCommand.Execute(model);
                }
            }
        }
    }
}
