using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// a faire la rotation utiliser les vecteurs utiliser fonction spéciale juste aux intersectiosn
public class AIDéplacement : MonoBehaviour
{
    GameObject[,] Carte;
    List<GameObject> ListePoints;
    AlgoDeRecherche algoDeRecherche;
    public List<Vector3> chemins;
    GameObject Départ, Destination;
    Vector3 destination;
    Vector3 PointSuivant;
    bool Initiation = true;
    int index =1;
    float time = 0;
    [SerializeField] GameObject o;
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
        transform.position = Départ.transform.position+Vector3.up;
        destination = Destination.transform.position;
        chemins = algoDeRecherche.AlgoDijkstra(Carte, Départ, Destination);
        PointSuivant = chemins[index];
        VérifierDirection();


    }
    private void FixedUpdate()
    {

        time += Time.deltaTime;
        t = time / 300;
        t = t * t * (3f - 2f * t);
        transform.position = Vector3.Lerp(transform.position, PointSuivant, t);
        if (Vector3.Magnitude(PointSuivant - transform.position) < 0.01f)
        {
            index++;
            PointSuivant = chemins[index];
            VérifierDirection();
        }
    }


    void VérifierDirection() // utiliser vecteurs 
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
                time = 0;
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
                time = 0;
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
                time = 0;
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
                time = 0;
            }
            PointSuivant += new Vector3(0, 0, 0.1f);
            Debug.Log(3);
        }
    }
}
