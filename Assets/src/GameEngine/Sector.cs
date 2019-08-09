public class Sector
{
    public string name;
    public string type;

    public Sector(string name, string type)
    {
        this.name = name;
        this.type = type;
    }

    public string Type { get => type; set => type = value; }
    public string Name { get => name; set => name = value; }
}