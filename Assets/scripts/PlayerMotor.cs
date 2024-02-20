using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public int health = 1;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    
    void Update()
    {
        isGrounded = controller.isGrounded;
        if(health<= 0)
        {
            Debug.Log("31");
        }
        
    }
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime); 
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        
        controller.Move(playerVelocity * Time.deltaTime);


    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
