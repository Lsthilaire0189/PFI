using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotationVolant : MonoBehaviour
{
    private LogitechGSDK.DIJOYSTATE2ENGINES rec;
    
    private float rotation;

    private float previousRotation;

    // Update is called once per frame

    private void Awake()
    {
        rec = new LogitechGSDK.DIJOYSTATE2ENGINES();
    }


    void FixedUpdate()
    {
        rec = LogitechGSDK.LogiGetStateUnity(0);
        rotation = Input.GetAxis("Horizontal") * 90;
        gameObject.transform.rotation *= Quaternion.Euler(0,0,-(rotation-previousRotation));
        previousRotation = rotation;

    }
}
