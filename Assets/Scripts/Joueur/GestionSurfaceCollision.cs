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

    
    private WheelFrictionCurve fFrictionEau;
    private WheelFrictionCurve sFrictionEau;
    
    private WheelFrictionCurve fFrictionNormale;
    private WheelFrictionCurve sFrictionNormale;
    

    private void Awake()
    {
        fFrictionEau = new WheelFrictionCurve();
        fFrictionNormale = new WheelFrictionCurve();
        sFrictionEau = new WheelFrictionCurve();
        sFrictionNormale = new WheelFrictionCurve();
        

        fFrictionEau = listeRoueColliders[0].forwardFriction;
        fFrictionNormale = listeRoueColliders[0].forwardFriction;
        
        sFrictionEau = listeRoueColliders[0].sidewaysFriction;
        sFrictionNormale = listeRoueColliders[0].sidewaysFriction;
        
        
        fFrictionEau.stiffness = 0.5f;
        fFrictionNormale.stiffness = 1;
        sFrictionEau.stiffness = 0.2f;
        sFrictionNormale.stiffness = 1;
    }

    private void FixedUpdate()
    {
        WheelHit hit;
        foreach (var roueCollider in listeRoueColliders)
        {
            if(roueCollider.GetGroundHit(out hit))
            {
                if (hit.collider.gameObject.layer == boueLayer)
                {
                    print("hit mud");
                    roueCollider.motorTorque /=1.0075f;
                }
                else if (hit.collider.gameObject.layer == eauLayer)
                {
                    print("hit water");

                    roueCollider.forwardFriction = fFrictionEau;
                    roueCollider.sidewaysFriction = sFrictionEau;

                    print($"forward friction :{roueCollider.forwardFriction.stiffness}");
                    print($"sideways friction :{roueCollider.sidewaysFriction.stiffness}");
                }
                else
                {
                    roueCollider.forwardFriction = fFrictionNormale;
                    roueCollider.sidewaysFriction = sFrictionNormale;
                }

            }
        }
    }
    
}
