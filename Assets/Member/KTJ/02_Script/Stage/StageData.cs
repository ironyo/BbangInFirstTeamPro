public class StageData
{
    public string Name { get; private set; }
    public int RoadTotalLength { get; private set; }

    private StageData(string name, int length)
    {
        Name = name;
        RoadTotalLength = length;
    }

    public static StageData Create(string name, int length)
    {
        return new StageData(name, length);
    }
}
