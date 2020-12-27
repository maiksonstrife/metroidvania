﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImagePool : MonoBehaviour
{
    public static AfterImagePool Instance { get; private set; }

    [SerializeField]
    private GameObject _afterImagePrefab;
    private Queue<GameObject> _avaliableObjects = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    private void GrowPool() 
    {
        for (int i=0; i<10; i++)
        {
            var instanceToAdd = Instantiate(_afterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        _avaliableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool() 
    {
        if(_avaliableObjects.Count == 0)
        {
            GrowPool();
        }
        var instance = _avaliableObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
