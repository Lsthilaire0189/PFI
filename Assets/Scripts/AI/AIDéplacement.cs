using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// a faire la rotation utiliser les vecteurs utiliser fonction spéciale juste aux intersectiosn
public class AIDéplacement : MonoBehaviour
{
    AlgoDeRecherche algoDeRecherche;

    GameObject[,] Carte;
    List<GameObject> ListePoints;

    const float Largeurvoie = .1f;
    public List<Vector3> chemins;
    GameObject Départ, Destination;

    Vector3 PointSuivant;
    Vector3 vecteur1;
    float angle;
    float angletarget;

    bool intersection;
    bool intersectionp2;
    int index;
    int directionActuelle = 0;
    float time = 0;
    float t;

    float vitesse = 0.004f;
    float vitesseMax = 0.004f;
    float vitesseMin = 0.002f;
    float incrémentvitesse = 0.001f;
    // Start is called before the first frame update
    void Awake()
    {

        algoDeRecherche = gameObject.GetComponent<AlgoDeRecherche>();
        SceneManagerScript s = gameObject.GetComponentInParent<SceneManagerScript>();
        Carte = s.Carte;
        ListePoints = s.ListePoints;
        TrouverDestination();
    }
    void TrouverDestination()
    {
        int dp = UnityEngine.Random.Range(0, ListePoints.Count);
        int ds = UnityEngine.Random.Range(0, ListePoints.Count);
        Départ = ListePoints[dp];
        Destination = ListePoints[ds];

        transform.position = Départ.transform.position + Vector3.up;
        chemins = algoDeRecherche.AlgoDijkstra(Carte, Départ, Destination);
        index = 0;
        PointSuivant = chemins[++index];
        if (chemins.Count < 2)
        {
            TrouverDestination();
        }
        InitiationNPC();
    }
    void InitiationNPC()
    {
        if ((PointSuivant.z - transform.position.z) < -0.7f)
        {
            transform.Rotate(0, 180, 0);
            directionActuelle = 1;
            PointSuivant -= new Vector3(Largeurvoie, 0, 0);
        }
        if ((PointSuivant.x - transform.position.x) > 0.7f)
        {
            transform.Rotate(0, 90, 0);
            directionActuelle = 0;
            PointSuivant -= new Vector3(0, 0, Largeurvoie);
        }
        if ((PointSuivant.x - transform.position.x) < -0.7f)
        {
            transform.Rotate(0, -90, 0);
            directionActuelle = 0;
            PointSuivant += new Vector3(0, 0, Largeurvoie);
        }
        else
        {
            PointSuivant += new Vector3(Largeurvoie, 0, 0);
        }
    }
    private void FixedUpdate()
    {
        //print(vitesse);
        if (intersection)
        {
            vitesse = Mathf.MoveTowards(vitesse, vitesseMin, incrémentvitesse * Time.deltaTime);
        }
        if (intersectionp2)
        {
            vitesse = Mathf.MoveTowards(vitesse, vitesseMax, incrémentvitesse * Time.deltaTime);
        }
        transform.position = Vector3.MoveTowards(transform.position, PointSuivant, vitesse);

        if (Mathf.Abs(angletarget) - Mathf.Abs(angle) > 5)
        {
            angle += 0.7f;
            transform.Rotate(0, 0.7f * Mathf.Sign(angletarget), 0);
        }

        if (Vector3.Magnitude(PointSuivant - transform.position) < 0.01f)
        {
            if (index == chemins.Count - 2)
            {
                TrouverDestination();
            }
            else
            {
                intersectionp2 = false;
                if (intersection)
                {
                    intersection = false;
                    intersectionp2 = true;
                }
                index++;
                PointSuivant = chemins[index];
                Vector3 vecteur = PointSuivant - transform.position;
                angletarget = Vector3.SignedAngle(transform.forward, vecteur, Vector3.up);
                angle = 0;
                time = 0;
                if (VérifierDirection(PointSuivant, transform.position) == 0)
                {
                    PointSuivant -= new Vector3(0, 0, Largeurvoie) * MathF.Sign(vecteur1.x);
                    directionActuelle = 0;
                }
                else
                {
                    PointSuivant += new Vector3(Largeurvoie, 0, 0) * MathF.Sign(vecteur1.z);
                    directionActuelle = 1;
                }
                if (VérifierDirection(chemins[index + 1], PointSuivant) != directionActuelle)
                {
                    intersection = true;
                    print(intersection);
                }
            }
        }
    }


