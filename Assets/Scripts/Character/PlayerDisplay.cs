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
        if (!gameObject.TryGetComponent(out playerModel)) Debug.LogWarning("Player Model Missing!");
    }

    private void SubscribeDataEvents()
    {
        if (playerModel == null) return;
        playerModel.level.Changed += OnLevelChanged;
        playerModel.health.Changed += OnHealthChanged;
        playerModel.speed.Changed += OnSpeedChanged;
        playerModel.energy.Changed += OnEnergyChanged;
        playerModel.freq.Changed += OnFreqChanged;
    }

    private void DesubscribeDataEvents()
    {
        if (playerModel == null) return;
        playerModel.level.Changed -= OnLevelChanged;
        playerModel.health.Changed -= OnHealthChanged;
        playerModel.speed.Changed -= OnSpeedChanged;
        playerModel.energy.Changed -= OnEnergyChanged;
        playerModel.freq.Changed -= OnFreqChanged;
    }

    public void UpdateLevelView()
    {

    }

    public void OnLevelChanged()
    {
        UpdateLevelView();
    }

    public void UpdateHealthView()
    {

    }

    public void OnHealthChanged()
    {
        UpdateHealthView();
    }

    public void UpdateSpeedView()
    {

    }

    public void OnSpeedChanged()
    {
        UpdateSpeedView();
    }

    public void UpdateEnergyView()
    {

    }

    public void OnEnergyChanged()
    {
        UpdateEnergyView();
    }

    public void UpdateFreqView()
    {

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
            case (int)PlayerStateType.RUN:
                EnterRunState();
                break;
            case (int)PlayerStateType.DASH:
                EnterDashState();
                break;
            case (int)PlayerStateType.JUMP:
                break;
            case (int)PlayerStateType.PUSH:
                break;
            default:
                break;
        }

        switch (toState.m_StateType)
        {
            case (int)PlayerStateType.IDLE:
                break;
            case (int)PlayerStateType.RUN:
                ExitRunState();
                break;
            case (int)PlayerStateType.DASH:
                ExitDashState();
                break;
            case (int)PlayerStateType.JUMP:
                break;
            case (int)PlayerStateType.PUSH:
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

    private void EnterRunState()
    {
        playerAni.SetBool("isRun", true);
    }

    private void ExitRunState()
    {
        playerAni.SetBool("isRun", false);
    }

    private void EnterDashState()
    {
        playerAni.SetBool("isDash", true);
    }

    private void ExitDashState()
    {
        playerAni.SetBool("isDash", false);
    }

    #endregion
}
