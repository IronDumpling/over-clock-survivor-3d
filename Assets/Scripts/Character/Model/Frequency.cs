using System.Collections.Generic;

public class Frequency : GeneralData
{
    protected int naturalDrop;
    public int NaturalDrop => naturalDrop;

    protected List<int> lowerLimits;
    public List<int> LowerLimits
    {
        get => lowerLimits;
        set => lowerLimits = value;
    }

    protected List<int> upperLimits;
    public List<int> UpperLimits
    {
        get => upperLimits;
        set => upperLimits = value;
    }

    public void Maximise()
    {
        curr = max;
        Update();
    }
}
