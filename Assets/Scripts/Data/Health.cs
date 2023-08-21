using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : GeneralData
{
    public void Maximise()
    {
        curr = max;
        Update();
    }
}
