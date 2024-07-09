using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   private int coinCount = 0;
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
        // movimiento horizontal (espejo)
        float x = Input.GetAxis("Horizontal");
        Vector3 horizontalMove = transform.right * x;
        //invertir el movimiento horizontal si es el jugador espejo 
        if (isMainPlayer)
        {
            horizontalMove = -horizontalMove;
        }
        //movimiento vetical independiente 
        float z = Input.GetAxis("Vertical");

        Vector3 verticalMove = transform.forward * z;

       // combinar movimientos
        Vector3 move = horizontalMove + verticalMove;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // salto (independiente para cada jugador )

        if(Input.GetButtonDown("Jump") && isGrounded)
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
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x,mirroredRotation.y,transform.rotation.eulerAngles.z));
        }

        

    }
    












    public void CollectCoin(CoinRotation coinRotation)
    {
        coinCount += coinRotation.value;
        Debug.Log($"Monedas recogidas: {coinCount}");
    }

    


}
