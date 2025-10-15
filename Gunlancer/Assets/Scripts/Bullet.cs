using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // variables for the bullet
    [SerializeField] public float speed = 10f;
    [SerializeField] public float lifeTime = 4f;

    //rigidbody
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //destorys bullet after lifetime is reached
        Destroy(gameObject, lifeTime);
    }

    void Awake()
    {
        //getting rigidbody
        rb = GetComponent<Rigidbody2D>();
        // error checking
        if (rb == null)
        {
            Debug.LogError("Bullet is missing Rigidbody2D!");
        }
    }

    //fires bullet in 
    public void Launch(Vector2 direction)
    {
        rb.linearVelocity = direction.normalized * speed;
    }

    //when bullet collides with another bullet or player they will be destroyed
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    
    
}