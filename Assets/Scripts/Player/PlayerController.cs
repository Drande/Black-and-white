using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    [SerializeField] private bool isMirror;
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpForce = 7;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private int  coinsToWin = 4;
    private readonly float rayLength = 1.05f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponentInChildren<Animator>();
    }

    public bool IsGrounded()
    {
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

        // Activate animations
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

        if(GameManager.Instance.totalCoins == coinsToWin)
        {
            GameManager.Instance.Victory();
        }
    }
}
