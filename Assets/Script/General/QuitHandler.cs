//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.InputSystem;

//public class QuitHandler : MonoBehaviour
//{
//    //public InputActionAsset actionAsset;            // input actions and associated bindings
//    public InputActionReference actionReference;    // reference to an action
//    //public UnityEvent actionEvent;                  // action event list

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    //void Start()
//    //{
//    //    actionReference.action.performed += OnButtonPressed;    // add a callback to call when action is performed
//    //}
//    //private void OnDestroy()
//    //{
//    //    actionReference.action.performed -= OnButtonPressed;    // remove the callback
//    //}

//    //// Callback to call when action is performed
//    //void OnButtonPressed(InputAction.CallbackContext callback)
//    //{
//    //    // invoke actions on the action events list
//    //    actionEvent.Invoke();
//    //}
//    void OnEnable()
//    {
//        if (actionReference != null)
//        {
//            actionReference.action.Enable();
//            actionReference.action.performed += ShowMenu;
//        }
//    }

//    void OnDisable()
//    {
//        if (actionReference != null)
//        {
//            actionReference.action.performed -= ShowMenu;
//            actionReference.action.Disable();
//        }
//    }

//    void ShowMenu(InputAction.CallbackContext context)
//    {
//        // This will be handled by UnityEvent in Inspector to activate the Menu object
//    }

//    public void QuitApplication()
//    {
//        // quits the application
//        Application.Quit();
//    }
//}
using UnityEngine;

public class QuitHandler : MonoBehaviour
{
    public GameObject menuToShow;

    public void ShowMenu()
    {
        if (menuToShow != null)
        {
            menuToShow.SetActive(true);
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
