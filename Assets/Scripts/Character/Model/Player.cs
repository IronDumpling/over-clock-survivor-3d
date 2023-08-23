using UnityEngine;

public class Player : MonoBehaviour, IAgent
{
    [SerializeField]
    private PlayerSO playerConfig;

    #region Level

    public Level level;

    private void LoadLevel(NoLimitData lv)
    {
        level.Min = lv.min;
        level.Curr = lv.curr;
        level.Max = lv.max;
    }

    #endregion

    #region Health

    public Health health;

    private void LoadHealth(OneLimitData hp)
    {
        health.Min = hp.min;
        health.Curr = hp.curr;
        health.Max = hp.max;
        health.Limits = hp.limits;
    }

    #endregion

    #region Speed

    public Speed speed;

    private void LoadSpeed(TwoLimitsData sp)
    {
        speed.Min = sp.min;
        speed.Curr = sp.curr;
        speed.Max = sp.max;
        speed.LowerLimits = sp.lowerLimits;
        speed.UpperLimits = sp.upperLimits;
    }

    #endregion

    #region Energy

    public Energy energy;

    private void LoadEnergy(OneLimitData en)
    {
        energy.Min = en.min;
        energy.Curr = en.curr;
        energy.Max = en.max;
        energy.Limits = en.limits;
    }

    #endregion

    #region Frequency

    public Frequency freq;

    private void LoadFreq(TwoLimitsData fq)
    {
        freq.Min = fq.min;
        freq.Curr = fq.curr;
        freq.Max = fq.max;
        freq.LowerLimits = fq.lowerLimits;
        freq.UpperLimits = fq.upperLimits;
    }

    #endregion

    #region State

    public StateMachine StateMachine;

    private State[] states =
    {
        new PlayerIdleState(),
        new PlayerRunState(),
        new PlayerJumpState(),
        new PlayerPushState()
    };

    #endregion

    private void Awake()
    {
        level = new Level();
        health = new Health();
        speed = new Speed();
        energy = new Energy();
        freq = new Frequency();

        StateMachine = new StateMachine(this);
        StateMachine.AddStates(states);
        StateMachine.SetDefault((int)PlayerStateType.IDLE);

        if(playerConfig != null)
        {
            LoadLevel(playerConfig.level);
            LoadHealth(playerConfig.health);
            LoadEnergy(playerConfig.energy);
            LoadSpeed(playerConfig.speed);
            LoadFreq(playerConfig.frequency);
        }
    }
}

#region State Definitions

public enum PlayerStateType
{
    IDLE,
    RUN,
    DASH,
    JUMP,
    PUSH
}

public class PlayerIdleState : State
{
    public PlayerIdleState()
    {
        m_StateType = (int)PlayerStateType.IDLE;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override State Execute()
    {
        return this;
    }
}

public class PlayerRunState : State
{
    public PlayerRunState()
    {
        m_StateType = (int)PlayerStateType.RUN;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override State Execute()
    {
        return this;
    }
}

public class PlayerDashState : State
{
    public PlayerDashState()
    {
        m_StateType = (int)PlayerStateType.DASH;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override State Execute()
    {
        return this;
    }
}

public class PlayerJumpState : State
{
    public PlayerJumpState()
    {
        m_StateType = (int)PlayerStateType.JUMP;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override State Execute()
    {
        return this;
    }
}

public class PlayerPushState : State
{
    public PlayerPushState()
    {
        m_StateType = (int)PlayerStateType.PUSH;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override State Execute()
    {
        return this;
    }
}

#endregion
