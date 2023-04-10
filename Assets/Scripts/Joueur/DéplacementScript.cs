using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
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

    public bool peutAvancer;
    
    
    private void FixedUpdate()
    {

        if (peutAvancer == true)
        {
            Accélération = ValeurAccélération * Input.GetAxis("Vertical"); //ValeurAccélération * (rec.lY/32760f);
            ForceFreinage = ValeurForceFreinage * Input.GetAxis("Fire1");
            RoueAvantDroite.motorTorque = Accélération;
            RoueAvantGauche.motorTorque = Accélération;
            RoueAvantDroite.brakeTorque = ForceFreinage;
            RoueAvantGauche.brakeTorque = ForceFreinage;
            RoueArrièreDroite.brakeTorque = ForceFreinage;
            RoueArrièreGauche.brakeTorque = ForceFreinage;

            Angle = ValeurAngleMaximum * Input.GetAxis("Horizontal");
            RoueAvantDroite.steerAngle = Angle;
            RoueAvantGauche.steerAngle = Angle;
        }
        else
        {
            RoueAvantDroite.motorTorque = 0;
            RoueAvantGauche.motorTorque = 0;
        }


    }

    public static float GetAxis(string axisName)
    {
        rec = LogitechGSDK.LogiGetStateUnity(0);
        switch (axisName)
        {
            case "Steering Horizontal" : return rec.lX / 32760f;
            case "Gas Vertical": return rec.lY / -32760f;
            case "Clutch Vertical" : return rec.rglSlider[0] / -32760f;
            case "Brake Vertical" : return rec.lRz / -32760f;
        }

        return 0f;
    }
    
    
}
