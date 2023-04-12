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
    bool Initiation;
    int index;
    float time = 0;
    [SerializeField] GameObject o;
    // Start is called before the first frame update
    void Awake()
    {
        index = 1;
        var creerCarte = gameObject.GetComponentInParent<CréerCarte>();
        destination = creerCarte.Destination.transform.position;
        chemins = creerCarte.chemin;
        PointSuivant = chemins[index];
        Initiation = true;
        VérifierDirection();
        //Instantiate(o, PointSuivant, Quaternion.identity);

    }
    private void FixedUpdate()
    {
        time += Time.deltaTime;
        float t = time / 50;
        t = t * t * (3f - 2f * t);
        transform.position = Vector3.Lerp(transform.position, PointSuivant, t);
        if (Vector3.Magnitude(PointSuivant - transform.position) < 0.01f)
        {
            index++;
            PointSuivant = chemins[index];
            VérifierDirection();
            time = 0;
        }
    }


    void VérifierDirection()
    {
        Debug.Log(PointSuivant);

        if ((PointSuivant.z-transform.position.z)>0.7f) // vérifier la direction
        {
            if (Initiation)
            {
                transform.position += new Vector3(0.1f, 0, 0);
                Initiation = false;
            }
            if (Mathf.Abs(chemins[index + 1].x - PointSuivant.x) > 0.90f)
            {
                PointSuivant  -= new Vector3(0, 0, 0.5f);
            }

            PointSuivant += new Vector3(0.1f, 0, 0);
            Debug.Log(0);

        }
        if ((PointSuivant.z - transform.position.z) < -0.7f)
        {
            if (Initiation)
            {
                transform.position -= new Vector3(0.1f, 0, 0);
                transform.Rotate(0, 180, 0);
                Initiation = false;
            }
            if (Mathf.Abs(chemins[index + 1].x - PointSuivant.x)>0.90f)
            { 
                PointSuivant += new Vector3(0, 0, 0.5f);
            }
            PointSuivant -= new Vector3(0.1f, 0, 0);
            Debug.Log(1);
        }
        if ((PointSuivant.x - transform.position.x) > 0.7f)
        {
            if (Initiation)
            {
                transform.position -= new Vector3(0, 0, 0.1f);
                transform.Rotate(0, 90, 0);
                Initiation = false;
            }
            if (Mathf.Abs(chemins[index + 1].z - PointSuivant.z) > 0.90f)
            {
                PointSuivant -= new Vector3(0.5f, 0, 0);
            }
            PointSuivant -= new Vector3(0, 0, 0.1f);
            Debug.Log(2);

        }
        if ((PointSuivant.x - transform.position.x) < - 0.7f)
        {
            if (Initiation)
            {
                transform.position += new Vector3(0, 0, 0.1f);
                transform.Rotate(0, -90, 0);
                Initiation = false;
            }
            if (Mathf.Abs(chemins[index + 1].z - PointSuivant.z) > 0.90f)
            {
                PointSuivant += new Vector3(0.5f, 0, 0);
            }
            PointSuivant += new Vector3(0, 0, 0.1f);
            Debug.Log(3);
        }
    }
}
