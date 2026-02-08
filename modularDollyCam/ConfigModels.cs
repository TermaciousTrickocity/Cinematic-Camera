using System.Xml.Serialization;

namespace modularDollyCam
{
    [XmlRoot("Configuration")]
    public class GameConfiguration
    {
        [XmlArray("Processes")]
        [XmlArrayItem("Process")]
        public List<string> Processes { get; set; }

        [XmlArray("Games")]
        [XmlArrayItem("Game")]
        public List<Game> Games { get; set; }
    }

    public class Game
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Build")]
        public List<Build> Builds { get; set; }
    }

    public class Build
    {
        [XmlAttribute("number")]
        public string Number { get; set; }

        public PointerSet Pointers { get; set; }
    }

    public class PointerSet
    {
        public string X { get; set; }
        public string Y { get; set; }
        public string Z { get; set; }
        public string Yaw { get; set; }
        public string Pitch { get; set; }
        public string Roll { get; set; }
        public string FOV { get; set; }
        public string TickCount { get; set; }
    }
}
