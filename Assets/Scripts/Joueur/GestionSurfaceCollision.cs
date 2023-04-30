using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GestionSurfaceCollision : MonoBehaviour
{
    
    public List<WheelCollider> listeRoueColliders;

    private int boueLayer = 11;
    private int eauLayer = 12;
    


    private void FixedUpdate()
    {
        WheelHit hit;
        foreach (var roueCollider in listeRoueColliders)
        {
            if(roueCollider.GetGroundHit(out hit))
            {
                if (hit.collider.gameObject.layer == 11)
                {
                    print("hit mud");
                    roueCollider.motorTorque /=1.0075f;
                }
                else if (hit.collider.gameObject.layer == 12)
                {
                    print("hit water");
                    
                    WheelFrictionCurve fFriction = roueCollider.forwardFriction;
                    WheelFrictionCurve sFriction = roueCollider.sidewaysFriction;
                    fFriction.stiffness = 0;
                    sFriction.stiffness = 0;

                    print($"forward friction :{roueCollider.forwardFriction.stiffness}");
                    print($"sideways friction :{roueCollider.sidewaysFriction.stiffness}");
                }

            }
        }
    }
    
}
