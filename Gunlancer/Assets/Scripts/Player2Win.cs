using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2Win : MonoBehaviour
{
    //when player 2 collides with there appropriate gameobject they win
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            SceneManager.LoadScene("P2Win");
        }

    }
}