    int VérifierDirection(Vector3 prochainPoint, Vector3 positionActuelle) // utiliser vecteurs 
    {
        vecteur1 = prochainPoint - positionActuelle;
        float directionx = MathF.Abs(vecteur1.x);
        float directionz = MathF.Abs(vecteur1.z);
        if (directionx > directionz)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    //    intersection = false;
    //        Vector3 vecteur1 = PointSuivant - transform.position;
    //    float directionx = MathF.Abs(vecteur1.x);
    //    float directionz = MathF.Abs(vecteur1.z);
    //    int ndirection;
    //        if (directionx > directionz)
    //        {
    //            PointSuivant -= new Vector3(0, 0, 0.1f) * MathF.Sign(vecteur1.x);

    //            ndirection = 0;
    //        }
    //        else
    //{
    //    PointSuivant += new Vector3(0.1f, 0, 0) * MathF.Sign(vecteur1.z);

    //    ndirection = 1;
    //}

    //if (ndirection != direction)
    //{
    //    intersection = true;
    //    Debug.Log("Intersection");
    //    direction = ndirection;
    //}
    //Debug.Log(PointSuivant);

    //if ((PointSuivant.z - transform.position.z) > 0.7f) // vérifier la direction
    //{
    //    if (Initiation)
    //    {
    //        transform.position += new Vector3(0.1f, 0, 0);
    //        Initiation = false;
    //    }
    //    //if (Mathf.Abs(chemins[index + 1].x - PointSuivant.x) > 0.90f)
    //    //{

    //    //    //PointSuivant -= new Vector3(0, 0, 0.5f);

    //    //}

    //    PointSuivant += new Vector3(0.1f, 0, 0);
    //    Debug.Log(0);

    //}
    //if ((PointSuivant.z - transform.position.z) < -0.7f)
    //{
    //    if (Initiation)
    //    {
    //        transform.position -= new Vector3(0.1f, 0, 0);
    //        transform.Rotate(0, 180, 0);
    //        Initiation = false;
    //    }
    //    //if (Mathf.Abs(chemins[index + 1].x - PointSuivant.x) > 0.90f)
    //    //{
    //    //    PointSuivant += new Vector3(0, 0, 0.5f);

    //    //}
    //    PointSuivant -= new Vector3(0.1f, 0, 0);
    //    Debug.Log(1);
    //}
    //if ((PointSuivant.x - transform.position.x) > 0.7f)
    //{
    //    if (Initiation)
    //    {
    //        transform.position -= new Vector3(0, 0, 0.1f);
    //        transform.Rotate(0, 90, 0);
    //        Initiation = false;
    //    }
    //    //if (Mathf.Abs(chemins[index + 1].z - PointSuivant.z) > 0.90f)
    //    //{
    //    //    PointSuivant -= new Vector3(0.5f, 0, 0);

    //    //}
    //    PointSuivant -= new Vector3(0, 0, 0.1f);
    //    Debug.Log(2);

    //}
    //if ((PointSuivant.x - transform.position.x) < -0.7f)
    //{
    //    if (Initiation)
    //    {
    //        transform.position += new Vector3(0, 0, 0.1f);
    //        transform.Rotate(0, -90, 0);
    //        Initiation = false;
    //    }
    //    //if (Mathf.Abs(chemins[index + 1].z - PointSuivant.z) > 0.90f)
    //    //{
    //    //    PointSuivant += new Vector3(0.5f, 0, 0);

    //    //}
    //    PointSuivant += new Vector3(0, 0, 0.1f);
    //    Debug.Log(3);
    //}
    //if (Initiation)
    //{
    //    angle = transform.rotation.eulerAngles.y;
    //}        //if ((PointSuivant.z - transform.position.z) > 0.7f) // vérifier la direction
    //{
    //    if (Initiation)
    //    {
    //        transform.position += new Vector3(0.1f, 0, 0);
    //        Initiation = false;
    //    }
    //    //if (Mathf.Abs(chemins[index + 1].x - PointSuivant.x) > 0.90f)
    //    //{

    //    //    //PointSuivant -= new Vector3(0, 0, 0.5f);

    //    //}

    //    PointSuivant += new Vector3(0.1f, 0, 0);
    //    Debug.Log(0);

    //}
    //if ((PointSuivant.z - transform.position.z) < -0.7f)
    //{
    //    if (Initiation)
    //    {
    //        transform.position -= new Vector3(0.1f, 0, 0);
    //        transform.Rotate(0, 180, 0);
    //        Initiation = false;
    //    }
    //    //if (Mathf.Abs(chemins[index + 1].x - PointSuivant.x) > 0.90f)
    //    //{
    //    //    PointSuivant += new Vector3(0, 0, 0.5f);

    //    //}
    //    PointSuivant -= new Vector3(0.1f, 0, 0);
    //    Debug.Log(1);
    //}
    //if ((PointSuivant.x - transform.position.x) > 0.7f)
    //{
    //    if (Initiation)
    //    {
    //        transform.position -= new Vector3(0, 0, 0.1f);
    //        transform.Rotate(0, 90, 0);
    //        Initiation = false;
    //    }
    //    //if (Mathf.Abs(chemins[index + 1].z - PointSuivant.z) > 0.90f)
    //    //{
    //    //    PointSuivant -= new Vector3(0.5f, 0, 0);

    //    //}
    //    PointSuivant -= new Vector3(0, 0, 0.1f);
    //    Debug.Log(2);

    //}
    //if ((PointSuivant.x - transform.position.x) < -0.7f)
    //{
    //    if (Initiation)
    //    {
    //        transform.position += new Vector3(0, 0, 0.1f);
    //        transform.Rotate(0, -90, 0);
    //        Initiation = false;
    //    }
    //    //if (Mathf.Abs(chemins[index + 1].z - PointSuivant.z) > 0.90f)
    //    //{
    //    //    PointSuivant += new Vector3(0.5f, 0, 0);

    //    //}
    //    PointSuivant += new Vector3(0, 0, 0.1f);
    //    Debug.Log(3);
    //}
    //if (Initiation)
    //{
    //    angle = transform.rotation.eulerAngles.y;
    //}

    //Vector3 PointSuivantSuivant = chemins[index + 1];
    //Vector3 v1 = PointSuivant - PointActuel;
    //Vector3 v2 = PointSuivantSuivant - PointActuel;

    //float angle1 = Vector3.Angle(PointActuel, PointSuivant);
    //float angle2 = Vector3.Angle(PointActuel, PointSuivantSuivant);

    //Vector3 v1v2 = Vector3.Cross(v1, v2);

    //if (v1v2 == Vector3.zero)
    //{

    //}
    //else
    //{
    //    if ((angle2 - angle1) > 0)
    //    {
    //        PointSuivant1 = PointActuel + (PointSuivant - PointActuel) / 2;
    //        Pointsuivant2 = PointSuivantSuivant - (PointSuivantSuivant - PointSuivant) / 2;
    //    }
    //}
    //float angle = Vector3.Angle(PointActuel, PointSuivant);
    //PointSuivant.x = PointSuivant.x + (Largeur / 4) * Mathf.Sin(Mathf.Deg2Rad * angle);
    //PointSuivant.z = PointSuivant.z + (Largeur / 4) * Mathf.Cos(Mathf.Deg2Rad * angle);
}


