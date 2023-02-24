using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureHelper : MonoBehaviour
{
    public GameObject prefab;//the prefab we wanna spawn
    public Dictionary<Vector3Int, GameObject> StructureDictionary = new Dictionary<Vector3Int, GameObject>(); //a dictionary containing the positions of all prefabs and their precise position

    public void PlaceStructureAroundRoad(List<Vector3Int> roadPositions)
    {
        Dictionary<Vector3Int, Direction> freeEstateSpots = FindFreeSpacesAroundRoad(roadPositions);
        foreach (var position in freeEstateSpots.Keys)//for each position in freeEstateSpots, we will Instantiate a prefab
        {
            Instantiate(prefab, position, Quaternion.identity, transform);
        }
    }

    private Dictionary<Vector3Int, Direction> FindFreeSpacesAroundRoad(List<Vector3Int> roadPositions)//method to allow use to find those free spaces
    {
        Dictionary<Vector3Int, Direction> freeSpaces = new Dictionary<Vector3Int, Direction>();
        foreach (var position in roadPositions)//we will go thru each road
        {
            var neighborDirections = PlacementHelper.findNeighbor(position, roadPositions);//uses the intrant position and goes tru roadPositions to find it's neighbors. Allows
            //to get all the directions of the neighbor of the current position
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))//loop thru our Enums (we should get each type of direction from our Enum)
            {
                if (neighborDirections.Contains(direction) == false)//then we know that there is no neighbor in this direction at the current position
                {//We then go thru Enum Direction.cs to check if any of those directions appear in the list
                    var newPosition = position + PlacementHelper.GetOffsetFromDirection(direction);//basically for each road position, we create its own list of neighborDirections.
                    //We then go thru Enum Direction.cs to check if any of those Directions appear in the neighborDirections list and if they don't, that means that the current position + 
                    //the direction where there is no neighbor is a freeSpace.
                    if (freeSpaces.ContainsKey(newPosition))
                    {
                        continue;//to avoid duplication
                    }
                    freeSpaces.Add(newPosition, PlacementHelper.FindOrientation(newPosition));
                }
            }
            
        }

        return freeSpaces;
    }
}
