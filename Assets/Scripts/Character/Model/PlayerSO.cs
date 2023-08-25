using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Character/Player")]
public class PlayerSO : ScriptableObject
{
    public NoLimitData level;
    public OneLimitData health;
    public OneLimitData energy;
    public TwoLimitsData speed;
    public TwoLimitsData frequency;
}

[System.Serializable]
public struct NoLimitData
{
    public int curr;
    public int min;
    public int max;
}

[System.Serializable]
public struct OneLimitData
{
    public int curr;
    public int min;
    public int max;
    public List<int> limits;
}

[System.Serializable]
public struct TwoLimitsData
{
    public float curr;
    public float min;
    public float max;
    public float naturalDrop;
    public List<float> lowerLimits;
    public List<float> upperLimits;
}

