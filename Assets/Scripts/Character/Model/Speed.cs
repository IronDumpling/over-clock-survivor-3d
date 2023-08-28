using System.Collections.Generic;
using System;
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

    public event Action Inputed;

    protected float speed = 0, speedX = 0, speedY = 0, speedZ = 0;
    public float SpeedXZ
    {
        get => speed;
        set
        {
            speed = value;
            Update();
        }
    }

    public float SpeedX
    {
        get => speedX;
        set
        {
            speedX = value;
            Update();
        }
    }

    public float SpeedY
    {
        get => speedY;
        set
        {
            speedY = value;
            Update();
        }
    }

    public float SpeedZ
    {
        get => speedZ;
        set
        {
            speedZ = value;
            Update();
        }
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

    protected override void Update()
    {
        base.Update();
        Inputed?.Invoke();
    }
}

public class Movement
{
    public Speed speed;

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

    public Movement(TwoLimitsData sp)
    {
        speed = new Speed(sp);
    }
}
