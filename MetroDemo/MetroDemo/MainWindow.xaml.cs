using MetroDemo.MetroData;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Windows.Input;
using MetroDemo.Graph;
using System.Windows.Media.Animation;

namespace MetroDemo
{
    /// <summary>
    /// Main application window
    /// </summary>
    public partial class MainWindow : Window
    {
        // List of metro stations and segments
        private List<MetroStation> Stations;
        private List<MetroSegment> Segments;

        // Coordination of each station
        private Dictionary<MetroSegment, Point?> StationsPoints;
        private Dictionary<MetroStation, Tuple<string, Point>> AdditionalPoints;

        // List of metro stations controls
        private List<MetroStationControl> MetroStationControls;

        // Selected stations
        private MetroStation FirstStation, SecondStation;

        // Visualing help variables
        private bool IsFirstStationSelected;
        private MetroStationControl FirstStationControl, SecondStationControl;

        private Path CurrentVisualPath;

        /// <summary>
        /// Construct MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Stations = new List<MetroStation>();
            Segments = new List<MetroSegment>();
            MetroStationControls = new List<MetroStationControl>();
            StationsPoints = new Dictionary<MetroSegment, Point?>();
            AdditionalPoints = new Dictionary<MetroStation, Tuple<string, Point>>();

            ParseCurrentMap();
            SetValidCommand();
        }

        /// <summary>
        /// Loads data about metro stations and paths from data source and makes visualizing
        /// </summary>
        private void ParseCurrentMap()
        {
            // XML document that represents city metro
            XDocument xDocument = XDocument.Parse(Properties.Resources.KyivMetro);

            #region Lines
            // Iterating for all lines that consists of stations
            foreach (XElement lineElement in xDocument.Element("Metro").Elements("Lines").Elements("Line"))
            {
                // Points of stations
                PolyLineSegment metroLinePolyLineSegment = new PolyLineSegment();

                // Current metro line
                MetroLine currentLine = new MetroLine(lineElement.Element("Name").Value, lineElement.Element("Color").Value);

                MetroStation previousMetroStation = null;
                double timeToPrevious = 0, previousX = 0, previousY = 0;

                #region Stations
                // Iterating for all stations in line
                foreach (XElement stationElement in lineElement.Element("Stations").Elements("Station"))
                {
                    // Init current metro station
                    double currentX = Convert.ToDouble(stationElement.Element("X").Value);
                    double currentY = Convert.ToDouble(stationElement.Element("Y").Value);

                    MetroStation currentStation = new MetroStation(stationElement.Element("Name").Value, currentLine, new Point(currentX, currentY));
                    Stations.Add(currentStation);

                    // Init visual control of metro station
                    MetroStationControl currentMetroStationControl = new MetroStationControl();
                    currentMetroStationControl.DataContext = currentStation;
                    currentMetroStationControl.MouseDown += MetroStation_Click;

                    // Placing visual control to canvas
                    SetMetroControlPosition(currentX, currentY,
                        Convert.ToBoolean(stationElement.Element("FromPointToLabel").Value),
                        currentMetroStationControl);

                    if (stationElement.Element("TimeToNext") != null)
                        timeToPrevious = Convert.ToDouble(stationElement.Element("TimeToNext").Value.Replace('.', ','));

                    metroLinePolyLineSegment.Points.Add(new Point(Convert.ToDouble(stationElement.Element("X").Value), Convert.ToDouble(stationElement.Element("Y").Value)));

                    // Additional visual points
                    if (stationElement.Element("AdditionalPathPoint") != null)
                    {
                        Point additionalPoint = new Point(
                            Convert.ToDouble(stationElement.Element("AdditionalPathPoint").Element("X").Value),
                            Convert.ToDouble(stationElement.Element("AdditionalPathPoint").Element("Y").Value));

                        metroLinePolyLineSegment.Points.Add(additionalPoint);

                        AdditionalPoints.Add(currentStation, new Tuple<string, Point>(stationElement.Element("AdditionalPathPoint").Element("To").Value, additionalPoint));
                    }

                    // Adding segment between two stations
                    if (previousMetroStation != null)
                    {
                        MetroSegment metroSegment = new MetroSegment(previousMetroStation, currentStation, timeToPrevious);
                        Segments.Add(metroSegment);
                    }

                    previousX = currentX;
                    previousY = currentY;
                    previousMetroStation = currentStation;

                }
                #endregion

                SetLinePath(metroLinePolyLineSegment, lineElement.Element("Color").Value, 5);
            }
            #endregion

            ParseTransfers(xDocument);

            Stations.Sort();
            cbFrom.ItemsSource = Stations;
            cbTo.ItemsSource = Stations;
        }

