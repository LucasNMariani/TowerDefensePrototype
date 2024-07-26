using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBarLookAtCamera : MonoBehaviour
{
    [SerializeField] Camera _cam;

    private void Start()
    {
        _cam = Camera.main;    
    }

    void Update()
    {
        transform.LookAt(_cam.transform.position);
        //transform.rotation = Quaternion.LookRotation(transform.position -  _cam.transform.position);
    }
}
