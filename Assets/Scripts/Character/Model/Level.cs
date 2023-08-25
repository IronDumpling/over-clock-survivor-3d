using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : GeneralData<int>
{
    public Level(NoLimitData lv) : base()
    {
        this.min = lv.min;
        this.max = lv.max;
        this.curr = lv.curr;
    }
}
