using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ArgentSpawner : MonoBehaviour
{
    public RoadHelper rH;

    public int chanceInstantierArgent;

    private List<Vector3Int> positionsRoutes;
    

    public GameObject monnaiePrefab;
    
    

    public void InstatierMonnaies()
    {
        positionsRoutes = rH.GetRoadPositions();

        var rotation = Quaternion.identity;

        var random = new Random();

        foreach (var positionRoute in positionsRoutes)
        {
            if (random.Next(100) <= chanceInstantierArgent)
            {
                Instantiate(monnaiePrefab, positionRoute, rotation, transform);
            }
        }
    }
}
