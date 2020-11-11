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
    /// Interaktionslogik für BrewingControl.xaml
    /// </summary>
    public partial class BrewingControl : UserControl, IDisposable
    {

        public bool IsRunnig { get; set; } = false;

        public BrewingControl()
        {
            InitializeComponent();
            BrewingViewModel dataContext = new BrewingViewModel();
            DataContext = dataContext;
        }

        public BrewingControl(Models.Recipe recipe)
        {
            InitializeComponent();
            BrewingViewModel dataContext = new BrewingViewModel(recipe);
            DataContext = dataContext;
        }      


        private void TurnPumpON(object sender, RoutedEventArgs e)
        {
            PumpSwitch.Content = "Pumpe an";
        }

        private void TurnHeatOFF(object sender, RoutedEventArgs e)
        {
            HeatSwitch.Content = "Heizung auto";
        }

        private void TurnHeatON(object sender, RoutedEventArgs e)
        {
            HeatSwitch.Content = "Heizung an";
        }

        private void TurnPumpOFF(object sender, RoutedEventArgs e)
        {
            PumpSwitch.Content = "Pumpe auto";
        }

        public void Dispose()
        {
            if(DataContext is BrewingViewModel viewModel)
            {
                viewModel.Dispose();
            }            
        }
    }
}
