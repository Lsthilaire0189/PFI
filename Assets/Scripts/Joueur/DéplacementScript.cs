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
    public float ValeurAccélération = 500f; // Est modifié par Génération joueur
    public float VitesseMaximum = 50f; // Est modifiée par Génération joueur
    public float ValeurForceFreinage = 300f;
    public float ValeurAngleMaximum = 15f;

    private float Accélération = 0f;
    private float ForceFreinage = 0f;
    private float Angle = 0f;


    public bool peutAvancer;


    private void FixedUpdate()
    {
        rec = LogitechGSDK
            .LogiGetStateUnity(
                0); //qd on augmente l'acceleration la base du log diminue, puis en meme temps on peut upgrade la vitesse maximale qu'on met dans le if statement
        if (peutAvancer)
        {
            if (rec.lY is < 32760 and > 0)
            {
                Accélération =
                    Mathf.Log(ValeurAccélération * (32760f - rec.lY) / 32760f + 1,
                        2); //chercher une fonction qui permet de faire en sorte l'acceleration se fasse graduellement 

                RoueArrièreDroite.motorTorque += Accélération;
                RoueArrièreGauche.motorTorque += Accélération;
                RoueAvantDroite.motorTorque += Accélération;
                RoueAvantGauche.motorTorque += Accélération;
                print("case 1 true");

            }
            else if (rec.lY < 0)
            {
                Accélération = Mathf.Log(ValeurAccélération * (-rec.lY + 32760) / 32760f + 1, 2);

                RoueArrièreDroite.motorTorque += Accélération;
                RoueArrièreGauche.motorTorque += Accélération;
                RoueAvantDroite.motorTorque += Accélération;
                RoueAvantGauche.motorTorque += Accélération;
                print("case 2 true");
            }

            if (RoueArrièreDroite.motorTorque > 0 && RoueArrièreGauche.motorTorque > 0 &&
                     RoueAvantDroite.motorTorque > 0 && RoueAvantGauche.motorTorque > 0)
            {
                RoueArrièreDroite.motorTorque -= 0.03f;
                RoueArrièreGauche.motorTorque -= 0.03f;
                RoueAvantDroite.motorTorque -= 0.03f;
                RoueAvantGauche.motorTorque -= 0.03f;
                print("case 3 true");
            }

            /*if (rec.lRz is < 32760 and > 0 && RoueArrièreDroite.motorTorque > 0)
            {
                ForceFreinage = ValeurForceFreinage * (32760-rec.lRz)/-32760f;
                RoueAvantDroite.motorTorque -= ForceFreinage;
                RoueAvantGauche.motorTorque -= ForceFreinage;
                RoueArrièreDroite.motorTorque -= ForceFreinage;
                RoueArrièreGauche.motorTorque -= ForceFreinage;
                print("case 4 true");
            }
            else if (rec.lRz < 0 && RoueArrièreDroite.motorTorque > 0)
            {
                ForceFreinage = ValeurForceFreinage * rec.lRz/-32760f;
                RoueAvantDroite.motorTorque -= ForceFreinage;
                RoueAvantGauche.motorTorque -= ForceFreinage;
                RoueArrièreDroite.motorTorque -= ForceFreinage;
                RoueArrièreGauche.motorTorque -= ForceFreinage;
            }*/

            Angle = ValeurAngleMaximum * Input.GetAxis("Horizontal");
            RoueAvantDroite.steerAngle = Angle;
            RoueAvantGauche.steerAngle = Angle;
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
