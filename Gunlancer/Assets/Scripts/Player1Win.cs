using UnityEngine;
using UnityEngine.SceneManagement;

public class Player1Win : MonoBehaviour
{
    //when player 2 collides with there appropriate gameobject they win
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            SceneManager.LoadScene("P1Win");
        }
    }
}
