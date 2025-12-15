using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //setting up player settings: movespeed, checking player 1 vs player 2, rigidbodies, move inputs
    [SerializeField] private float moveSpeed = 10f;
    private float originalMoveSpeed = 10f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public bool isPlayerOne = true;
   
    //shooting and ammo settings and bool to control ammo recharge
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int maxAmmoPlayer1 = 3;
    [SerializeField] private int maxAmmoPlayer2 = 3;
    private int maxAmmo;
    public int currentAmmo;
    private bool isRecharging = false;

    //text for stun timer
    [SerializeField] private TMP_Text stunTimerText;
    [SerializeField] private Canvas canvas;

    //bullet UI images
    public Image[] bulletUIImages;

    //items buffs: potion being speed increase for 3 seconds and a sheild to block bullets for 3 seconds
    [SerializeField] private float speedBoostMultiplier = 1.5f;
    [SerializeField] private float powerUpDuration = 3f;

    private Coroutine speedBoostCoroutine;
    private Coroutine shieldCoroutine;

    //shield check
    private bool hasShield = false;

    //setting up spriteRenderer to change players color when picking up item
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //making sure player 1 and 2 ammo are seperate
        maxAmmo = isPlayerOne ? maxAmmoPlayer1 : maxAmmoPlayer2;
        currentAmmo = maxAmmo;

        //getting players rigidbody
        rb = GetComponent<Rigidbody2D>();

        UpdateBulletUI();
        
        //getting SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //moving the player when player inputs an action
        rb.linearVelocity = moveInput * moveSpeed;

    }

    //LateUpdate is called after all updates
    void LateUpdate()
    {
        
        if (stunTimerText.gameObject.activeSelf)
        {
            Vector3 offset = new Vector3(1f, -1, 0); //manually setting effset position below players
            Vector3 worldPos = transform.position + offset; //get pos with offset

            // Convert world position to screen point
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

            // Set the UI text position
            stunTimerText.transform.position = screenPos;
        }
    }

    // moving the player appropriatly with input action
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    //firing a bullet prefab when appropriate fire button is performed
    public void Fire(InputAction.CallbackContext context)
    {
        if (!context.started) return; // only trigger once per press

        //no ammo
        if (currentAmmo <= 0)
        {
            //Debug.Log((isPlayerOne ? "Player 1" : "Player 2") + " has no ammo!");
            return;
        }

        //shooting and removing a bullet
        Shoot();
        currentAmmo--;
        UpdateBulletUI();

        //Debug.Log((isPlayerOne ? "Player 1" : "Player 2") + " ammo left: " + currentAmmo);

        if (!isRecharging)
        {
            StartCoroutine(RechargeAmmo(3f));
        }

    }

    //when current ammo is less than max ammo start recharging ammo
    private IEnumerator RechargeAmmo(float ammoRechargeTime)
    {
        isRecharging = true;

        while (currentAmmo < maxAmmo)
        {
            //adding a bullet back
            yield return new WaitForSeconds(ammoRechargeTime);
            currentAmmo++;
            UpdateBulletUI();
            //Debug.Log((isPlayerOne ? "Player 1" : "Player 2") + " fired at " + Time.time);
        }

        isRecharging = false; //stop rechargin when full
    }

    //to be called when firing a bullet and will fire in corrcet direction based on character
    private void Shoot()
    {
        Vector2 direction = isPlayerOne ? Vector2.right : Vector2.left;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Launch(direction, isPlayerOne);
        //Debug.Log((isPlayerOne ? "Player 1" : "Player 2") + " ammo recharged: " + currentAmmo);
    }

    //when hit with a bullet tagged object player will be stunned for 5 seconds
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (hasShield)
            {
                Destroy(collision.gameObject); // shield blocks bullet
                return;
            }

            StartCoroutine(DisableMovement(5f));
            Destroy(collision.gameObject);
        }
    }

    //when player collides an trigger game object with specific tag: it will give a specific buff
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //triggering speed potion effect
        if (collision.CompareTag("Potion"))
        {
            ApplySpeedBoost();
            Destroy(collision.gameObject);
        }

        //triggering shield effect
        if (collision.CompareTag("Shield"))
        {
            ApplyShield();
            Destroy(collision.gameObject);
        }
    }

    //applying speed boost
    private void ApplySpeedBoost()
    {
        //stop stacking
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);
        }

        speedBoostCoroutine = StartCoroutine(SpeedBoost());
    }

    //applying shield buff with duration
    private void ApplyShield()
    {
        //stop stacking
        if (shieldCoroutine != null)
        {
            StopCoroutine(shieldCoroutine);
        }

        shieldCoroutine = StartCoroutine(Shield());
    }

    //durations for potion and shield both 3 seconds from private float
    private IEnumerator SpeedBoost()
    {
        //chaning movespeed and color
        moveSpeed = originalMoveSpeed * speedBoostMultiplier;
        spriteRenderer.color = Color.green;

        yield return new WaitForSeconds(powerUpDuration);

        //return to normal
        moveSpeed = originalMoveSpeed;
        spriteRenderer.color = originalColor;
    }

    private IEnumerator Shield()
    {
        //changing shield bool to true and changing color
        hasShield = true;
        spriteRenderer.color = Color.yellow;

        yield return new WaitForSeconds(powerUpDuration);

        //return to normal
        hasShield = false;
        spriteRenderer.color = originalColor;
    }

    //will stop players movement for 5 seconds 
    private IEnumerator DisableMovement(float duration)
    {
        moveSpeed = 0;

        if (stunTimerText != null)
        {
            stunTimerText.gameObject.SetActive(true);
        }

        float remainingTime = duration;
        while (remainingTime > 0)
        {
            if (stunTimerText != null)
            {
                stunTimerText.text = remainingTime.ToString("F1") + "s";
            }

            remainingTime -= Time.deltaTime;
            yield return null;
        }

        //giving back original MoveSpeed
        moveSpeed = originalMoveSpeed;

        if (stunTimerText != null)
            stunTimerText.gameObject.SetActive(false);
    }

    //updating bullet UI
    private void UpdateBulletUI()
    {

        for (int i = 0; i < bulletUIImages.Length; i++)
        {
            bulletUIImages[i].enabled = i < currentAmmo;
        }
    }

}
