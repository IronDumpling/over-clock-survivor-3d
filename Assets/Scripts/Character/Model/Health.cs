using System.Collections.Generic;

public class Health : GeneralData<int>
{
    public Health(OneLimitData hp) : base()
    {
        min = hp.min;
        curr = hp.curr;
        max = hp.max;
        limits = hp.limits;
    }

    protected List<int> limits;
    public List<int> Limits
    {
        get => limits;
        set => limits = value;
    }

    public void Maximise()
    {
        curr = max;
        Update();
    }
}
