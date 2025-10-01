using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    private float originalMoveSpeed = 10f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    public bool isPlayerOne = true;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Gamepad.current != null) // make sure a controller is connected
        {
            if (isPlayerOne)
            {
                // Left stick for Player 1
                moveInput = Gamepad.current.leftStick.ReadValue();
            }
            else
            {
                // Right stick for Player 2
                moveInput = Gamepad.current.rightStick.ReadValue();
            }

            //had chatgpt help with getting both players to share one controller and Keyboard
            if (isPlayerOne)
            {
                moveInput += new Vector2(
                    (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0),
                    (Keyboard.current.wKey.isPressed ? 1 : 0) - (Keyboard.current.sKey.isPressed ? 1 : 0));
            }
            else
            {
                moveInput += new Vector2(
                    (Keyboard.current.lKey.isPressed ? 1 : 0) - (Keyboard.current.jKey.isPressed ? 1 : 0),
                    (Keyboard.current.iKey.isPressed ? 1 : 0) - (Keyboard.current.kKey.isPressed ? 1 : 0));
            }

        }

        rb.linearVelocity = moveInput * moveSpeed;

    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log($"{gameObject.name} got input: {moveInput}");
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 direction = isPlayerOne ? Vector2.right : Vector2.left;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Launch(direction);
            }


        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(DisableMovement(5f));
        }
    }

    private IEnumerator DisableMovement(float duration)
    {
        moveSpeed = 0;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalMoveSpeed; // restore
    }
}
