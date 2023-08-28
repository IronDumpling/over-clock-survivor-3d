using System.Collections.Generic;

public class Frequency : GeneralData<float>
{
    public Frequency(TwoLimitsData fq) : base()
    {
        min = fq.min;
        curr = fq.curr;
        max = fq.max;
        naturalDrop = fq.naturalDrop;
        lowerLimits = fq.lowerLimits;
        upperLimits = fq.upperLimits;
    }

    protected float naturalDrop;
    public float NaturalDrop => naturalDrop;

    protected List<float> lowerLimits;
    public List<float> LowerLimits
    {
        get => lowerLimits;
        set => lowerLimits = value;
    }

    protected List<float> upperLimits;
    public List<float> UpperLimits
    {
        get => upperLimits;
        set => upperLimits = value;
    }
}
