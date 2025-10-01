using UnityEngine;
using UnityEngine.SceneManagement;

public class Player2Win : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player2"))
        {
            SceneManager.LoadScene("P2Win");
        }

    }
}
