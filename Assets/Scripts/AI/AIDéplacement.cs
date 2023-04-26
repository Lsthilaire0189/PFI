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

    const float Largeur = .014f;
    public List<Vector3> chemins;
    GameObject Départ, Destination;
    Vector3 destination;

    Vector3 PointPrécédent;
    Vector3 PointSuivant;
    float angle;
    Vector3 PointSuivant1, Pointsuivant2;

    bool Initiation = true;
    int index;
    float time = 0;
    float t;
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
        destination = Destination.transform.position;
        chemins = algoDeRecherche.AlgoDijkstra(Carte, Départ, Destination);
        index = 0;
        PointPrécédent = chemins[index];
        PointSuivant = chemins[++index];

        VérifierDirection();


    }
    private void FixedUpdate()
    {

        time += Time.deltaTime;
        t = time / 50;
        t = t * t * (3f - 2f * t);
        transform.position = Vector3.Lerp(transform.position, PointSuivant, t);
        if (Mathf.Abs(angle - transform.rotation.eulerAngles.y) <175|| Mathf.Abs(angle - transform.rotation.eulerAngles.y)>3)
        {
            transform.Rotate(new Vector3(0, (angle-transform.rotation.eulerAngles.y) * 0.01f, 0));
        }
        if (Vector3.Magnitude(PointSuivant - transform.position) < 0.01f)
        {
            angle = 0;
            PointPrécédent = chemins[index];
            PointSuivant = chemins[index + 1];
            Vector3 v = PointSuivant - PointPrécédent;
            angle = Vector3.Angle(v, Vector3.forward);
            time = 0;
            index++;
            PointSuivant = chemins[index];

            VérifierDirection();
        }
    }


    void VérifierDirection() // utiliser vecteurs 
    {
        Debug.Log(PointSuivant);
        if ((PointSuivant.z - transform.position.z) > 0.7f) // vérifier la direction
        {
            if (Initiation)
            {
                transform.position += new Vector3(0.1f, 0, 0);
                Initiation = false;
            }
            //if (Mathf.Abs(chemins[index + 1].x - PointSuivant.x) > 0.90f)
            //{

            //    //PointSuivant -= new Vector3(0, 0, 0.5f);

            //}

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
            //if (Mathf.Abs(chemins[index + 1].x - PointSuivant.x) > 0.90f)
            //{
            //    PointSuivant += new Vector3(0, 0, 0.5f);

            //}
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
            //if (Mathf.Abs(chemins[index + 1].z - PointSuivant.z) > 0.90f)
            //{
            //    PointSuivant -= new Vector3(0.5f, 0, 0);

            //}
            PointSuivant -= new Vector3(0, 0, 0.1f);
            Debug.Log(2);

        }
        if ((PointSuivant.x - transform.position.x) < -0.7f)
        {
            if (Initiation)
            {
                transform.position += new Vector3(0, 0, 0.1f);
                transform.Rotate(0, -90, 0);
                Initiation = false;
            }
            //if (Mathf.Abs(chemins[index + 1].z - PointSuivant.z) > 0.90f)
            //{
            //    PointSuivant += new Vector3(0.5f, 0, 0);

            //}
            PointSuivant += new Vector3(0, 0, 0.1f);
            Debug.Log(3);
        }
        if (Initiation)
        {
            angle = transform.rotation.eulerAngles.y;
        }
    }
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


