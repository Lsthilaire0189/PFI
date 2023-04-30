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

    public AudioSource sonAcceleration;
    public AudioSource sonStatique;

    public float ValeurAccélération = 0.05f;// Est modifié par Génération joueur
    public float ValeurForceFreinage = 0.2f;// Est modifiée par Génération joueur

    public float VitesseMaximale = 50f; // Est modifiée par Génération joueur

    public float ValeurAngleMaximum = 15f;

    private float Accélération;
    private float ForceFreinage;
    private float Angle;

    private float vRotation;

    private int sens=1;

    GestionEssence gestionEssence;

    private GestionVieJoueur gestionVieJoueur;

    private Rigidbody rbVoiture;

    //private List<WheelCollider> listeRoues;

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
            sens = -1;
        }
        else if(Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            sens = 1;
        }

        Angle = ValeurAngleMaximum * Input.GetAxis("Horizontal");
        RoueAvantDroite.steerAngle = Angle;
        RoueAvantGauche.steerAngle = Angle;
        
        vRotation = RoueArrièreDroite.rpm * (360f / 60) * Time.deltaTime;
        goRoueAvantDroite.transform.Rotate(-vRotation,Angle,0);
        goRoueAvantGauche.transform.Rotate(vRotation,Angle,0);
        goRoueArrGauche.transform.Rotate(vRotation,0,0);
        goRoueArrDroite.transform.Rotate(-vRotation,0,0);

        print(rbVoiture.velocity.magnitude < VitesseMaximale);

        if (gestionEssence.VérifierEssence() && gestionVieJoueur.VérifierVieJoueur())
        {
            if (rbVoiture.velocity.magnitude <= VitesseMaximale)
            {
                if (rec.lY is < 32760 and > 0)
                {
                    Accélération =
                        sens * Mathf.Log(ValeurAccélération * (32760f - rec.lY) / 32760f + 1,
                            2);

                    RoueArrièreDroite.motorTorque += Accélération;
                    RoueArrièreGauche.motorTorque += Accélération;
                    RoueAvantDroite.motorTorque += Accélération;
                    RoueAvantGauche.motorTorque += Accélération;
                    sonStatique.Pause();
                    sonAcceleration.PlayDelayed(1);
                    //print("case 1 true");

                }
                else if (rec.lY < 0)
                {
                    Accélération = sens * Mathf.Log(ValeurAccélération * (-rec.lY + 32760) / 32760f + 1, 2);


                    RoueArrièreDroite.motorTorque += Accélération;
                    RoueArrièreGauche.motorTorque += Accélération;
                    RoueAvantDroite.motorTorque += Accélération;
                    RoueAvantGauche.motorTorque += Accélération;
                    sonAcceleration.Pause();
                    sonAcceleration.PlayDelayed(1);
                    //print("case 2 true");
                }
            }



            if (rec.lRz is < 32760 and > 0 && RoueArrièreDroite.motorTorque > 0)
            {
                ForceFreinage = ValeurForceFreinage * (32760 - rec.lRz) / 32760f;
                RoueAvantDroite.motorTorque -= ForceFreinage;
                RoueAvantGauche.motorTorque -= ForceFreinage;
                RoueArrièreDroite.motorTorque -= ForceFreinage;
                RoueArrièreGauche.motorTorque -= ForceFreinage;
                //print("case 3 true");
                sonAcceleration.Pause();
                sonStatique.Play();
            }
            else if (rec.lRz < 0 && RoueArrièreDroite.motorTorque > 0)
            {
                ForceFreinage = ValeurForceFreinage * (rec.lY + 32760) / 32760f;
                RoueAvantDroite.motorTorque -= ForceFreinage;
                RoueAvantGauche.motorTorque -= ForceFreinage;
                RoueArrièreDroite.motorTorque -= ForceFreinage;
                RoueArrièreGauche.motorTorque -= ForceFreinage;
                sonAcceleration.Pause();
                sonStatique.Play();
            }


            if (RoueArrièreDroite.motorTorque > 0 && RoueArrièreGauche.motorTorque > 0 &&
                  RoueAvantDroite.motorTorque > 0 && RoueAvantGauche.motorTorque > 0 && rec.lY > 32760 && rec.lRz > 32760)
            {
                RoueArrièreDroite.motorTorque -= sens*0.03f;
                RoueArrièreGauche.motorTorque -= sens*0.03f;
                RoueAvantDroite.motorTorque -= sens*0.03f;
                RoueAvantGauche.motorTorque -= sens*0.03f;
                //print("case 4 true");
                sonAcceleration.Pause();
                sonStatique.Play();

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
