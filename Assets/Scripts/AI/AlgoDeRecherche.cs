using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class Noeud
{
    public GameObject Route { get; set; }
    public Noeud Suivant { get; set; }
    public Noeud Pr�c�dent { get; set; }
    public int x { get; set; }
    public int y { get; set; }
    public bool visit� { get; set; }

    public Noeud(GameObject route)
    {
        Route = route;
        Suivant = null; // Pas n�cessaire, mais plus clair
        Pr�c�dent = null; // Pas n�cessaire, mais plus clair
        x = (int)route.transform.position.x + 50;
        y = (int)route.transform.position.z + 50;
    }
}

public class AlgoDeRecherche : MonoBehaviour
{
    public GameObject[,] Carte;
    public List<Vector3> chemin;
    public List<Vector3> AlgoDijkstra(GameObject[,] carte, GameObject d�part, GameObject destination)
    {
        Carte = carte;
        Queue<Noeud> fronti�re = new Queue<Noeud>();
        Noeud D�part = new Noeud(d�part);
        Noeud Destination = new Noeud(destination);
        Noeud current;
        List<GameObject> NoeudVisit�s = new List<GameObject>();
        fronti�re.Enqueue(D�part);
        while (fronti�re.Count > 0)
        {
            current = fronti�re.Dequeue();
            NoeudVisit�s.Add(current.Route);
            if (current.Route.transform.position == Destination.Route.transform.position)
            {
                Destination.Pr�c�dent = current.Pr�c�dent;
                break;
            }
            var voisin = TrouverVoisins(current);
            for (int i = 0; i < voisin.Count; i++)
            {
                if (!NoeudVisit�s.Contains(voisin[i].Route))
                {
                    voisin[i].Pr�c�dent = current;
                    fronti�re.Enqueue(voisin[i]);
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
        while (current.Pr�c�dent != null)
        {
            temp.Push(current.Pr�c�dent);
            current = current.Pr�c�dent;
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
