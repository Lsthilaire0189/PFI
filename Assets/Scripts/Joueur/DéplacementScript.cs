using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DéplacementScript : MonoBehaviour
{
    static LogitechGSDK.DIJOYSTATE2ENGINES rec;
    
    [SerializeField] WheelCollider RoueAvantDroite;
    [SerializeField] WheelCollider RoueAvantGauche;
    [SerializeField] WheelCollider RoueArrièreDroite;
    [SerializeField] WheelCollider RoueArrièreGauche;
    public float ValeurAccélération = 500f;
    public float ValeurForceFreinage = 300f;
    public float ValeurAngleMaximum = 15f;

    private float Accélération = 0f;
    private float ForceFreinage = 0f;
    private float Angle = 0f;
    
    
    private void FixedUpdate()
    { 
    //{
    //    rec = LogitechGSDK.LogiGetStateUnity(0);
        
    //    Accélération = ValeurAccélération * (rec.lY/32760f);
    //    ForceFreinage = ValeurForceFreinage * (rec.lRz / 32760f);
    //    RoueAvantDroite.motorTorque = Accélération;
    //    RoueAvantGauche.motorTorque = Accélération;
    //    RoueAvantDroite.brakeTorque = ForceFreinage;
    //    RoueAvantGauche.brakeTorque= ForceFreinage;
    //    RoueArrièreDroite.brakeTorque = ForceFreinage;
    //    RoueArrièreGauche.brakeTorque= ForceFreinage;

    //    Angle = ValeurAngleMaximum * Input.GetAxis("Horizontal");
    //    RoueAvantDroite.steerAngle = Angle;
    //    RoueAvantGauche.steerAngle = Angle;

    }
}
