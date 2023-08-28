using UnityEngine;

public class Player : MonoBehaviour, IAgent
{
    [SerializeField]
    private PlayerSO playerConfig;

    public Level level;
    public Health health;
    public Movement movement;
    public Energy energy;
    public Frequency freq;

    #region State

    public StateMachine StateMachine;

    private State[] states =
    {
        new PlayerIdleState(),
    };

    #endregion

    private void Awake()
    {
        if (playerConfig == null) return;

        level = new Level(playerConfig.level);
        health = new Health(playerConfig.health);
        movement = new Movement(playerConfig.speed);
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

#endregion
