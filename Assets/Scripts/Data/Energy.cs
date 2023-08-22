using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : GeneralData
{
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
