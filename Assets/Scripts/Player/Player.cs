using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : HealthBase
{

    public Rigidbody2D myRigidBody;
    /*
    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed = 5f;
    public float speedRun = 10f;
    public float jumpForce = 10f;

    [Header("Animation Setup")]
    // public float jumpScaleY = 1.5f;
    // public float jumpScaleX = .5f;
    // public float duration = .3f;
    // public Ease ease = Ease.OutBack;
    public float playerSwipeDuration = .1f;
    public string goingUp = "_goingUp";
    public string goingDown = "_goingDown";
    public string grounded = "_grounded";
    */

    [Header("Setup")]
    public SOPlayerSetup soPlayerSetup;
    public Animator playerAnimator;

    private float _currentSpeed;

    void Start()
    {
        OnKill += OnPlayerKill;
    }

    private void Update()
    {
        HandleJump();
        HandleJumpAnimation();
        HandleMovement();

    }

    private void HandleMovement()
    {

        _currentSpeed = Input.GetKey(KeyCode.LeftShift) ? soPlayerSetup.speedRun : soPlayerSetup.speed;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x != -1)
            {
                myRigidBody.transform.DOScaleX(-1, soPlayerSetup.playerSwipeDuration);
            }
            playerAnimator.SetBool("_running", true);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x != 1)
            {
                myRigidBody.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            }
            playerAnimator.SetBool("_running", true);
        }
        else
        {
            playerAnimator.SetBool("_running", false);
        }

        if (myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity -= soPlayerSetup.friction;
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity += soPlayerSetup.friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity = Vector2.up * soPlayerSetup.jumpForce;
            // myRigidBody.transform.localScale = Vector2.one;
            // HandleScaleJump();

        }
    }

    private void HandleJumpAnimation()
    {
        if (myRigidBody.velocity.y > 0)
        {
            // Debug.Log("moving up");
            playerAnimator.SetBool(soPlayerSetup.grounded, false);
            playerAnimator.SetBool(soPlayerSetup.goingDown, false);
            playerAnimator.SetBool(soPlayerSetup.goingUp, true);
        }
        else if (myRigidBody.velocity.y < 0)
        {
            // Debug.Log("moving down");
            playerAnimator.SetBool(soPlayerSetup.grounded, false);
            playerAnimator.SetBool(soPlayerSetup.goingUp, false);
            playerAnimator.SetBool(soPlayerSetup.goingDown, true);
        }
        else
        {
            playerAnimator.SetBool(soPlayerSetup.goingUp, false);
            playerAnimator.SetBool(soPlayerSetup.goingDown, false);
            playerAnimator.SetBool(soPlayerSetup.grounded, true);
        }
    }

    // private void HandleScaleJump()
    // {
    //     DOTween.Kill(myRigidBody.transform);
    //     myRigidBody.transform.DOScaleY(jumpScaleY, duration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    //     myRigidBody.transform.DOScaleX(jumpScaleX, duration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    // }

    private void OnPlayerKill()
    {
        OnKill -= OnPlayerKill;
        playerAnimator.SetTrigger("_death");
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

}
