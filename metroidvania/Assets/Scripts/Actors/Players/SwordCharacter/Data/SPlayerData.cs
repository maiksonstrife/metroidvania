using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSPlayerData", menuName = "Data/SPlayer Data/Base Data")]
public class SPlayerData : ScriptableObject
{
    [Header("Move State")]
    public float MovementVelocity = 10f;

    [Header("Jump State")]
    public float JumpVelocity = 15f;
    public int AmountOfJumps = 2;

    [Header("Air State")]
    public float CoyoteTime = 0.2f;
    public float JumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float WallSlideVelocity = 3f;    
    public float WallClimbVelocity = 3f;

    [Header("Wall Jump State")]
    public float WallJumpVelocity = 22f;
    public float WallJumpTime = 0.1f;
    public Vector2 WallJumpAngle = new Vector2(0.3f, 1.5f);

    [Header("Ledge Climb State")]
    public Vector2 StartOffset;
    public Vector2 StopOffset;
    public float OffsetTransition;

    [Header("AirDash State")]
    public float AirDashCooldown = 0.5f;
    public float AirDashMaxHoldTime = 1f;
    public float HoldTimeScale = 0.25f; 
    public float AirDashTimeDistance = 0.2f;
    public float AirDashVelocity = 30f; 
    public float Drag = 10f;
    public float DashEndYMultiplier = 0.2f; 
    public float DistBetweenAfterImages = 0.5f; 

    [Header("Dash State")]
    public float DashCooldown = 0.5f;
    public float DashVelocity = 20f;
    public float DashTimeDistance = 0.2f; 
    public float DashDistBetweenAfterImages = 0.5f;
    public float DashJumpVelocity = 30f;
    public Vector2 DashJumpAngle = new Vector2(0.3f, 1.5f);

    [Header("Check Variables")]
    public float GroundCheckRadius = 0.2f;
    public float WallCheckDistance = 0.5f;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsGrabbable;

    [Header("Relics Unlock")]
    public bool canSlide = false;
    public bool canDoubleJump = false;
    public bool canWallJump = false;
    public bool canDash = false;
    public bool canAirDash = false;
    public bool canAirDashBulletTime = false;
}
