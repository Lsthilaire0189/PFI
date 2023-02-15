using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlacementHelper
{
    public static List<Direction> findNeighbor(Vector3Int position, ICollection<Vector3Int> collection)
        //finds the neighbors of a position and goes thru the ICollection collection to find the references of those neighbors
    {
        //the idea is that we will return a list of directions in which there are neighbors (road neighbors) ex: 
        //if i'm a road and have a neighbor on my left, on my right and up, i will need the 3 way road, the same principal
        //applies with the 4 way roads
        List<Direction> neighborDirections = new List<Direction>();
        if (collection.Contains(position +
                                Vector3Int.right)) //this checks if there is a road to the right of the position
        {
            neighborDirections.Add(Direction.Right);

        }

        if (collection.Contains(position -
                                Vector3Int.right)) //this checks if there is a road to the left of the position
        {
            neighborDirections.Add(Direction.Left);
        }

        if (collection.Contains(position + new Vector3Int(0, 0, 1))) //this checks if there is a road above the position
        {
            neighborDirections.Add(Direction.Up);
        }

        if (collection.Contains(position - new Vector3Int(0, 0, 1))) //this checks if there is a road below the position
        {
            neighborDirections.Add(Direction.Down);
        }

        return neighborDirections;
    }
    
}
