using System;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // variables for the bullet
    [SerializeField] public float speed = 10f;
    [SerializeField] public float lifeTime = 4f;

    //rigidbody
    Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    //setting player 1 and 2 bullet sprites
    public Sprite Player1Bullet;
    public Sprite Player2Bullet;


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

        //getting SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Bullet is missing SpriteRenderer!");
        }

    }

    //fires bullet in correct direction with correct sprite
    public void Launch(Vector2 direction, bool isPlayerOne)
    {
        rb.linearVelocity = direction.normalized * speed;

        // Set sprite based on player
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = isPlayerOne ? Player1Bullet : Player2Bullet;
        }
    }

    //when bullet collides with another bullet or player they will be destroyed
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


}