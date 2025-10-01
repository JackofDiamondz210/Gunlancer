using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void StartGameButton()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Menu");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
