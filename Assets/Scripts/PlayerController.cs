using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
      public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    public bool isMainPlayer = true;
    public Transform mirroredPlayer;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Invertir el moviento si es el jugador espejo 

        if (!isMainPlayer)
        {
            move = -move;
        }
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // Espejear la rotación
        if (mirroredPlayer != null)
        {
            Vector3 mirroredRotation = mirroredPlayer.rotation.eulerAngles;
            mirroredRotation.y = -mirroredRotation.y;
            transform.rotation = Quaternion.Euler(mirroredRotation);
        }



}
}
