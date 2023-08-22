using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Model")]
    [SerializeField]
    private PlayerSO playerConfig;
    private Player playerModel;

    [Header("View")]
    private Animator playerAni;

    [Header("Input")]
    private InputHandler playerInput;
    private InputAction move;
    private InputAction jump;
    private InputAction dash;
    private InputAction push;
    private InputAction look;

    #region Life Cycle

    private void Start()
    {
        LoadModel();
        LoadView();
        LoadInput();
    }

    private void LoadModel()
    {
        if (playerConfig == null) playerModel = new Player();
        else playerModel = new Player(playerConfig);
    }

    private void LoadView()
    {
        playerAni = gameObject.GetComponent<Animator>();
        if (playerAni == null) Debug.LogWarning("Player Animator Controller Missing!");
    }

    private void LoadInput()
    {
        playerInput = new InputHandler();
        move = playerInput.Player.Move;
        jump = playerInput.Player.Jump;
        dash = playerInput.Player.Dash;
        push = playerInput.Player.Push;
        look = playerInput.Player.Look;
    }

    private void OnEnable()
    {
        if (playerModel == null) return;
        playerModel.level.Changed += OnLevelChanged;
        playerModel.health.Changed += OnHealthChanged;
        playerModel.speed.Changed += OnSpeedChanged;
        playerModel.energy.Changed += OnEnergyChanged;
        playerModel.freq.Changed += OnFreqChanged;

        if (playerInput == null) return;
        jump.started += DoJump;
        move.started += DoMove;
        dash.started += DoDash;
        push.started += DoPush;

        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        if (playerModel == null) return;
        playerModel.level.Changed -= OnLevelChanged;
        playerModel.health.Changed -= OnHealthChanged;
        playerModel.speed.Changed -= OnSpeedChanged;
        playerModel.energy.Changed -= OnEnergyChanged;
        playerModel.freq.Changed -= OnFreqChanged;

        if (playerInput == null) return;
        jump.started -= DoJump;
        move.started -= DoMove;
        dash.started -= DoDash;
        push.started -= DoPush;

        playerInput.Player.Disable();
    }

    #endregion

    #region Model

    #region Level

    public void DownGrade(int amount)
    {
        playerModel.level?.Decrement(amount);
    }

    public void Upgrade(int amount)
    {
        playerModel.level?.Increment(amount);
    }

    public void UpdateLevelView()
    {

    }

    public void OnLevelChanged()
    {
        UpdateLevelView();
    }

    #endregion

    #region Health

    public void Damage(int amount)
    {
        playerModel.health?.Decrement(amount);
    }

    public void Heal(int amount)
    {
        playerModel.health?.Increment(amount);
    }

    public void Reset()
    {
        playerModel.health?.Maximise();
    }

    public void UpdateHealthView()
    {

    }

    public void OnHealthChanged()
    {
        UpdateHealthView();
    }

    #endregion

    #region Speed

    public void SpeedDown(int amount)
    {
        playerModel.speed?.Decrement(amount);
    }

    public void SpeedUp(int amount)
    {
        playerModel.speed?.Increment(amount);
    }

    public void SpeedMax()
    {
        playerModel.speed?.Maximise();
    }

    public void UpdateSpeedView()
    {

    }

    public void OnSpeedChanged()
    {
        UpdateSpeedView();
    }

    #endregion

    #region Energy

    public void LoseEngergy(int amount)
    {
        playerModel.energy?.Decrement(amount);
    }

    public void GainEnergy(int amount)
    {
        playerModel.energy?.Increment(amount);
    }

    public void ResetEnergy()
    {
        playerModel.energy?.Minimise();
    }

    public void UpdateEnergyView()
    {

    }

    public void OnEnergyChanged()
    {
        UpdateEnergyView();
    }

    #endregion

    #region Frequency

    public void DropFreq(int amount)
    {
        playerModel.freq?.Decrement(amount);
    }

    public void RiseFreq(int amount)
    {
        playerModel.freq?.Increment(amount);
    }

    public void ResetFreq()
    {
        playerModel.freq?.Maximise();
    }

    public void UpdateFreqView()
    {

    }

    public void OnFreqChanged()
    {
        UpdateFreqView();
    }

    #endregion

    #endregion

    #region Input

    private void DoMove(InputAction.CallbackContext context)
    {
        Vector2 inputDirect = move.ReadValue<Vector2>();
        Vector3 forward = CameraHandler.Instance.GetCameraForward() * inputDirect.y;
        Vector3 horizontal = CameraHandler.Instance.GetCameraRight() * inputDirect.x;
        Vector3 movement = Vector3.ClampMagnitude(forward + horizontal, 1);
        transform.Translate(playerModel.speed.Curr * Time.deltaTime * movement, Space.World);
        CameraHandler.Instance.LookAt();
    }

    private void DoDash(InputAction.CallbackContext context)
    {
        Vector2 inputDirect = move.ReadValue<Vector2>();
        Vector3 forward = CameraHandler.Instance.GetCameraForward() * inputDirect.y;
        Vector3 horizontal = CameraHandler.Instance.GetCameraRight() * inputDirect.x;
        Vector3 movement = Vector3.ClampMagnitude(forward + horizontal, 1);

        playerModel.speed.Maximise();
        transform.Translate(playerModel.speed.Curr * Time.deltaTime * movement, Space.World);

        CameraHandler.Instance.LookAt();
    }

    private void DoJump(InputAction.CallbackContext context)
    {
        playerModel.speed.Curr += (int)(playerModel.speed.Gravity *
                                        playerModel.speed.GravityScale * Time.deltaTime);

        if (IsGrounded(0.3f)) playerModel.speed.Curr = 10;

        transform.Translate(new Vector3(0, playerModel.speed.Curr, 0) * Time.deltaTime);
    }

    private bool IsGrounded(float rayLength)
    {
        Ray ray = new(transform.position + Vector3.up * rayLength, Vector3.down);
        if (Physics.Raycast(ray, out _, rayLength))
            return true;
        return false;
    }

    private void DoPush(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region View

    #endregion
}
