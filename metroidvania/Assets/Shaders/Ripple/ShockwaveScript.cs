using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveScript : MonoBehaviour {

    [SerializeField]
    private float _multiplier;
    [SerializeField]
    private Vector3 _startValue;

    private void Awake()
    {
        transform.localScale = _startValue;
    }

    void Update ()
    {
        transform.localScale += _startValue * _multiplier;
        if(transform.localScale.x >= 6f)
        {
            gameObject.SetActive(false);
            transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
	}

    private void OnDisable()
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }
}
    