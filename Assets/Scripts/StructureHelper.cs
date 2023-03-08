using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StructureHelper : MonoBehaviour
{
    public GameObject prefab;//the prefab we wanna spawn
    public GameObject BigBuilding;
    [Range(0, 1)]
    public float chanceToSpawnBigBuilding = 0.1f;
    public Dictionary<Vector3Int, GameObject> StructureDictionary = new Dictionary<Vector3Int, GameObject>(); //a dictionary containing the positions of all prefabs and their precise position

    public void PlaceStructureAroundRoad(List<Vector3Int> roadPositions)
    {
        Dictionary<Vector3Int, Direction> freeEstateSpots = FindFreeSpacesAroundRoad(roadPositions);
        Dictionary<Vector3, Direction> freeBigBuildingsSpot = BigBuildingPositionGetter(freeEstateSpots);

        foreach (var freeSpot in freeEstateSpots)//for each position in freeEstateSpots, we will Instantiate a prefab
        {
            var rotation = Quaternion.identity;
            
            if(freeSpot.Value==Direction.Up)
                rotation=Quaternion.Euler(0,90,0);
            else if(freeSpot.Value==Direction.Down)
                rotation=Quaternion.Euler(0,-90,0);
            else if(freeSpot.Value==Direction.Right)
                rotation=Quaternion.Euler(0,180,0);
            
            Instantiate(prefab, freeSpot.Key, rotation, transform);
        }

        Debug.Log(freeBigBuildingsSpot.Keys.Count);
        foreach (var bigBuildingFreeSpot in freeBigBuildingsSpot)
        {
            var rotation = Quaternion.identity;
            
            if(bigBuildingFreeSpot.Value==Direction.Up)
                rotation=Quaternion.Euler(0,90,0);
            else if(bigBuildingFreeSpot.Value==Direction.Down)
                rotation=Quaternion.Euler(0,-90,0);
            else if(bigBuildingFreeSpot.Value==Direction.Right)
                rotation=Quaternion.Euler(0,180,0);
            
            Instantiate(BigBuilding, bigBuildingFreeSpot.Key, rotation, transform);
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
                    freeSpaces.Add(newPosition, FindOrientation(newPosition, roadPositions));
                }
            }
            
        }

        return freeSpaces;
    }
    
    public Direction FindOrientation(Vector3Int newPosition, List<Vector3Int> roadPositions)
    {
        Direction rotationBatiment = Direction.Up;
        foreach (var position in roadPositions)
        {
            if (position + Vector3Int.right == newPosition)
            {
                rotationBatiment= Direction.Left;
            }
            else if (position + Vector3Int.left == newPosition)
            {
                rotationBatiment= Direction.Right;
            }
            else if (position + new Vector3Int(0, 0, 1) == newPosition)
            {
                rotationBatiment= Direction.Down;
            }
            else if(position - new Vector3Int(0, 0, 1) == newPosition)
            {
                rotationBatiment= Direction.Up;
            }
            
        }
        return rotationBatiment;
    }

    public Dictionary<Vector3, Direction> BigBuildingPositionGetter(Dictionary<Vector3Int, Direction> freeSpaces)
    {
        Dictionary<Vector3, Direction> bigBuildingPositions = new Dictionary<Vector3, Direction>();
        Vector3 currentPositionRight = new Vector3();
        Vector3 currentPositionUp = new Vector3();
        foreach (var position in freeSpaces)
        {
            
            if(UnityEngine.Random.value < chanceToSpawnBigBuilding)
            {
                currentPositionRight = (position.Key +position.Key+Vector3.right)/2;
                currentPositionUp = (position.Key + position.Key + new Vector3(0, 0, 1)) / 2;
                Debug.Log(currentPositionRight);
                if (freeSpaces.ContainsKey(position.Key+Vector3Int.right))
                {
                    bigBuildingPositions.Add(currentPositionRight, position.Value);
                } 
                else if (freeSpaces.ContainsKey(position.Key+ new Vector3Int(0,0,1)))
                {
                    bigBuildingPositions.Add(currentPositionUp, position.Value);
                }
                
            }
            

        }

        return bigBuildingPositions;
    }

}

