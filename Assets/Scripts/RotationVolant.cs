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
    

    void FixedUpdate()
    {
        /*rec = LogitechGSDK.LogiGetStateUnity(0);
        rotation =  15*Input.GetAxis("Horizontal");
        if (rotation != previousRotation)
        {
            transform.Rotate(0, 0, rotation, Space.Self);
        }

        previousRotation = rotation;*/
    }
}
