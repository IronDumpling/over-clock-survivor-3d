using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Model")]
    [SerializeField]
    private PlayerSO playerConfigure;
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

    [SerializeField]
    private Camera playerCamera;

    #region Life Cycle

    private void Start()
    {
        LoadModel();
        LoadView();
        LoadInput();
    }

    private void LoadModel()
    {
        playerModel = new Player();

        if (playerConfigure == null) return;
        // load player SO configuration into model
    }

    private void LoadView()
    {
        playerAni = gameObject.GetComponent<Animator>();
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
        playerInput.Player.Jump.started -= DoJump;

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

    private void DoJump(InputAction.CallbackContext context)
    {
        if (IsGrounded(0.3f))
        {

        }
    }

    private bool IsGrounded(float rayLength)
    {
        Ray ray = new(this.transform.position + Vector3.up * rayLength, Vector3.down);
        if (Physics.Raycast(ray, out _, rayLength))
            return true;
        return false;
    }

    #endregion

    #region View

    #endregion
}
