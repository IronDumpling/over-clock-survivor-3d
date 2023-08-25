using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Energy : GeneralData<int>
{
    public Energy(OneLimitData en) : base()
    {
        min = en.min;
        curr = en.curr;
        max = en.max;
        limits = en.limits;
    }

    protected List<int> limits;
    public List<int> Limits
    {
        get => limits;
        set => limits = value;
    }

    public void Minimise()
    {
        curr = min;
        Update();
    }
}
