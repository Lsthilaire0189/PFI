using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DéplacementScript : MonoBehaviour
{
   // LogitechGSDK.LogiControllerPropertiesData properties;
    
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
        Accélération = ValeurAccélération * Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.Space))
            ForceFreinage = ValeurForceFreinage;
        else
            ForceFreinage = 0f;
        RoueAvantDroite.motorTorque = Accélération;
        RoueAvantGauche.motorTorque = Accélération;
        RoueAvantDroite.brakeTorque = ForceFreinage;
        RoueAvantGauche.brakeTorque= ForceFreinage;
        RoueArrièreDroite.brakeTorque = ForceFreinage;
        RoueArrièreGauche.brakeTorque= ForceFreinage;

        Angle = ValeurAngleMaximum * Input.GetAxis("Horizontal");
        RoueAvantDroite.steerAngle = Angle;
        RoueAvantGauche.steerAngle = Angle;

    }
}
