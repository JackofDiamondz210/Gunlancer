using UnityEngine;

public class StartMatch : MonoBehaviour
{

    private int playersInZone = 0;
    public GameObject objectToActivate;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            playersInZone++;
            CheckForActivation();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            playersInZone--;
            if (playersInZone < 2 && objectToActivate != null && objectToActivate.activeSelf)
            {
                objectToActivate.SetActive(false);
            }
        }
    }
    void CheckForActivation()
    {
        if (playersInZone >= 2)
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
                Debug.Log("Two players are in the zone! Activating object.");
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