        /// <summary>
        /// Loads data about metro transfers between two lines
        /// </summary>
        /// <param name="xDocument"></param>
        private void ParseTransfers(XDocument xDocument)
        {
            // Iterating for all transfers
            foreach (XElement transfer in xDocument.Element("Metro").Elements("Transfers").Elements("Transfer"))
            {
                MetroStation fromStation = null, toStation = null;

                // Find start and destination stations
                // Then adding route between them
                foreach (MetroStation station in Stations)
                {
                    if (station.Name == transfer.Element("From").Element("Station").Value
                        && station.StationLine.Name == transfer.Element("From").Element("Line").Value)
                        fromStation = station;
                    else if (station.Name == transfer.Element("To").Element("Station").Value
                        && station.StationLine.Name == transfer.Element("To").Element("Line").Value)
                        toStation = station;

                    #region Connect figure
                    if (fromStation != null && toStation != null)
                    {
                        MetroSegment metroSegment = new MetroSegment(fromStation, toStation, 5);
                        Segments.Add(metroSegment);
                        StationsPoints.Add(metroSegment, null);

                        Path path = new Path();
                        path.Stroke = Brushes.Black;

                        // Add visual transfer path
                        double width, height;
                        if (Math.Abs(fromStation.Location.X - toStation.Location.X) > Math.Abs(fromStation.Location.Y - toStation.Location.Y))
                        {
                            width = 30;
                            height = 15;
                        }
                        else
                        {
                            width = 15;
                            height = 30;
                        }

                        TranslateTransform translateTransform = translateTransform = new TranslateTransform((fromStation.Location.X + toStation.Location.X) / 2 - width / 2,
                            (fromStation.Location.Y + toStation.Location.Y) / 2 - height / 2);

                        RectangleGeometry rectangle = new RectangleGeometry(
                            new Rect(new Size(width, height)), 
                            5, 
                            5,
                            translateTransform);
                        path.Data = rectangle;

                        MetroMap.Children.Add(path);

                        break;
                    }
                    #endregion
                }
            }
        }

        /// <summary>
        /// Set position of metroStationControl on Canvas
        /// </summary>
        private void SetMetroControlPosition(double stationX, double stationY, bool isInverse, MetroStationControl metroStationControl)
        {
            double stationVisualRadius = (double)Application.Current.Resources["StationCircleDiameter"];

            Panel.SetZIndex(metroStationControl, 1);

            Canvas.SetTop(metroStationControl, stationY - stationVisualRadius / 2 - 2);
            if (!isInverse)
            {
                metroStationControl.FlowDirection = FlowDirection.RightToLeft;
                Canvas.SetRight(metroStationControl, MetroMap.Width - stationX - stationVisualRadius / 2);
            }
            else
            {
                Canvas.SetLeft(metroStationControl, stationX - stationVisualRadius / 2);
            }
            MetroMap.Children.Add(metroStationControl);
            MetroStationControls.Add(metroStationControl);
        }

