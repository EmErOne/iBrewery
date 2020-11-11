using System;
using System.Windows.Controls;

namespace IBrewery.Client.Views
{
    /// <summary>
    /// Interaktionslogik für DashboardControl.xaml
    /// </summary>
    public partial class DashboardControl : UserControl
    {    
        public DashboardControl()
        {
            InitializeComponent();
            ContentArea.Height = 40;
            ContentArea.Content = new RatingControl(3);
        }       
    }
}
