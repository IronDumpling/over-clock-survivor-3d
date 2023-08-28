using System;

public class GeneralData<T> where T : IComparable
{
    public event Action Changed;

    protected T min;
    protected T max;
    protected T curr;

    public GeneralData()
    {

    }

    public T Min
    {
        get => min;
        set => min = value;
    }

    public T Max
    {
        get => max;
        set => max = value;
    }

    public T Curr
    {
        get => curr;
        set
        {
            curr = value;
            curr = Clamp(curr, min, max);
            Update();
        }
    }

    protected virtual void Update()
    {
        Changed?.Invoke();
    }

    private T Clamp(T value, T min, T max)
    {
        if (value.CompareTo(min) < 0)
            return min;
        if (value.CompareTo(max) > 0)
            return max;
        return value;
    }
}