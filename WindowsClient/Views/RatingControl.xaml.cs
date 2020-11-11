using IBrewery.Client.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace IBrewery.Client.Views
{
    /// <summary>
    /// Interaktionslogik für RatingControl.xaml
    /// </summary>
    public partial class RatingControl : UserControl
    {
        private readonly SolidColorBrush marked = new SolidColorBrush(Color.FromRgb(255, 102, 0));
        private readonly SolidColorBrush clear = new SolidColorBrush(Color.FromRgb(255, 255, 255));

        public int StarCount
        {
            get
            {
                return (int)GetValue(StarCountProperty);
            }
            set
            {
                SetValue(StarCountProperty, value);

                if (this.DataContext is Criteria criteria)
                {
                    if (criteria.Ranking != value)
                    {
                        criteria.Ranking = value;
                    }
                }
            }
        }

        // Using a DependencyProperty as the backing store for StarCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StarCountProperty =
            DependencyProperty.Register("StarCount", typeof(int), typeof(RatingControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnValueChanged)));


        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RatingControl ratingControl)
            {
                if (ratingControl.DataContext is IRankingModel ranking)
                {
                    if (ratingControl.StarCount != ranking.Ranking)
                    {
                        ranking.Ranking = ratingControl.StarCount;
                    }

                    ratingControl.UserControl_MouseLeave(null, null);
                }
            }
        }

        public RatingControl()
        {
            InitializeComponent();
        }

        public RatingControl(int selected)
        {
            InitializeComponent();
            StarCount = selected;
            UserControl_MouseLeave(null, null);
        }

        private void Clear()
        {
            Star1.Fill = clear;
            Star2.Fill = clear;
            Star3.Fill = clear;
            Star4.Fill = clear;
            Star5.Fill = clear;
        }

        private void PaintOrageNumberTill(int index)
        {
            PaintTill(index, marked);
        }

        private void PaintClearNumberTill(int index)
        {
            PaintTill(index, clear);
        }

        private void PaintTill(int index, SolidColorBrush brush)
        {
            Clear();
            switch (index)
            {
                case 1:
                    Star1.Fill = brush;
                    break;
                case 2:
                    Star1.Fill = brush;
                    Star2.Fill = brush;
                    break;
                case 3:
                    Star1.Fill = brush;
                    Star2.Fill = brush;
                    Star3.Fill = brush;
                    break;
                case 4:
                    Star1.Fill = brush;
                    Star2.Fill = brush;
                    Star3.Fill = brush;
                    Star4.Fill = brush;
                    break;
                case 5:
                    Star1.Fill = brush;
                    Star2.Fill = brush;
                    Star3.Fill = brush;
                    Star4.Fill = brush;
                    Star5.Fill = brush;
                    break;
            }
        }


        private void S1_MouseEnter(object sender, MouseEventArgs e)
        {
            PaintOrageNumberTill(1);
        }

        private void S2_MouseEnter(object sender, MouseEventArgs e)
        {
            PaintOrageNumberTill(2);
        }

        private void S3_MouseEnter(object sender, MouseEventArgs e)
        {
            PaintOrageNumberTill(3);
        }

        private void S4_MouseEnter(object sender, MouseEventArgs e)
        {
            PaintOrageNumberTill(4);
        }

        private void S5_MouseEnter(object sender, MouseEventArgs e)
        {
            PaintOrageNumberTill(5);
        }

        private void S1_MouseLeave(object sender, MouseEventArgs e)
        {
            PaintClearNumberTill(1);
        }

        private void S2_MouseLeave(object sender, MouseEventArgs e)
        {
            PaintClearNumberTill(2);
        }

        private void S3_MouseLeave(object sender, MouseEventArgs e)
        {
            PaintClearNumberTill(3);
        }

        private void S4_MouseLeave(object sender, MouseEventArgs e)
        {
            PaintClearNumberTill(4);
        }

        private void S5_MouseLeave(object sender, MouseEventArgs e)
        {
            PaintClearNumberTill(5);
        }

        private void S1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StarCount = 1;
            PaintOrageNumberTill(StarCount);
        }

        private void S2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StarCount = 2;
            PaintOrageNumberTill(StarCount);
        }

        private void S3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StarCount = 3;
            PaintOrageNumberTill(StarCount);
        }

        private void S4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StarCount = 4;
            PaintOrageNumberTill(StarCount);
        }

        private void S5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StarCount = 5;
            PaintOrageNumberTill(StarCount);
        }

        public void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            PaintOrageNumberTill(StarCount);
        }
    }
}
