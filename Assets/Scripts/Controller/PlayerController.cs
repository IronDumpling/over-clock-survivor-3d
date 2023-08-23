using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Model")]
    private Player playerModel;

    [Header("View")]

    [Header("Input")]
    private InputHandler playerInput;
    private InputAction run;
    private InputAction jump;
    private InputAction dash;
    private InputAction push;
    private InputAction look;

    #region Life Cycle

    private void Start()
    {
        LoadModel();
        LoadInput();
        SubscribeDataEvents();
        SubscribeStateEvents();
        playerInput.Player.Enable();
    }

    private void Update()
    {
        playerModel.StateMachine.Update();
        
        if (run.IsPressed()) DoRun();
        if (dash.IsPressed()) DoDash();
        if (jump.IsPressed()) DoJump();
        if (push.IsPressed()) DoPush();
    }

    private void OnDestroy()
    {
        DesubscribeDataEvents();
        DesubscribeStateEvents();
        playerInput.Player.Disable();
    }

    #endregion

    #region Set Data in Model

    private void LoadModel()
    {
        if (!gameObject.TryGetComponent(out playerModel))
            Debug.LogWarning("Player Model Missing!");
    }

    private void SubscribeDataEvents()
    {
        if (playerModel == null) return;
    }

    private void DesubscribeDataEvents()
    {
        if (playerModel == null) return;
    }

    #region Level

    public void DownGrade(int amount)
    {
        playerModel.level?.Decrement(amount);
    }

    public void Upgrade(int amount)
    {
        playerModel.level?.Increment(amount);
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



    #endregion

    #endregion

    #region Set States in Model

    private void SubscribeStateEvents()
    {
        if (playerModel == null) return;
        playerModel.StateMachine.Changed += DoWhat;
    }

    private void DesubscribeStateEvents()
    {
        if (playerModel == null) return;
        playerModel.StateMachine.Changed -= DoWhat;
    }

    private void DoWhat(State fromState, State toState)
    {
        
    }

    #endregion

    #region Operate Inputs

    private void LoadInput()
    {
        playerInput = new InputHandler();
        run = playerInput.Player.Run;
        jump = playerInput.Player.Jump;
        dash = playerInput.Player.Dash;
        push = playerInput.Player.Push;
        look = playerInput.Player.Look;
        Debug.Log("Load Input");
    }

    #region Run

    private void DoRun()
    {
        Vector2 inputDirect = run.ReadValue<Vector2>();
        Vector3 forward = CameraHandler.Instance.GetCameraForward() * inputDirect.y;
        Vector3 horizontal = CameraHandler.Instance.GetCameraRight() * inputDirect.x;
        Vector3 runment = Vector3.ClampMagnitude(forward + horizontal, 1);
        transform.Translate(playerModel.speed.Curr * Time.deltaTime * runment, Space.World);
        CameraHandler.Instance.LookAt();

        Debug.Log("Run!");
    }

    #endregion

    #region Dash

    private void DoDash()
    {
        playerModel.speed.Maximise();

        Debug.Log("Dash!");
    }

    #endregion

    #region Jump

    private void DoJump()
    {
        playerModel.speed.Curr += (int)(playerModel.speed.Gravity *
                                        playerModel.speed.GravityScale * Time.deltaTime);

        if (IsGrounded(0.3f)) playerModel.speed.Curr = 10;

        transform.Translate(new Vector3(0, playerModel.speed.Curr, 0) * Time.deltaTime);

        Debug.Log("Jump!");
    }

    private bool IsGrounded(float rayLength)
    {
        Ray ray = new(transform.position + Vector3.up * rayLength, Vector3.down);
        if (Physics.Raycast(ray, out _, rayLength))
        {
            Debug.Log("Is Grounded!");
            return true;
        }

        Debug.Log("Not Grounded!");
        return false;
    }

    #endregion

    #region Push

    private void DoPush()
    {
        Debug.Log("Push!");
    }

    #endregion

    #endregion
}


