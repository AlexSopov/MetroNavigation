using System;
using System.Windows;

namespace MetroDemo.MetroData
{
    /// <summary>
    /// Metro station
    /// </summary>
    class MetroStation : IComparable<MetroStation>
    {
        /// <summary>
        /// Metro station name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Line where station is
        /// </summary>
        public MetroLine StationLine { get; private set; }

        /// <summary>
        /// Location of station
        /// </summary>
        public Point Location;

        public MetroStation(string name, MetroLine stationLine, Point location)
        {
            Name = name;
            StationLine = stationLine;
            Location = location;
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(MetroStation other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
