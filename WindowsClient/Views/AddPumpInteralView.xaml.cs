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
using System.Windows.Shapes;

namespace IBrewery.Client.Views
{
    /// <summary>
    /// Interaktionslogik für AddPumpInteralView.xaml
    /// </summary>
    public partial class AddPumpInteralView : Window
    {
        public AddPumpInteralView()
        {
            InitializeComponent();

            AddPumpInteralViewModel dataContext = new AddPumpInteralViewModel();
            this.DataContext = dataContext;
            if (dataContext.CloseAction == null)
                dataContext.CloseAction = new Action(this.Close);
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
    }
}
