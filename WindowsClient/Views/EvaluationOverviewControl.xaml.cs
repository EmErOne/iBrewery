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
    /// Interaktionslogik für EvaluationOverviewControl.xaml
    /// </summary>
    public partial class EvaluationOverviewControl : UserControl
    {
        private IMainWindow mainWindow;

        public EvaluationOverviewControl(IMainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Recipes_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null && item.Content is OverviewModel model)
            {
                mainWindow.ChangeContentControl(new EvaluationControl(model.RecipeID, mainWindow));
            }
        }
    }
}
