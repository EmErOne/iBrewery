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
    /// Interaktionslogik für EvaluationControl.xaml
    /// </summary>
    public partial class EvaluationControl : UserControl
    {
        private readonly IMainWindow mainWindow;

        public EvaluationControl(int recipeID, IMainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            EvaluationViewModel dataContext = new EvaluationViewModel(recipeID);
            DataContext = dataContext;
            if (dataContext.CloseAction == null)
                dataContext.CloseAction = new Action(ControlClose);
        }

        private void ControlClose()
        {
            this.DataContext = null;
            mainWindow.ChangeContentControl(new EvaluationOverviewControl(mainWindow));
        }
    }
}
