using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Transform startPoint;
    
    public float speed = 31f;
    public float gravity = 9.81f;
    private Vector3 initialVelocity;
    private float timeSinceStart;
    private bool fired = false;

    void Update()
    {
        if (fired)
        {
            timeSinceStart += Time.deltaTime;
            Vector3 gravityVector = Vector3.down * gravity * timeSinceStart;
            Vector3 currentVelocity = initialVelocity + gravityVector;
            transform.position += currentVelocity * Time.deltaTime;

            if (transform.position.y <= 0)
            {
                transform.position = startPoint.position;
                fired = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && !fired)
        {
            Fire();
        }
    }

    void Fire()
    {
        initialVelocity = transform.forward * speed;
        timeSinceStart = 0f;
        fired = true;
    }

    void OnTriggerEnter(Collider other)
    {
        transform.position = startPoint.position;
        fired = false;
    }
}
