using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : GeneralData
{
    protected int naturalDrop;
    public int NaturalDrop => naturalDrop;

    protected const float gravity = -9.81f;
    public float Gravity => gravity;

    protected int gravityScale = 5;
    public int GravityScale
    {
        get => gravityScale;
        set => gravityScale = value;
    }

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
