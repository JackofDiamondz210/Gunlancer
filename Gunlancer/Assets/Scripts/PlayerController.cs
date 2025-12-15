using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    //getting settings for player movement and rigidbody, flag for player one, and setting up firing for player 1 and 2
    [SerializeField] private float moveSpeed = 10f;
    private float originalMoveSpeed = 10f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public bool isPlayerOne = true;
   
    //shooting settings and timer
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int maxAmmoPlayer1 = 3;
    [SerializeField] private int maxAmmoPlayer2 = 3;
    private int maxAmmo;
    private int currentAmmo;
    private bool isRecharging = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxAmmo = isPlayerOne ? maxAmmoPlayer1 : maxAmmoPlayer2;
        currentAmmo = maxAmmo;

        //getting players rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //moving the player when player inputs an action
        rb.linearVelocity = moveInput * moveSpeed;

    }

    // moving the player appropriatly with input action
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Firing a bullet prefab when appropriate fire button is performed
    public void Fire(InputAction.CallbackContext context)
    {
        if (!context.started) return; // only trigger once per press

        if (currentAmmo <= 0)
        {
            Debug.Log((isPlayerOne ? "Player 1" : "Player 2") + " has no ammo!");
            return;
        }

        Shoot();
        currentAmmo--;
        Debug.Log((isPlayerOne ? "Player 1" : "Player 2") + " ammo left: " + currentAmmo);

        if (!isRecharging)
            StartCoroutine(RechargeAmmo(3f));
    }

    // when current ammo is less than max ammo start recharging ammo
    private IEnumerator RechargeAmmo(float ammoRechargeTime)
    {
        isRecharging = true;

        while (currentAmmo < maxAmmo)
        {
            yield return new WaitForSeconds(ammoRechargeTime);
            currentAmmo++;
            //Debug.Log((isPlayerOne ? "Player 1" : "Player 2") + " fired at " + Time.time);
        }

        isRecharging = false; //stop rechargin when full
    }

    //to be called when firing a bullet and will fire in corrcet direction based on character
    private void Shoot()
    {
        Vector2 direction = isPlayerOne ? Vector2.right : Vector2.left;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Launch(direction);
        Debug.Log((isPlayerOne ? "Player 1" : "Player 2") + " ammo recharged: " + currentAmmo);
    }

    //When hit with a bullet tagged object player will be stunned for 5 seconds
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(DisableMovement(5f));
        }
    }

    // Will stop players movement for 5 seconds 
    private IEnumerator DisableMovement(float duration)
    {
        moveSpeed = 0;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalMoveSpeed; // restore
    }



}
