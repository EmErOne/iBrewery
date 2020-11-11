using System;
using System.Collections.Generic;
using System.Text;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace IBrewery.Client.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public DashboardViewModel()
        {          
            DrawDuration();
            DrawIngredientsChart();
        }


        #region IngredientsChart - Bestandteile
        public Func<ChartPoint, string> IngredientsChart_PointLabel { get; set; }
        public SeriesCollection IngredientsCollection { get; set; }
        private void DrawIngredientsChart()
        {
            IngredientsChart_PointLabel = chartPoint => string.Format("{0}", chartPoint.SeriesView.Title);

            IngredientsCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Wasser",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(8) },
                    DataLabels = true,

                    LabelPoint=IngredientsChart_PointLabel
                },
                new PieSeries
                {
                    Title = "Gerste",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(4) },
                    DataLabels = true,
                    LabelPoint=IngredientsChart_PointLabel
                },
                new PieSeries
                {
                    Title = "Hopfen",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(1) },
                    DataLabels = true,
                    LabelPoint=IngredientsChart_PointLabel
                },
                new PieSeries
                {
                    Title = "Hefe",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(0.11) },
                    DataLabels = true,
                    LabelPoint=IngredientsChart_PointLabel
                }
            };
        }
        #endregion

        #region Duration Daueranzeige
        public SeriesCollection DurationCollection { get; set; }
        public string[] DurationLabels { get; set; }
        public Func<double, string> DurationFormatter { get; set; }

        private void DrawDuration()
        {
            DurationCollection = new SeriesCollection
            {
                new RowSeries
                {
                    Title = "Dauer in Stunden",
                    Values = new ChartValues<double> { 8, 3, 6, 5, 3}
                }
            };

            DurationLabels = new[] { "Pilz", "Weizen", "Radler", "Fante", "Colaastaa" };
            DurationFormatter = value => value.ToString("N0");

        }
        #endregion
    }
}
