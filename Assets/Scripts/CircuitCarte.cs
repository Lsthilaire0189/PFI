using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CircuitCarte : MonoBehaviour 
{
    const int Éloignement = 6;
    const int ÉloignementMaison = 3;
    public RoadHelper RRoadHelper;

    public GameObject Arbre;
    public CircuitCarte(GameObject arbre, RoadHelper roadHelper)
    {
        Arbre = arbre;
        RRoadHelper = roadHelper;
        var listv = new List<List<Vector3>>();
        listv = TrouverPointsCircuit();
        FormerCircuit(listv);
        
    }
    private  List<List<Vector3>> TrouverPointsCircuit()
    {
        List<Vector3Int> listeRouteInitial = new List<Vector3Int>();
        listeRouteInitial = RRoadHelper.GetRoadPositions();
        var listeRoute = convertirEnVector3(listeRouteInitial);

        // Trouver les 4 points aux extremites de la carte
        float xMax = listeRoute.Max(v => v.x);
        float xMin = listeRoute.Min(v => v.x);
        float zMax = listeRoute.Max(v => v.z);
        float zMin = listeRoute.Min(v => v.z);

        float deltaX = xMax - xMin;
        float delatZ = zMax - zMin;
        // La spline cubique avec les mathématiques que j'ai utilisé peut seulement interpoler entre des points qui ont une différence de marche positive entre les deux, je dois alors faire deux splines

        var p1Spline1 = new Vector3(xMin - Éloignement, 0, zMin + delatZ/2);
        var p2Spline1 = new Vector3(xMin, 0, zMax+ ÉloignementMaison);
        var p3Spline1 = new Vector3(xMin + deltaX / 2, 0, zMax + Éloignement);
        var p4Spline1 = new Vector3(xMax, 0, zMax+ ÉloignementMaison);
        var p5Spline1 = new Vector3(xMax + Éloignement, 0, zMin + delatZ / 2);

        var p1Spline2 = new Vector3(p1Spline1.x, 0, p1Spline1.z);
        var p2Spline2 = new Vector3(xMin, 0, zMin- ÉloignementMaison);
        var p3Spline2 = new Vector3(xMin + deltaX / 2, 0,zMin-Éloignement);
        var p4Spline2 = new Vector3(xMax, 0, zMin - ÉloignementMaison);
        var p5Spline2 = new Vector3(p5Spline1.x, 0, p5Spline1.z);

        //
        var pointsInitiauxSpline = new List<List<Vector3>>() { new List<Vector3> { p1Spline1,p2Spline1, p3Spline1, p4Spline1,p5Spline1 }, new List<Vector3> { p1Spline2, p2Spline2, p3Spline2, p4Spline2,  p5Spline2 } };
        return pointsInitiauxSpline;
     


    }
    private List<Vector3> convertirEnVector3(List<Vector3Int> listeRoute)
    {
        List<Vector3> nvListeRoute = new List<Vector3>(listeRoute.Count);
        foreach (Vector3Int route in listeRoute)
        {
            nvListeRoute.Add((Vector3)route);
        }
        return nvListeRoute;
    }

    public void FormerCircuit(List<List<Vector3>> pointsInitiauxSpline)
    {
        var splinePartie1 = new Spline(pointsInitiauxSpline[0],Arbre);
        var splinePartie2 = new Spline(pointsInitiauxSpline[1],Arbre);
        splinePartie1.InstancierObjets();
        splinePartie2.InstancierObjets();
        
    }
   
}
