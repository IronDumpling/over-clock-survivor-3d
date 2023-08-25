using System;

public class GeneralData<T> where T : IComparable<T>
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
        set => curr = value;
    }

    public virtual void Increment(T amount)
    {
        //curr = curr.Add(curr, amount);
        //curr += amount;
        //curr = curr + amount;
        curr = Clamp(curr, min, max);
        Update();
    }

    public void Decrement(T amount)
    {
        //curr = curr.Minus(curr, amount);
        //curr -= amount;
        curr = Clamp(curr, min, max);
        Update();
    }

    public void Update()
    {
        Changed.Invoke();
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

public interface IAddable<T>
{
    T Add(T a, T b);
}

public interface IMinable<T>
{
    T Minus(T a, T b);
}

public struct IntData : IAddable<int>, IMinable<int>
{
    public readonly int Add(int a, int b)
    {
        return a + b;
    }

    public readonly int Minus(int a, int b)
    {
        return a - b;
    }
}

public struct FloatData : IAddable<float>, IMinable<float>
{
    public readonly float Add(float a, float b)
    {
        return a + b;
    }

    public readonly float Minus(float a, float b)
    {
        return a - b;
    }
}