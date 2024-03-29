using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook Look;
   
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        Look = GetComponent<PlayerLook>();
    }

    
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        
    }

    private void LateUpdate()
    {

        Look.ProcessLook(onFoot.Look.ReadValue<Vector2>());


    }

    private void OnEnable()
    {

        onFoot.Enable();

    }
    private void OnDisable()
    {
        onFoot.Disable();

    }
}
