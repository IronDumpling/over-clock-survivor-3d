using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    [Header("Model")]
    private Player playerModel;

    [Header("View")]
    private Animator playerAni;

    #region Life Cycle

    private void Start()
    {
        LoadModel();
        LoadView();
        SubscribeStateEvents();
        SubscribeDataEvents();
    }

    private void OnDestroy()
    {
        DesubscribeStateEvents();
        DesubscribeDataEvents();
    }

    #endregion

    #region Get Data in Model

    private void LoadModel()
    {
        if (!gameObject.TryGetComponent(out playerModel))
            Debug.LogWarning("Player Model Missing!");
    }

    private void SubscribeDataEvents()
    {
        if (playerModel == null) return;
        playerModel.level.Changed += OnLevelChanged;
        playerModel.health.Changed += OnHealthChanged;
        playerModel.movement.speed.Changed += OnSpeedChanged;
        playerModel.energy.Changed += OnEnergyChanged;
        playerModel.freq.Changed += OnFreqChanged;
    }

    private void DesubscribeDataEvents()
    {
        if (playerModel == null) return;
        playerModel.level.Changed -= OnLevelChanged;
        playerModel.health.Changed -= OnHealthChanged;
        playerModel.movement.speed.Changed -= OnSpeedChanged;
        playerModel.energy.Changed -= OnEnergyChanged;
        playerModel.freq.Changed -= OnFreqChanged;
    }

    public void OnLevelChanged()
    {
        UpdateLevelView();
    }

    public void OnHealthChanged()
    {
        UpdateHealthView();
    }

    public void OnSpeedChanged()
    {
        UpdateSpeedParams();
        UpdateSpeedView();
    }

    public void OnEnergyChanged()
    {
        UpdateEnergyView();
    }

    public void OnFreqChanged()
    {
        UpdateFreqView();
    }

    #endregion

    #region Get States in Model

    private void SubscribeStateEvents()
    {
        if (playerModel == null) return;
        playerModel.StateMachine.Changed += ChangeState;
    }

    private void DesubscribeStateEvents()
    {
        if (playerModel == null) return;
        playerModel.StateMachine.Changed -= ChangeState;
    }

    private void ChangeState(State fromState, State toState)
    {
        switch (fromState.m_StateType)
        {
            case (int)PlayerStateType.IDLE:
                break;
            default:
                break;
        }

        switch (toState.m_StateType)
        {
            case (int)PlayerStateType.IDLE:
                break;
            default:
                break;
        }
    }

    #endregion

    #region Operate View

    private void LoadView()
    {
        if (!gameObject.TryGetComponent(out playerAni))
            Debug.LogWarning("Player Animator Controller Missing!");
    }

    private void UpdateSpeedParams()
    {
        playerAni.SetFloat("speed", playerModel.movement.speed.SpeedXZ);
        playerAni.SetFloat("speedX", playerModel.movement.speed.SpeedX);
        playerAni.SetFloat("speedZ", playerModel.movement.speed.SpeedZ);
    }

    public void UpdateLevelView()
    {

    }

    public void UpdateHealthView()
    {

    }

    public void UpdateSpeedView()
    {

    }

    public void UpdateEnergyView()
    {

    }

    public void UpdateFreqView()
    {

    }

    public void LookAt()
    {
        //Vector3 direction = rb.velocity;
        //direction.y = 0f;

        //if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
        //    this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        //else
        //    rb.angularVelocity = Vector3.zero;
    }

    #endregion
}
