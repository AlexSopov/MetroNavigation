using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MetroDemo
{
    public partial class MetroStationControl : UserControl
    {
        public MetroStationControl()
        {
            InitializeComponent();
        }

        // Flashing marker color
        static SolidColorBrush fillBrushMarkerAnimation;
        bool IsAnimation;

        /// <summary>
        /// Makes flashing animation to station
        /// </summary>
        public void StartMarkerAnimation()
        {
            marker.StrokeThickness = 1;

            if (fillBrushMarkerAnimation == null)
            {
                fillBrushMarkerAnimation = new SolidColorBrush();

                ColorAnimation markerColorAnimation = new ColorAnimation()
                {
                    AutoReverse = true,
                    RepeatBehavior = RepeatBehavior.Forever,
                    From = Colors.White,
                    To = Colors.Green,
                    Duration = new Duration(TimeSpan.FromSeconds(1))
                };

                fillBrushMarkerAnimation.BeginAnimation(SolidColorBrush.ColorProperty, markerColorAnimation);
            }
            marker.Fill = fillBrushMarkerAnimation;
            stationNameBlock.Foreground = Brushes.Red;

            IsAnimation = true;
        }

        /// <summary>
        /// Stops flashing animation
        /// </summary>
        public void StopMarkerAnimation()
        {
            marker.StrokeThickness = 3;
            marker.Fill = Resources["fillBrush"] as Brush;
            stationNameBlock.Foreground = Resources["textBrush"] as Brush;

            IsAnimation = false;
        }

        private void PanelDirection_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            stationNameBlock.Foreground = Brushes.Red;
        }
        private void PanelDirection_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!IsAnimation)
                stationNameBlock.Foreground = Brushes.Black;
        }
    }
}
