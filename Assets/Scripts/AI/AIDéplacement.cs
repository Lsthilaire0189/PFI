using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
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
    Vector3 PointSuivant;
    int direction;
    // Start is called before the first frame update
    void Awake()
    {

        var creerCarte = gameObject.GetComponentInParent<CréerCarte>();
        destination = creerCarte.Destination.transform.position;
        chemins = creerCarte.chemin;
        PointSuivant = chemins[1];
    }
    private void FixedUpdate()
    {
        int i = 1;
        Vector3 currentDirection = transform.forward;

        // calculate the angle between current direction and target direction
        float angle = Vector3.SignedAngle(currentDirection, PointSuivant, transform.up);

        // calculate the steer angle based on the angle and maximum steer angle
        float steerAngle = Mathf.Clamp(angle, -Angle,+Angle);
        float Accélération = ValeurAccélération * 0.5f;
        RoueAvantDroite.motorTorque = Accélération;
        RoueAvantGauche.motorTorque = Accélération;
        if (transform.position == PointSuivant)
        {
            PointSuivant = chemins[i + 1];
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
            return 0;
        }
        if (transform.position.z > PointSuivant.z)
        {
            return 1;
        }
        if (transform.position.x <PointSuivant.x)
        {
            return 2;
        }
        if (transform.position.x > PointSuivant.x)
        {
            return 3;
        }
        return direction;
    }
}
