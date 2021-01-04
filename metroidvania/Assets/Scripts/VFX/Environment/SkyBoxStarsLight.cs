using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxStarsLight : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Shader.SetGlobalVector("_SunDirection", transform.forward);
    }
}
