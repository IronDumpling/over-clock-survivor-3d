using System.Collections.Generic;

public class Health : GeneralData
{
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
