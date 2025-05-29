using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public InputActionAsset actionAsset;            // input actions and associated bindings
    public InputActionReference actionReference;    // reference to an action
    public UnityEvent actionEvent;                  // action event list
    public GameObject missionSheet;
    
    private bool missionSheetActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actionReference.action.performed += OnButtonPressed;    // add a callback to call when action is performed
        missionSheetActive = missionSheet.activeInHierarchy;
    }
    private void OnDestroy()
    {
        actionReference.action.performed -= OnButtonPressed;    // remove the callback
    }

    // Callback to call when action is performed
    void OnButtonPressed(InputAction.CallbackContext callback)
    {
        // invoke actions on the action events list
        actionEvent.Invoke();
    }

    public void ToggleMissionSheet()
    {
        missionSheetActive = !missionSheetActive;
        missionSheet.SetActive(missionSheetActive);
    }

    public void ForceOpenMissionSheet()
    {
        missionSheetActive = true;
        missionSheet.SetActive(missionSheetActive);
    }

    public void ForceCloseMissionSheet()
    {
        missionSheetActive = false;
        missionSheet.SetActive(missionSheetActive);
    }

    public void QuitApplication()
    {
        // quits the application
        Application.Quit();
    }
}
