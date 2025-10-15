using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Buttons will be assigned with specific sceneloader tag for traversing the game scenes
/// </summary>
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
