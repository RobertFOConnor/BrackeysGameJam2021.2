using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindingKeys : MonoBehaviour
{
    [SerializeField] private InputActionReference jumpAction = null;
    [SerializeField] private TMP_Text UpbindingDisplayNameText = null;
    [SerializeField] private GameObject startRebindObject = null;
    [SerializeField] private GameObject waitingForInputObject = null;
    
    [SerializeField]
    private PlayerInput playerInput = null;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;


    public void JumpRebind()    
    {
        startRebindObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        playerInput.SwitchCurrentActionMap("Player");

        jumpAction.action.Disable();

        rebindingOperation = jumpAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                int bindingIndex = jumpAction.action.GetBindingIndexForControl(jumpAction.action.controls[0]);
                
                UpbindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
                    jumpAction.action.bindings[bindingIndex].effectivePath,
                    InputControlPath.HumanReadableStringOptions.OmitDevice);

                rebindingOperation.Dispose();

                startRebindObject.SetActive(true);
                waitingForInputObject.SetActive(false);
                jumpAction.action.Enable();
            }).Start();
    }
}
