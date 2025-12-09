using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{
    [Header("Speed Setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed = 5f;
    public float speedRun = 10f;
    public float jumpForce = 10f;

    [Header("Animation Setup")]
    public float playerSwipeDuration = .1f;
    public string goingUp = "_goingUp";
    public string goingDown = "_goingDown";
    public string grounded = "_grounded";
}
