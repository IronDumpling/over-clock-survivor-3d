using System;
using UnityEngine;

public class GeneralData
{
    public event Action Changed;

    protected int min;
    protected int max;
    protected int curr;

    public int Min
    {
        get => min;
        set => min = value;
    }

    public int Max
    {
        get => max;
        set => max = value;
    }

    public int Curr
    {
        get => curr;
        set => curr = value;
    }

    public void Increment(int amount)
    {
        curr += amount;
        curr = Mathf.Clamp(curr, min, max);
        Update();
    }

    public void Decrement(int amount)
    {
        curr -= amount;
        curr = Mathf.Clamp(curr, min, max);
        Update();
    }

    /// <summary>
    /// Invokes the event
    /// </summary>
    public void Update()
    {
        Changed.Invoke();
    }
}
