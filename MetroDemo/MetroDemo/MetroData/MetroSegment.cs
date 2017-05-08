namespace MetroDemo.MetroData
{
    /// <summary>
    /// Metro segment between two stations
    /// </summary>
    class MetroSegment
    {
        /// <summary>
        /// First station
        /// </summary>
        public MetroStation Station1 { get; private set; }

        /// <summary>
        /// Second station
        /// </summary>
        public MetroStation Station2 { get; private set; }

        /// <summary>
        /// Time between stations
        /// </summary>
        public double Time;

        public MetroSegment(MetroStation station1, MetroStation station2, double time)
        {
            Station1 = station1;
            Station2 = station2;
            Time = time;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}: {2}", Station1, Station2, Time);
        }
    }
}
