using UnityEngine;

public class Player : MonoBehaviour, IAgent
{
    [SerializeField]
    private PlayerSO playerConfig;

    public Level level;
    public Health health;
    public Speed speed;
    public Energy energy;
    public Frequency freq;

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
        if (playerConfig == null) return;

        level = new Level(playerConfig.level);
        health = new Health(playerConfig.health);
        speed = new Speed(playerConfig.speed);
        energy = new Energy(playerConfig.energy);
        freq = new Frequency(playerConfig.frequency);

        StateMachine = new StateMachine(this);
        StateMachine.AddStates(states);
        StateMachine.SetDefault((int)PlayerStateType.IDLE);
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
