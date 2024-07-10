using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    [SerializeField] private bool isMirror;
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private bool isGrounded = true;
    private float rayLength = 1.05f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = gameObject.GetComponentInChildren<Animator>();
    }

    public bool IsGrounded()
    {
        // float rayLength = 3f; // Adjust based on your character's size
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out var hit, rayLength))
        {
            return true;
        }
        return false;
    }

    void Update()
    {
        isGrounded = IsGrounded();

        // Get input from the user
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        moveHorizontal *= isMirror ? -1 : 1;

        // Create a vector based on input
        Vector3 movement = new(-moveHorizontal, 0.0f, -moveVertical);
        // Move the player
        playerRb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
        // Move the opposite object with inverted X direction
        playerAnim.SetFloat("Speed_f", Math.Abs(moveHorizontal) + Math.Abs(moveVertical));

        // Rotate the player to face the movement direction
        if (movement != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(movement);
            playerRb.MoveRotation(newRotation);
        }

        // Jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            AudioManager.Instance.PlaySFX("Jump");
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = true;
    //     }
    // }

    // private void OnCollisionExit(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = false;
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            CollectCoin(other.gameObject.GetComponent<CoinRotation>());
            other.gameObject.SetActive(false);
        }
    }

    public void CollectCoin(CoinRotation coinRotation)
    {
        GameManager.Instance.AddCoin(coinRotation.value);
    }
}
