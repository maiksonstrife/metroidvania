﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    private Transform _SPlayerTransform;
    private SpriteRenderer _SPlayerSR;
    private SpriteRenderer _SR;
    private Color _color;
    [SerializeField]
    private float _activeTime = 0.1f;
    private float _timeActivated;
    private float _alpha;
    [SerializeField]
    private float _alphaSet = 0.8f;
    [SerializeField]
    private float alphaDecay = 10f; //Smaller the number the faster it fades

    private void OnEnable()
    {
        _SR = GetComponent<SpriteRenderer>();
        _SPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _SPlayerSR = _SPlayerTransform.GetComponent<SpriteRenderer>();

        _alpha = _alphaSet;
        _SR.sprite = _SPlayerSR.sprite;
        transform.position = _SPlayerTransform.position;
        transform.rotation = _SPlayerTransform.rotation;
        _timeActivated = Time.time;
    }

    private void Update()
    {
        _alpha -= alphaDecay * Time.deltaTime;
        _color = new Color(0f, 1f, 0.9f, _alpha);
        _SR.color = _color;
        if(Time.time >= (_timeActivated + _activeTime))
        {
            AfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
