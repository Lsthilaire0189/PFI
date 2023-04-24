using System;
using System.Collections.Generic;
using UnityEngine;

public class DéplacementScript : MonoBehaviour
{
    
    private LogitechGSDK.DIJOYSTATE2ENGINES rec;

    [SerializeField] WheelCollider RoueAvantDroite;
    [SerializeField] WheelCollider RoueAvantGauche;
    [SerializeField] WheelCollider RoueArrièreDroite;
    [SerializeField] WheelCollider RoueArrièreGauche;

    public float ValeurAccélération = 0.05f;// Est modifié par Génération joueur
    public float ValeurForceFreinage = 0.2f;// Est modifiée par Génération joueur

    public float VitesseMaximum = 50f; // Est modifiée par Génération joueur

    public float ValeurAngleMaximum = 15f;

    private float Accélération;
    private float ForceFreinage;
    private float Angle;

    private float vRotation;

    private int direction=1;

    GestionEssence gestionEssence;

    //private List<WheelCollider> listeRoues;

    public GameObject goRoueAvantDroite;
    public GameObject goRoueAvantGauche;
    public GameObject goRoueArrGauche;
    public GameObject goRoueArrDroite;

    private void Awake()
    {

        rec = new LogitechGSDK.DIJOYSTATE2ENGINES();
        gestionEssence =gameObject.GetComponent<GestionEssence>();
        RoueAvantDroite.ConfigureVehicleSubsteps(5,12,15);
        RoueAvantGauche.ConfigureVehicleSubsteps(5,12,15);
        RoueArrièreDroite.ConfigureVehicleSubsteps(5,12,15);
        RoueArrièreGauche.ConfigureVehicleSubsteps(5,12,15);
    }

    private void FixedUpdate()
    {

        rec = LogitechGSDK.LogiGetStateUnity(0);

        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            direction = -1;
        }
        else if(Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            direction = 1;
        }

        Angle = ValeurAngleMaximum * Input.GetAxis("Horizontal");
        RoueAvantDroite.steerAngle = Angle;
        RoueAvantGauche.steerAngle = Angle;
        
        vRotation = RoueArrièreDroite.rpm * (360f / 60) * Time.deltaTime;
        goRoueAvantDroite.transform.Rotate(-vRotation,Angle,0);
        goRoueAvantGauche.transform.Rotate(vRotation,Angle,0);
        goRoueArrGauche.transform.Rotate(vRotation,0,0);
        goRoueArrDroite.transform.Rotate(-vRotation,0,0);


        if (gestionEssence.VérifierEssence())
        {
            if (rec.lY is < 32760 and > 0)
            {
                Accélération =
                    direction * Mathf.Log(ValeurAccélération * (32760f - rec.lY) / 32760f + 1,
                        2);

                RoueArrièreDroite.motorTorque += Accélération;
                RoueArrièreGauche.motorTorque += Accélération;
                RoueAvantDroite.motorTorque += Accélération;
                RoueAvantGauche.motorTorque += Accélération;
                //print("case 1 true");

            }
            else if (rec.lY < 0)
            {
                Accélération = direction * Mathf.Log(ValeurAccélération * (-rec.lY + 32760) / 32760f + 1, 2);


                RoueArrièreDroite.motorTorque += Accélération;
                RoueArrièreGauche.motorTorque += Accélération;
                RoueAvantDroite.motorTorque += Accélération;
                RoueAvantGauche.motorTorque += Accélération;
                //print("case 2 true");
            }



            if (rec.lRz is < 32760 and > 0 && RoueArrièreDroite.motorTorque > 0)
            {
                ForceFreinage = ValeurForceFreinage * (32760 - rec.lRz) / 32760f;
                RoueAvantDroite.motorTorque -= ForceFreinage;
                RoueAvantGauche.motorTorque -= ForceFreinage;
                RoueArrièreDroite.motorTorque -= ForceFreinage;
                RoueArrièreGauche.motorTorque -= ForceFreinage;
                //print("case 3 true");
            }
            else if (rec.lRz < 0 && RoueArrièreDroite.motorTorque > 0)
            {
                ForceFreinage = ValeurForceFreinage * (rec.lY + 32760) / 32760f;
                RoueAvantDroite.motorTorque -= ForceFreinage;
                RoueAvantGauche.motorTorque -= ForceFreinage;
                RoueArrièreDroite.motorTorque -= ForceFreinage;
                RoueArrièreGauche.motorTorque -= ForceFreinage;
            }


            if (RoueArrièreDroite.motorTorque > 0 && RoueArrièreGauche.motorTorque > 0 &&
                  RoueAvantDroite.motorTorque > 0 && RoueAvantGauche.motorTorque > 0 && rec.lY > 32760 && rec.lRz > 32760)
            {
                RoueArrièreDroite.motorTorque -= 0.03f;
                RoueArrièreGauche.motorTorque -= 0.03f;
                RoueAvantDroite.motorTorque -= 0.03f;
                RoueAvantGauche.motorTorque -= 0.03f;
                //print("case 4 true");
            }

        }
        else
        {
            RoueAvantDroite.motorTorque = 0;
            RoueAvantGauche.motorTorque = 0;
            RoueArrièreDroite.motorTorque = 0;
            RoueArrièreGauche.motorTorque = 0;
        }
    }

    

}
