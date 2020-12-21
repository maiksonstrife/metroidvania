using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Data/Player Data/Base Data")]
public class SwordCharacterData : ScriptableObject
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
    public float AirDashMaxHoldTime = 1f; //How much can hold dash button (slow time) before dash
    public float HoldTimeScale = 0.25f; //The bullet time
    public float AirDashTimeDistance = 0.2f; //The duration of dash
    public float AirDashVelocity = 30f; 
    public float Drag = 10f; // The drag slow down the Mass of a object, used here for "agains wind" effect
    public float DashEndYMultiplier = 0.2f; //Adds a force at the end of dash to keep momentum
    public float DistBetweenAfterImages = 0.5f; //The ghosting effect of Castlevania/Celeste

    [Header("Check Variables")]
    public float GroundCheckRadius = 0.2f;
    public float WallCheckDistance = 0.5f;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsGrabbable;

    [Header("Relics Unlock")]
    public bool canSlide = false;
    public bool canDoubleJump = false;
    public bool canWallJump = false;
}
