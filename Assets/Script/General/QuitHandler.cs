using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
