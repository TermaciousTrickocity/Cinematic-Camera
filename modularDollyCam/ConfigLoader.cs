using modularDollyCam;
using System.Xml.Serialization;

public static class ConfigLoader
{
    public static GameConfiguration Load(string path)
    {
        var serializer = new XmlSerializer(typeof(GameConfiguration));
        using var stream = File.OpenRead(path);
        return (GameConfiguration)serializer.Deserialize(stream);
    }
}
