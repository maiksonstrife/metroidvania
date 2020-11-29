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
    
    [Header("Wall Slide State")]
    public float WallClimbVelocity = 3f;

    [Header("Check Variables")]
    public float GroundCheckRadius = 0.2f;
    public float WallCheckDistance = 0.5f;
    public LayerMask WhatIsGround;
    public LayerMask WhatIsGrabbable;

    [Header("Relics Unlock")]
    public bool canSlide = false;
    public bool canDoubleJump = false;
}
