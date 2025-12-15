using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Buttons will be assigned with specific sceneloader tag for traversing the game scenes
/// </summary>
public class SceneLoader : MonoBehaviour
{
    //start button
    public void StartGameButton()
    {
        SceneManager.LoadScene("Gameplay");
    }

    //main menu button
    public void Restart()
    {
        SceneManager.LoadScene("Menu");
    }

    //quit game
    public void QuitGame()
    {
        Application.Quit();
    }
}
