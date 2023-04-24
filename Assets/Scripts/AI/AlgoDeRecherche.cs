using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class Noeud
{
    public GameObject Route { get; set; }
    public Noeud Suivant { get; set; }
    public Noeud Précédent { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public bool visité { get; set; }

    public Noeud(GameObject route)
    {
        Route = route;
        Suivant = null; // Pas nécessaire, mais plus clair
        Précédent = null; // Pas nécessaire, mais plus clair
        x = (int)route.transform.position.x + 50;
        y = (int)route.transform.position.z + 50;
    }
}

public class AlgoDeRecherche : MonoBehaviour
{
    public GameObject[,] Carte;
    public List<Vector3> chemin;
    public List<Vector3> AlgoDijkstra(GameObject[,] carte, GameObject départ, GameObject destination)
    {
        Carte = carte;
        Queue<Noeud> frontière = new Queue<Noeud>();
        Noeud Départ = new Noeud(départ);
        Noeud Destination = new Noeud(destination);
        Noeud current;
        List<GameObject> NoeudVisités = new List<GameObject>();
        frontière.Enqueue(Départ);
        while (frontière.Count > 0)
        {
            current = frontière.Dequeue();
            NoeudVisités.Add(current.Route);
            if (current.Route.transform.position == Destination.Route.transform.position)
            {
                Destination.Précédent = current.Précédent;
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
        ConstructionChemin(Destination);
        return chemin;

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
        if (Carte[current.x - 1, current.y] != null)
        {
            voisin.Add(new Noeud(Carte[current.x - 1, current.y]));

        }
        if (Carte[current.x + 1, current.y] != null)
        {
            voisin.Add(new Noeud(Carte[current.x + 1, current.y]));

        }
        if (Carte[current.x, current.y - 1] != null)
        {
            voisin.Add(new Noeud(Carte[current.x, current.y - 1]));

        }
        if (Carte[current.x, current.y + 1] != null)
        {

            voisin.Add(new Noeud(Carte[current.x, current.y + 1]));

        }
        return voisin;
    }


}
