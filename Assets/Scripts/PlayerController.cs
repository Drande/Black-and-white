using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int coinCount = 0;
    [SerializeField] private GameObject mirrorPlayer;
    private Rigidbody playerRb;
    private Rigidbody mirrorRb;
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private bool isGrounded = true;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        mirrorRb = mirrorPlayer.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from the user
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a vector based on input
        Vector3 movement = new(-moveHorizontal, 0.0f, -moveVertical);
        // Move the player
        playerRb.AddForce(movement * moveSpeed);
        // Move the opposite object with inverted X direction
        Vector3 oppositeMovement = new(moveHorizontal, 0.0f, -moveVertical);
        mirrorRb.AddForce(oppositeMovement * moveSpeed);

        // Jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            mirrorRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.CompareTag("Ground")) {
            isGrounded = false;
        }
    }

    public void CollectCoin(CoinRotation coinRotation)
    {
        coinCount += coinRotation.value;
        Debug.Log($"Monedas recogidas: {coinCount}");
    }
}
