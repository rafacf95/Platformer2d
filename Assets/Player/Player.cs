using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Rigidbody2D myRigidBody;
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed = 5f;
    public float speedRun = 10f;
    public float jumpForce = 10f;

    private float _currentSpeed;
    private void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {

        // if (Input.GetKey(KeyCode.LeftShift))
        //     _currentSpeed = speedRun;
        // else
        //     _currentSpeed = speed;

        _currentSpeed = Input.GetKey(KeyCode.LeftShift) ? speedRun : speed;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
        }

        if (myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity -= friction;
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity += friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity = Vector2.up * jumpForce;
        }
    }
}
