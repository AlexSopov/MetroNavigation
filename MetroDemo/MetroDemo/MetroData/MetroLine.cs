namespace MetroDemo.MetroData
{
    /// <summary>
    /// Metro line
    /// </summary>
    class MetroLine
    {
        /// <summary>
        /// Metro line name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Metro line color
        /// </summary>
        public string LineColor{ get; private set; }

        public MetroLine(string name, string lineColor)
        {
            Name = name;
            LineColor = lineColor;
        }
    }
}
