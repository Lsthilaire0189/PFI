using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class EnvironnementScript : MonoBehaviour
{
    public List<GameObject> flaquesEau;
    public List<GameObject> boues;

    [Range(0, 100)] public int PourcentageRésidusEnvironnementaux;

    [Range(0,100)] public int PourcentageFlaques;


    public RoadHelper roadHelper;


    public void InstancierEnvironnement()
    {

        List<Vector3Int> positionsRoutes = roadHelper.GetRoadPositions();

        List<int> positionsPossibles = Enumerable.Range(0, positionsRoutes.Count).ToList();

        var random = new Random();
        
        
        for (int i = 0; i < positionsRoutes.Count; i++)
        {
            if (random.Next(100) <= PourcentageRésidusEnvironnementaux)
            {
                

                var point = random.Next(0, positionsPossibles.Count);
                var pointPossible = positionsPossibles[point];
                Vector3 positionPrefab = new Vector3(positionsRoutes[pointPossible].x, 0, positionsRoutes[pointPossible].z);//-0.01007f
                
                
                if (random.Next(100)<= PourcentageFlaques)
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