        /// <summary>
        /// Visualize metro line of polyLineSegment points
        /// </summary>
        private Path SetLinePath(PolyLineSegment polyLineSegment, string color, double thickness)
        {
            if (polyLineSegment.Points.Count == 0)
                return null;

            Path metroLinePath = new Path();
            PathGeometry metroLinePathGeometry = new PathGeometry();
            PathFigure metroLinePathFigure = new PathFigure();

            metroLinePathFigure.StartPoint = polyLineSegment.Points[0];

            metroLinePathFigure.Segments.Add(polyLineSegment);
            metroLinePathFigure.IsClosed = false;
            metroLinePathGeometry.Figures.Add(metroLinePathFigure);

            metroLinePath.StrokeThickness = thickness;
            metroLinePath.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
            metroLinePath.Data = metroLinePathGeometry;

            MetroMap.Children.Add(metroLinePath);

            return metroLinePath;
        }
        private void ShowPathInfo(IEnumerable<MetroStation> path, double totalPathWeight)
        {
            routeStationsView.Children.Clear();

            int stationsCount = 0;
            MetroStation previousStation = null;

            foreach (MetroStation station in path)
            {
                Path iconPath = null;
                string descriptionText = string.Empty; ;

                // Selecting icon and description text to current path
                if (previousStation == null)
                {
                    descriptionText = string.Format("Start your rout at the {0} station", station.Name);
                    iconPath = (Application.Current.Resources["walk_icon"] as Path);
                }
                else if (!station.StationLine.Equals(previousStation.StationLine))
                {
                    descriptionText = string.Format("Transfer here to the {0} station", station.Name);
                    iconPath = (Application.Current.Resources["walk_icon"] as Path);
                }
                else
                {
                    descriptionText = station.Name;
                    iconPath = (Application.Current.Resources["train_icon"] as Path);
                    iconPath.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(station.StationLine.LineColor));
                }

                // Creatin controls with data about current path
                Border border = new Border() { BorderThickness = new Thickness(0, 0, 0, 1), BorderBrush = Brushes.LightGray };

                Grid grid = new Grid() { Margin = new Thickness(0, 5, 0, 5) };
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50) });
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                iconPath.Margin = new Thickness(5, 0, 5, 0);
                iconPath.Width = 22;
                iconPath.Height = 30;

                TextBlock textBlckStationName = new TextBlock()
                {
                    Text = descriptionText,
                    Padding = new Thickness(5),
                    Margin = new Thickness(0, 0, 5, 0),
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 14
                };

                grid.Children.Add(iconPath);
                grid.Children.Add(textBlckStationName);

                iconPath.SetValue(Grid.ColumnProperty, 0);
                textBlckStationName.SetValue(Grid.ColumnProperty, 1);

                border.Child = grid;
                routeStationsView.Children.Add(border);
                previousStation = station;

                stationsCount++;
            }

            routeInfoDescription.Text = string.Format("{0} stations. Expected time: {1} min", 
                stationsCount, Math.Ceiling(totalPathWeight + stationsCount * 0.2));
        }
        private void ShowVisualPath(IEnumerable<MetroStation> path)
        {
            // Points of path
            PolyLineSegment metroLinePolyLineSegment = new PolyLineSegment();

            // Adding points to matroLinePolyLineSegment
            MetroStation previousStation = null;
            KeyValuePair<MetroStation, Tuple<string, Point>>? previousTo = null;
            foreach (MetroStation currentStation in path)
            {
                if (previousTo.HasValue && (previousTo.Value.Value.Item1.Equals(currentStation.Name) || previousTo.Value.Key.Equals(currentStation)))
                {
                    Point sourcePoint = previousTo.Value.Value.Item2;
                    metroLinePolyLineSegment.Points.Add(sourcePoint);
                }

                foreach (var item in AdditionalPoints)
                {
                    if (currentStation.Equals(item.Key) || currentStation.Name.Equals(item.Value.Item1))
                    {
                        previousTo = item;
                        break;
                    }
                }

                metroLinePolyLineSegment.Points.Add(currentStation.Location);
                previousStation = currentStation;
            }

            // Visualize path
            Path visualPath = SetLinePath(metroLinePolyLineSegment, "#FFFFFF", 2);

            if (CurrentVisualPath != null)
                MetroMap.Children.Remove(CurrentVisualPath);

            if (visualPath == null)
                return;

            CurrentVisualPath = visualPath;

            SolidColorBrush colorBrush = new SolidColorBrush();
            ColorAnimation colorAnimation = new ColorAnimation()
            {
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                From = Colors.Transparent,
                To = Colors.White,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };

            colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            CurrentVisualPath.Stroke = colorBrush;
        }
        private MetroSegment GetMetroSegment(MetroStation station1, MetroStation station2)
        {
            foreach (MetroSegment segment in Segments)
            {
                if ((segment.Station1.Equals(station1) && segment.Station2.Equals(station2) ||
                    (segment.Station1.Equals(station2) && segment.Station2.Equals(station1))))
                    return segment;
            }

            return null;
        }

        /// <summary>
        /// Gets and visualizes path between selected stations
        /// </summary>
        private void ProcesMinimumPath()
        {
            SetValidCommand();
            if (FirstStation == null || SecondStation == null)
                return;

            // Initializing Dijkstra algorithm data
            DijkstraAlgorithm<MetroStation> dijkstraAlgorithm = new DijkstraAlgorithm<MetroStation>();
            foreach (var segment in Segments)
                dijkstraAlgorithm.AddEdge(segment.Station1, segment.Station2, segment.Time);

            double totalPathWeight;

            // Finding minimum path
            IEnumerable<MetroStation> minimumPath = dijkstraAlgorithm.GetMinimumPath(FirstStation, SecondStation, out totalPathWeight);

            ShowPathInfo(minimumPath, totalPathWeight);
            ShowVisualPath(minimumPath);
        }

        /// <summary>
        /// Handles click on metro station
        /// </summary>
        private void MetroStation_Click(object sender, MouseButtonEventArgs e)
        {
            MetroStationControl selectedItem = sender as MetroStationControl;
            if (!IsFirstStationSelected)
            {
                FirstStation = selectedItem.DataContext as MetroStation;

                if (FirstStationControl != null)
                    FirstStationControl.StopMarkerAnimation();
                FirstStationControl = selectedItem;
                FirstStationControl.StartMarkerAnimation();

                cbFrom.SelectedItem = FirstStation;
                IsFirstStationSelected = true;
            }
            else
            {
                if (SecondStationControl != null)
                    SecondStationControl.StopMarkerAnimation();

                SecondStation = selectedItem.DataContext as MetroStation;

                if (SecondStationControl != null)
                    SecondStationControl.StopMarkerAnimation();
                SecondStationControl = selectedItem;
                SecondStationControl.StartMarkerAnimation();

                cbTo.SelectedItem = SecondStation;
            }

            e.Handled = true;

        }

        private void SwapStops_Click(object sender, RoutedEventArgs e)
        {
            var temp = cbFrom.SelectedItem;
            cbFrom.SelectedItem = cbTo.SelectedItem;
            cbTo.SelectedItem = temp;
        }
        private void ComboBoxStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FirstStation = cbFrom.SelectedItem as MetroStation;
            SecondStation = cbTo.SelectedItem as MetroStation;

            if (FirstStationControl != null)
                FirstStationControl.StopMarkerAnimation();

            if (SecondStationControl != null)
                SecondStationControl.StopMarkerAnimation();

            foreach (MetroStationControl control in MetroStationControls)
            {
                if (FirstStation != null && control.DataContext.Equals(FirstStation))
                {
                    control.StartMarkerAnimation();
                    FirstStationControl = control;
                }
                else if (SecondStation != null && control.DataContext.Equals(SecondStation))
                {
                    control.StartMarkerAnimation();
                    SecondStationControl = control;
                }

            }

            ProcesMinimumPath();
        }

        /// <summary>
        /// Sets valid command accordin to current selection status
        /// </summary>
        private void SetValidCommand()
        {
            if (FirstStation == null || SecondStation == null)
            {
                routeStationsContainer.Visibility = Visibility.Collapsed;
                routeInfo.Visibility = Visibility.Collapsed;
                commandTextContainer.Visibility = Visibility.Visible;
            }

            if (FirstStation == null)
            {
                commandText.Text = "Select starting station";
            }
            else if (SecondStation == null)
            {
                commandText.Text = "Select destination station";
            }
            else if (FirstStation != null && SecondStation != null)
            {
                routeStationsContainer.Visibility = Visibility.Visible;
                routeInfo.Visibility = Visibility.Visible;
                commandTextContainer.Visibility = Visibility.Collapsed;
            }
        }   
    }
}