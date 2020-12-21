using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImagePool : MonoBehaviour
{
    public static AfterImagePool Instance { get; private set; }

    [SerializeField]
    private GameObject _afterImagePrefab;
    //Queue sets objects inseide a queue, the first object added to queue, will be taken out with Dequeue
    private Queue<GameObject> _avaliableObjects = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    private void GrowPool() //Creates more GameObjects for the Pool
    {
        for (int i=0; i<10; i++)
        {
            var instanceToAdd = Instantiate(_afterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance) //Adds single Object to pool as false
    {
        instance.SetActive(false);
        _avaliableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool() //Deliver a object with Dequeue, if none object exists, it will create with GrowPool
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
