using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AIDéplacement : MonoBehaviour
{

    List<Vector3> chemins;
    Vector3 destination;
    [SerializeField] WheelCollider RoueAvantDroite;
    [SerializeField] WheelCollider RoueAvantGauche;
    [SerializeField] WheelCollider RoueArrièreDroite;
    [SerializeField] WheelCollider RoueArrièreGauche;
    public float ValeurAccélération = 500f;
    public float ValeurForceFreinage = 300f;
    public float Angle = 15f;
    public float Rayon = .1f;
    Vector3 PointSuivant;
    int direction;
    int index;
    [SerializeField] GameObject o;
    // Start is called before the first frame update
    void Awake()
    {
        index = 1;
        var creerCarte = gameObject.GetComponentInParent<CréerCarte>();
        destination = creerCarte.Destination.transform.position;
        chemins = creerCarte.chemin;
        PointSuivant = chemins[index];
        Instantiate(o, PointSuivant, Quaternion.identity);
        VérifierDirection();
    }
    private void FixedUpdate()
    {         
        float acceleration = ValeurAccélération;
        RoueAvantDroite.motorTorque= acceleration;
        RoueAvantGauche.motorTorque = acceleration;
        Direction();
        if (Vector3.Distance(transform.position,PointSuivant ) <=0.1f)
        {
            index++;
            //RoueAvantDroite.motorTorque = 0;
            //RoueAvantGauche.motorTorque = 0;
            //RoueAvantGauche.brakeTorque = 100f;
            //RoueAvantDroite.brakeTorque = 100f;
            PointSuivant = chemins[index];
            Debug.Log(1);
        }
    }
    void Direction()
    {
        Vector3 directionPointSuivant = transform.InverseTransformPoint(PointSuivant);
        directionPointSuivant /= directionPointSuivant.magnitude;
        float nouvelleDirection = (directionPointSuivant.x / directionPointSuivant.magnitude);
        if (nouvelleDirection > 0)
        {
            RoueAvantGauche.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (Rayon + (1.5f / 2))) * nouvelleDirection;
            RoueAvantDroite.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (Rayon - (1.5f / 2))) * nouvelleDirection;
        }
        else if (nouvelleDirection < 0)
        {
            RoueAvantGauche.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (Rayon - (1.5f / 2))) * nouvelleDirection;
            RoueAvantDroite.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (Rayon + (1.5f / 2))) * nouvelleDirection;
        }
        else
        {
            RoueAvantDroite.steerAngle = 0;
            RoueAvantGauche.steerAngle = 0;
        }

    }
    // Update is called once per frame
    //void Update()
    //{
    //    Vector3 Départ = gameObject.transform.position;
    //    int i = 1;
    //    PointSuivant = chemins[i];
    //    direction = VérifierDirection(); // 0 nord, 1 sud, 2 est, 3 ouest
    //    while (transform.position != destination)
    //    {
    //        int directionSuivante = VérifierDirection();
    //        if (direction != directionSuivante)
    //        {
    //            if (direction == 0)
    //            {
    //                if (directionSuivante == 2)
    //                {
    //                    RoueAvantDroite.steerAngle = Angle;
    //                    RoueAvantGauche.steerAngle = Angle;
    //                }
    //                else
    //                {
    //                    RoueAvantDroite.steerAngle = -Angle;
    //                    RoueAvantGauche.steerAngle = -Angle;
    //                }
    //            }
    //            if (direction == 1)
    //            {
    //                if (directionSuivante == 3)
    //                {
    //                    RoueAvantDroite.steerAngle = Angle;
    //                    RoueAvantGauche.steerAngle = Angle;
    //                }
    //                else
    //                {
    //                    RoueAvantDroite.steerAngle = -Angle;
    //                    RoueAvantGauche.steerAngle = -Angle;
    //                }
    //            }
    //            if (direction == 2)
    //            {
    //                if (directionSuivante == 0)
    //                {
    //                    RoueAvantDroite.steerAngle = Angle;
    //                    RoueAvantGauche.steerAngle = Angle;
    //                }
    //                else
    //                {
    //                    RoueAvantDroite.steerAngle = -Angle;
    //                    RoueAvantGauche.steerAngle = -Angle;
    //                }
    //            }
    //            if (direction == 3)
    //            {
    //                if (directionSuivante == 1)
    //                {
    //                    RoueAvantDroite.steerAngle = Angle;
    //                    RoueAvantGauche.steerAngle = Angle;
    //                }
    //                else
    //                {
    //                    RoueAvantDroite.steerAngle = -Angle;
    //                    RoueAvantGauche.steerAngle = -Angle;
    //                }
    //            }
    //        }
    //        if (transform.position.x == PointSuivant.x || transform.position.z == PointSuivant.z)
    //        {
    //            RoueAvantDroite.steerAngle = 0;
    //            RoueAvantGauche.steerAngle = 0;
    //        }
    //        if (transform.position != PointSuivant)
    //        {
    //            float Accélération = ValeurAccélération * 0.5f;
    //            float ForceFreinage = 0;
    //            RoueAvantDroite.motorTorque = Accélération;
    //            RoueAvantGauche.motorTorque = Accélération;
    //            RoueAvantDroite.brakeTorque = ForceFreinage;
    //            RoueAvantGauche.brakeTorque = ForceFreinage;
    //            RoueArrièreDroite.brakeTorque = ForceFreinage;
    //            RoueArrièreGauche.brakeTorque = ForceFreinage;
    //        }
    //        else
    //        {
    //            PointSuivant = chemins[i + 1];
    //        }
    //    }



    int VérifierDirection()
    {

        if (transform.position.z < PointSuivant.z)
        {
            transform.position += new Vector3(0.1f, 0, 0);
            Debug.Log(0);
            return 0;
        }
        if (transform.position.z > PointSuivant.z)
        {
            transform.position -= new Vector3(0.1f, 0, 0);
            transform.Rotate(0, 180, 0);
            Debug.Log(1);
            return 1;
        }
        if (transform.position.x <PointSuivant.x)
        {
            transform.position -= new Vector3(0, 0, 0.1f);
            transform.Rotate(0, 90, 0);
            Debug.Log(2);
            return 2;
        }
        if (transform.position.x > PointSuivant.x)
        {
            transform.position += new Vector3(0, 0, 0.1f);
            transform.Rotate(0,-90,0);
            Debug.Log(3);
            return 3;
        }
        return direction;
    }
}
