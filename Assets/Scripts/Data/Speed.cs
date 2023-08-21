using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : GeneralData
{
    public void Maximise()
    {
        curr = max;
        Update();
    }
}
