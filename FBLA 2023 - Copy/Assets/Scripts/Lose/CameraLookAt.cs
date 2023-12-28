using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public Transform ROBOT;
    void Update()
    {
        
        transform.LookAt(ROBOT);
    }
}
