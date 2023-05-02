using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;


public class StructureHelper : MonoBehaviour
{
    public GameObject maison;//the prefab we wanna spawn
    public GameObject apartementRouge;
    public GameObject apartementNoir;
    public GameObject bigBuilding;
    public GameObject gasStation; 
    public GameObject Garage;
    [Range(0, 6)] 
    public int nombreStationEssence;
    [Range(0, 6)] 
    public int nombreGarage;
    
    [Range(0.2f, 1)]
    public float chanceToSpawnBigBuilding = 0.2f;
    private List<Vector3Int> smallBuildingsDespawn = new List<Vector3Int>();
    public Dictionary<Vector3Int, GameObject> StructureDictionary = new Dictionary<Vector3Int, GameObject>(); //a dictionary containing the positions of all prefabs and their precise position

    public void PlaceStructureAroundRoad(List<Vector3Int> roadPositions)
    {

        int nombreRepetion = 0;

        Dictionary<Vector3Int, Direction> freeEstateSpots = FindFreeSpacesAroundRoad(roadPositions);
        Dictionary<Vector3, Direction> freeBigBuildingsSpots = BigBuildingPositionGetter(freeEstateSpots);
        Dictionary<Vector3, Direction> freeGasStationEtGarageSpots = GasStationEtGaragePositionGetter(freeBigBuildingsSpots, nombreStationEssence, nombreGarage);


        foreach (var smallBuilding in smallBuildingsDespawn)
        {
            freeEstateSpots.Remove(smallBuilding);
        }
        
        foreach (var freeSpot in freeEstateSpots)//for each position in freeEstateSpots, we will Instantiate a prefab
        {
            var rotation = Quaternion.identity;
            
            if(freeSpot.Value==Direction.Up)
                rotation=Quaternion.Euler(0,90,0);
            else if(freeSpot.Value==Direction.Down)
                rotation=Quaternion.Euler(0,-90,0);
            else if(freeSpot.Value==Direction.Right)
                rotation=Quaternion.Euler(0,180,0);

            if (UnityEngine.Random.value < 0.3f)
            {
                Instantiate(maison, freeSpot.Key, rotation, transform);
            }
            else if (UnityEngine.Random.value is >= 0.3f and <= 0.7f)
            {
                Instantiate(apartementRouge, freeSpot.Key, rotation, transform);
            }
            else
            {
                Instantiate(apartementNoir, freeSpot.Key, rotation, transform);
            }
        }

        foreach (var bigBuildingFreeSpot in freeBigBuildingsSpots)
        {
            var rotation = Quaternion.identity;
            
            if(bigBuildingFreeSpot.Value==Direction.Up)
                rotation=Quaternion.Euler(0,90,0);
            else if(bigBuildingFreeSpot.Value==Direction.Down)
                rotation=Quaternion.Euler(0,-90,0);
            else if(bigBuildingFreeSpot.Value==Direction.Right)
                rotation=Quaternion.Euler(0,180,0);
            
            Instantiate(bigBuilding, bigBuildingFreeSpot.Key, rotation, transform);
        }

        foreach (var gasStationEtGarageSpot in freeGasStationEtGarageSpots)
        {
            var rotation = Quaternion.identity;
            
            
            if (nombreRepetion < nombreStationEssence)
            {
                if(gasStationEtGarageSpot.Value==Direction.Left)
                    rotation=Quaternion.Euler(0,-90,0);
                else if(gasStationEtGarageSpot.Value==Direction.Down)
                    rotation=Quaternion.Euler(0,180,0);
                else if(gasStationEtGarageSpot.Value==Direction.Right)
                    rotation=Quaternion.Euler(0,90,0);
                else if(gasStationEtGarageSpot.Value==Direction.Up)
                    rotation=Quaternion.Euler(0,0,0);
                Instantiate(gasStation, gasStationEtGarageSpot.Key, rotation, transform);
            }
            else
            {if(gasStationEtGarageSpot.Value==Direction.Left)
                    rotation=Quaternion.Euler(0,180,0);
                else if(gasStationEtGarageSpot.Value==Direction.Down)
                    rotation=Quaternion.Euler(0,90,0);
                else if(gasStationEtGarageSpot.Value==Direction.Right)
                    rotation=Quaternion.Euler(0,0,0);
                else if(gasStationEtGarageSpot.Value==Direction.Up)
                    rotation=Quaternion.Euler(0,-90,0);
                Instantiate(Garage, gasStationEtGarageSpot.Key, rotation, transform);
            }

            nombreRepetion++;
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
        foreach (var position in freeSpaces)
        {
            
            if(UnityEngine.Random.value < chanceToSpawnBigBuilding)
            {
                var currentPositionRight = (position.Key +position.Key+Vector3.right)/2;
                var currentPositionUp = (position.Key + position.Key + new Vector3(0, 0, 1)) / 2;
                
                if (freeSpaces.ContainsKey(position.Key+Vector3Int.right) && freeSpaces.ContainsKey(position.Key-Vector3Int.right) &&!bigBuildingPositions.ContainsKey
                        (currentPositionRight+Vector3.right) && 
                    !bigBuildingPositions.ContainsKey(currentPositionRight-Vector3.right))
                {
                    bigBuildingPositions.Add(currentPositionRight, position.Value);
                    smallBuildingsDespawn.Add(position.Key);
                    smallBuildingsDespawn.Add(position.Key+Vector3Int.right);
                } 
                else if (freeSpaces.ContainsKey(position.Key+ new Vector3Int(0,0,1)) &&freeSpaces.ContainsKey(position.Key- new Vector3Int(0,0,1))
                         &&!bigBuildingPositions.ContainsKey(currentPositionUp+new Vector3(0,0,1)) && 
                         !bigBuildingPositions.ContainsKey(currentPositionUp+new Vector3(0,0,-1)))
                {
                    bigBuildingPositions.Add(currentPositionUp, position.Value);
                    smallBuildingsDespawn.Add(position.Key);
                    smallBuildingsDespawn.Add(position.Key+new Vector3Int(0,0,1));
                }
            }
        }
        return bigBuildingPositions;
    }


    private Dictionary<Vector3, Direction> GasStationEtGaragePositionGetter(
        Dictionary<Vector3, Direction> freeBigBuildingSpots, int nombreStationEssence, int nombreGarage)
    {
        var random = new Random();

        Dictionary<Vector3, Direction> GasStationEtGaragePositions = new Dictionary<Vector3, Direction>();

        int nmbreBigBuilding = freeBigBuildingSpots.Count;

        List<int> listeNombrePossible = new List<int>();

        for (int i = 0; i < nmbreBigBuilding; i++)
        {
            listeNombrePossible.Add(i);
        }

        for (int w = 0; w < nombreStationEssence; w++)
        {
            var nmbreHasard = random.Next(listeNombrePossible.Count);

            GasStationEtGaragePositions.Add(freeBigBuildingSpots.ElementAt(nmbreHasard).Key,
                freeBigBuildingSpots.ElementAt(nmbreHasard).Value);

            freeBigBuildingSpots.Remove(freeBigBuildingSpots.ElementAt(nmbreHasard).Key);

            listeNombrePossible.Remove(nmbreHasard);

        }
        
        for (int r = 0; r < nombreGarage; r++)
        {
            var nmbreHasard = random.Next(listeNombrePossible.Count);

            GasStationEtGaragePositions.Add(freeBigBuildingSpots.ElementAt(nmbreHasard).Key, 
                freeBigBuildingSpots.ElementAt(nmbreHasard).Value);

            freeBigBuildingSpots.Remove(freeBigBuildingSpots.ElementAt(nmbreHasard).Key);

            listeNombrePossible.Remove(nmbreHasard);

        }

        return GasStationEtGaragePositions;
    }

}

