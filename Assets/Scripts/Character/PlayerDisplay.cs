using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    [Header("Model")]
    private Player playerModel;

    [Header("View")]
    private Animator playerAni;

    private void Start()
    {
        LoadModel();
        LoadView();
    }

    #region Get Data in Model

    private void LoadModel()
    {
        playerModel = gameObject.GetComponent<Player>();
        if (playerModel == null) Debug.LogWarning("Player Model Missing!");
    }

    #endregion

    #region Get States in Model

    private void EnterMoveState()
    {
        playerAni.SetBool("isRun", true);
    }

    private void ExitMoveState()
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

    #region Operate View

    private void LoadView()
    {
        playerAni = gameObject.GetComponent<Animator>();
        if (playerAni == null) Debug.LogWarning("Player Animator Controller Missing!");
    }

    #endregion
}
