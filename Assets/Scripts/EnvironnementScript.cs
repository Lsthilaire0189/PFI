using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class EnvironnementScript : MonoBehaviour
{
    public List<GameObject> flaquesEau;//les différents types de flaques de d'eau
    public List<GameObject> boues;//les différents types de flaques de boue

    [Range(0, 100)] public int PourcentageRésidusEnvironnementaux; //la chance qu'un résidus environnementaux s'instancient sur une route

    [Range(0,100)] public int PourcentageFlaquesEau;//la chace que le résidus environnementaux instancié soit une flaque d'eau
    
    public RoadHelper roadHelper;

    public void InstancierEnvironnement()
    {
        List<Vector3Int> positionsRoutes = roadHelper.GetRoadPositions();//cette fonction retourne une liste contenant toutes les positions des routes présentent sur la carte

        List<int> positionsPossibles = Enumerable.Range(0, positionsRoutes.Count).ToList();

        var random = new Random();
        
        for (int i = 0; i < positionsRoutes.Count; i++)
        {
            if (random.Next(100) <= PourcentageRésidusEnvironnementaux)
            {
                var point = random.Next(0, positionsPossibles.Count);
                var pointPossible = positionsPossibles[point];
                Vector3 positionPrefab = new Vector3(positionsRoutes[pointPossible].x, -0.01007f, positionsRoutes[pointPossible].z);
                
                if (random.Next(100)<= PourcentageFlaquesEau)
                {
                    var typeDeFlaque = random.Next(0,flaquesEau.Count);
                    Instantiate(flaquesEau[typeDeFlaque], positionPrefab, Quaternion.Euler(-90, 0, random.Next(0,360)), transform);
                }
                else
                {
                    var typeDeBoue = random.Next(0,boues.Count);
                    Instantiate(boues[typeDeBoue], positionPrefab, Quaternion.Euler(-90, 0,random.Next(0,360)), transform);
                }
                positionsPossibles.Remove(positionsPossibles.ElementAt(point));
            }
        }
    }
}
