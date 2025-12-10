using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Data;

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

    [Header("Jump Collision Check")]
    public float distToGround = 0;
    public float spaceToGround = 1f;

    private float _currentSpeed;
    private Animator _currentPlayer;
    private ParticleSystem _particles;
    private ParticleSystem _particlesJump;

    void Start()
    {
        OnKill += OnPlayerKill;

        _currentPlayer = Instantiate(soPlayerSetup.playerAnimator, transform);

        InstantiateParticles();

    }

    private void Update()
    {
        HandleJump();
        HandleJumpAnimation();
        HandleMovement();
        HandleParticles();

    }

    private void InstantiateParticles()
    {
        if (soPlayerSetup.particles != null)
        {
            _particles = Instantiate(soPlayerSetup.particles, gameObject.transform);
            _particles.transform.position = gameObject.transform.position;
        }

        if (soPlayerSetup.particlesJump != null)
        {
            _particlesJump = Instantiate(soPlayerSetup.particlesJump, gameObject.transform);
            _particlesJump.transform.position = gameObject.transform.position;
        }
    }

    private bool isGrounded()
    {
        Debug.DrawRay(transform.position, new Vector2(0, -(spaceToGround + distToGround)), Color.magenta, .5f);
        return Physics2D.Raycast(transform.position, Vector2.down, spaceToGround + distToGround, LayerMask.GetMask("Floor"));
    }

    

    private void HandleParticles()
    {
        if(isGrounded() && !_particles.isPlaying)
        {
            _particles.Play();
        }
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
            _currentPlayer.SetBool("_running", true);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x != 1)
            {
                myRigidBody.transform.DOScaleX(1, soPlayerSetup.playerSwipeDuration);
            }
            _currentPlayer.SetBool("_running", true);
        }
        else
        {
            _currentPlayer.SetBool("_running", false);
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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            myRigidBody.velocity = Vector2.up * soPlayerSetup.jumpForce;
            // _particlesJump.Play();
            VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.JUMP, gameObject.transform.position);
            _particles.Stop();
            // myRigidBody.transform.localScale = Vector2.one;
            // HandleScaleJump();

        }
    }

    private void HandleJumpAnimation()
    {
        if (myRigidBody.velocity.y > 0 && !isGrounded())
        {
            // Debug.Log("moving up");
            _currentPlayer.SetBool(soPlayerSetup.grounded, false);
            _currentPlayer.SetBool(soPlayerSetup.goingDown, false);
            _currentPlayer.SetBool(soPlayerSetup.goingUp, true);
        }
        else if (myRigidBody.velocity.y < 0 && !isGrounded())
        {
            // Debug.Log("moving down");
            _currentPlayer.SetBool(soPlayerSetup.grounded, false);
            _currentPlayer.SetBool(soPlayerSetup.goingUp, false);
            _currentPlayer.SetBool(soPlayerSetup.goingDown, true);
        }
        else
        {
            _currentPlayer.SetBool(soPlayerSetup.goingUp, false);
            _currentPlayer.SetBool(soPlayerSetup.goingDown, false);
            _currentPlayer.SetBool(soPlayerSetup.grounded, true);
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
        _currentPlayer.SetTrigger("_death");
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }

}
