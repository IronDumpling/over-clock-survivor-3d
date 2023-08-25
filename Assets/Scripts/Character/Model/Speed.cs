using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : GeneralData<float>
{
    public Speed(TwoLimitsData sp) : base()
    {
        min = sp.min;
        curr = sp.curr;
        max = sp.max;
        naturalDrop = sp.naturalDrop;
        lowerLimits = sp.lowerLimits;
        upperLimits = sp.upperLimits;
    }

    protected float naturalDrop;
    public float NaturalDrop => naturalDrop;

    protected const float gravity = -9.81f;
    public float Gravity => gravity;

    protected float gravityScale = 5;
    public float GravityScale
    {
        get => gravityScale;
        set => gravityScale = value;
    }

    protected float checkRange = 0.1f;
    public float CheckRange => checkRange;

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

    public void Maximise()
    {
        curr = max;
        Update();
    }
}
