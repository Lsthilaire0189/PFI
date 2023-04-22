using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableWheelPhysicsMaterial : MonoBehaviour
{
    

    private WheelCollider wheel;
    void Start()
    {
        wheel = GetComponent< WheelCollider >();
    }
    // static friction of the ground material.
    void FixedUpdate()
    {
        WheelHit hit;
        if (wheel.GetGroundHit(out hit))
        {
            WheelFrictionCurve fFriction = wheel.forwardFriction;
            var material = hit.collider.material;
            fFriction.stiffness = material.staticFriction;
            wheel.forwardFriction = fFriction;
            WheelFrictionCurve sFriction = wheel.sidewaysFriction;
            sFriction.stiffness = material.staticFriction;
            wheel.sidewaysFriction = sFriction;
        }
    }

}
