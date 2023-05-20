using System;
using System.Collections.Generic;
using UnityEngine;

public class DéplacementScript : MonoBehaviour
{
    private LogitechGSDK.DIJOYSTATE2ENGINES rec;

    public List<WheelCollider> listeWC;
    
    public float ValeurAccélération = 0.05f;
    public float ValeurForceFreinage = 0.2f;
    public float VitesseMaximale = 50f;
    public float ValeurAngleMaximum = 15f;

    private float Accélération;
    private float ForceFreinage;
    private float Angle;
    private float vRotation;
    private int sens=1;

    GestionEssence gestionEssence;
    GestionVieJoueur gestionVieJoueur;
    Rigidbody rbVoiture;
    
    public GameObject goRoueAvantDroite;
    public GameObject goRoueAvantGauche;
    public GameObject goRoueArrGauche;
    public GameObject goRoueArrDroite;

    private void Awake()
    {
        rec = new LogitechGSDK.DIJOYSTATE2ENGINES();
        gestionEssence =gameObject.GetComponent<GestionEssence>();
        gestionVieJoueur = gameObject.GetComponent<GestionVieJoueur>();
        rbVoiture = gameObject.GetComponent<Rigidbody>();
        listeWC[0].ConfigureVehicleSubsteps(5,12,15);//on fait WheelCollider.ConfigureVehiculeSubsteps pour modifier la suspension 
        listeWC[1].ConfigureVehicleSubsteps(5,12,15);//du WheelCollider afin de faire en sorte que la voiture du joueur de tremble pas
        listeWC[2].ConfigureVehicleSubsteps(5,12,15);//lorsqu'elle n'est pas en mouvement.
        listeWC[3].ConfigureVehicleSubsteps(5,12,15);
    }

    private void FixedUpdate()
    {
        rec = LogitechGSDK.LogiGetStateUnity(0);//permet de lire l'état du volant et des pédales unity

        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            sens = -1;//si le joueur appuie sur le shifter gauche du volant Logitech, son déplacement va se faire vers le sens inverse lorsqu'il ajoute du torque au WheelColliders des roues avec les pédales.
            //Ceci fonctionne comme la marche arrière d'une voiture
        }
        else if(Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            sens = 1;//si le joueur appuie sur le shifter droit du volant Logitech, ceci met la voiture en marche avant
        }

        Angle = ValeurAngleMaximum * Input.GetAxis("Horizontal");//on lit la position horizontale du volant pour donner ensuite donner un angle de direction aux roues avants. Ceci permet de faire tourner la voiture
        listeWC[0].steerAngle = Angle;
        listeWC[1].steerAngle = Angle;
        
        vRotation = listeWC[0].rpm * (360f / 60) * Time.deltaTime;//on fait tourner les GameObjects qui représentent les roues de la voiture afin de leur donner une animation de rotation
                                                                        // qui dépend de la rotation par minute du WheelCollider qui leur est associée.
        goRoueAvantDroite.transform.Rotate(-vRotation,Angle,0);
        goRoueAvantGauche.transform.Rotate(vRotation,Angle,0);
        goRoueArrGauche.transform.Rotate(vRotation,0,0);
        goRoueArrDroite.transform.Rotate(-vRotation,0,0);
        
        if (gestionEssence.VérifierEssence() && gestionVieJoueur.VérifierVieJoueur())//on vérifie si le joueur peut toujours continuer
        {
            if (rbVoiture.velocity.magnitude*100 <= VitesseMaximale)//on vérifie si la vitesse de la voiture est en dessous de la vitesse maximale possible de la voiture
            {
                if (rec.lY is < 32760 and > 0)//rec.lY représente la position de la pédale d'accélération, allant de -32760 à 32760
                {
                    Accélération =
                        sens * Mathf.Log(ValeurAccélération * (32760f - rec.lY) / 32760f + 1, //on divise par 32760 pour faire en sorte que la position de la pédale lue varie entre -1 et 1 au lieu de 32760 à 32760, facilitant son utilisation dans les calculs
                            2);//on utilise Mathf.Log afin de caluler la quantité de torque à ajouter aux WheelCollders puisque ceci permet de représenter la courbe de progression de la quantité de torque
                    //des roues d'une voiture possèdant un moteur à combustion interne en vraie vie lorsque la voiture accélère
                    foreach (var roueCollider in listeWC)
                    {
                        roueCollider.motorTorque += Accélération;
                    }
                }
                else if (rec.lY < 0)
                {
                    Accélération = sens * Mathf.Log(ValeurAccélération * (-rec.lY + 32760) / 32760f + 1, 2);
                    foreach (var roueCollider in listeWC)
                    {
                        roueCollider.motorTorque += Accélération;
                    }
                }
            }
            
            if (rec.lRz is < 32760 and > 0 && listeWC[0].motorTorque > 0)
            {
                ForceFreinage = ValeurForceFreinage * (32760 - rec.lRz) / 32760f;
                foreach (var roueCollder in listeWC)
                {
                    roueCollder.motorTorque -= ForceFreinage;
                }
            }
            else if (rec.lRz < 0 && listeWC[0].motorTorque > 0)
            {
                ForceFreinage = ValeurForceFreinage * (rec.lY + 32760) / 32760f;
                foreach (var roueCollider in listeWC)
                {
                    roueCollider.motorTorque -= ForceFreinage;
                }
            }
            //si le joueur n'appuie sur aucune pédale et que la voiture est en mouvement,
            //la quantité de torque sur les WheelColliders diminue petit à petit afin de donner l'impression que la résistance de l'air agit sur la voiture.
            if (listeWC[0].motorTorque > 0 && listeWC[1].motorTorque > 0 &&
                listeWC[2].motorTorque > 0 && listeWC[3].motorTorque > 0 && rec.lY > 32760 && rec.lRz > 32760)
            {
                foreach (var roueCollider in listeWC)
                {
                    roueCollider.motorTorque -= sens * 0.03f;
                }
            }
        }
        else //si la voiture ne possède plus d'essence ou de vie, la quantité de torque des WheelColliders est de 0, immobilisant ainsi la voiture du joueur
        {
            foreach (var listeCollider in listeWC)
            {
                listeCollider.motorTorque = 0;
            }
        }
    }
}
