using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
class Noeud
{
    public GameObject Route { get; set; }
    public Noeud Suivant { get; set; }
    public Noeud Précédent { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public bool visité { get; set; }
    public int coût { get; set; }

    public Noeud(GameObject route)
    {
        Route = route;
        Suivant = null; // Pas nécessaire, mais plus clair
        Précédent = null; // Pas nécessaire, mais plus clair
        x = (int)route.transform.position.x + 50;
        y = (int)route.transform.position.z + 50;
    }
}

public class CréerCarte : MonoBehaviour
{
    [SerializeField] GameObject roadHelper;
    public GameObject[,] carte;
    List<GameObject> list;
    GameObject Départ;
    public GameObject Destination;
    public List<Vector3> chemin;
    [SerializeField] GameObject voiture;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(InitierAuto());
    }
    // Update is called once per frame
    IEnumerator InitierAuto ()
    {
        yield return new WaitForSeconds(.5f);
        carte = new GameObject[100, 100];
        int Nbrues = roadHelper.transform.childCount;
        list = new List<GameObject>();
        for (int i = 0; i < Nbrues; i++)
        {
            list.Add(roadHelper.transform.GetChild(i).gameObject);
        }
        foreach (GameObject item in list)
        {
            int x = (int)item.transform.position.x + 50;
            int y = (int)item.transform.position.z + 50;
            carte[x, y] = item;
        }
        TrouverDestination();
        AlgoDijkstra();
        GameObject auto = Instantiate(voiture,Départ.transform.position+Vector3.up, Départ.transform.rotation,gameObject.transform);
    }
    void TrouverDestination()
    {
        int dp = UnityEngine.Random.Range(0, list.Count);
        int ds = UnityEngine.Random.Range(0,list.Count);
        Départ = list[dp];
        Destination = list[ds];

    }
    void AlgoDijkstra()
    {
        Queue<Noeud> frontière = new Queue<Noeud>();
        Noeud départ = new Noeud(Départ);
        départ.coût = 0;
        Noeud destination = new Noeud(Destination);
        Noeud current;
        List<GameObject> NoeudVisités = new List<GameObject>();
        frontière.Enqueue(départ);
        while (frontière.Count > 0)
        {
            current = frontière.Dequeue();
            NoeudVisités.Add(current.Route);
            if (current.Route.transform.position == destination.Route.transform.position)
            {
                destination.Précédent = current.Précédent;
                break;
            }
            var voisin = TrouverVoisins(current);
            for (int i = 0; i < voisin.Count; i++)
            {
                if (!NoeudVisités.Contains(voisin[i].Route))
                {
                    voisin[i].Précédent = current;
                    frontière.Enqueue(voisin[i]);
                }
            }

        }
        ConstructionChemin(destination);

    }
    void ConstructionChemin(Noeud Destination)
    {
        Stack<Noeud> temp = new Stack<Noeud>();
        Noeud current = Destination;
        temp.Push(current);
        chemin = new List<Vector3>();
        while (current.Précédent != null)
        {
            temp.Push(current.Précédent);
            current = current.Précédent;
        }
        while (temp.Count > 0)
        {
            chemin.Add(temp.Pop().Route.transform.position);
        }
    }
    List<Noeud> TrouverVoisins(Noeud current)
    {
        List<Noeud> voisin = new List<Noeud>();
        if (carte[current.x - 1, current.y] != null)
        {
            voisin.Add(new Noeud(carte[current.x - 1, current.y]));

        }
        if (carte[current.x + 1, current.y] != null)
        {
            voisin.Add(new Noeud(carte[current.x + 1, current.y]));

        }
        if (carte[current.x, current.y - 1] != null)
        {
            voisin.Add(new Noeud(carte[current.x, current.y - 1]));

        }
        if (carte[current.x, current.y + 1] != null)
        {

            voisin.Add(new Noeud(carte[current.x, current.y + 1]));

        }
        return voisin;
    }



}
