using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : GeneralData
{
    public void Minimise()
    {
        curr = min;
        Update();
    }
}
