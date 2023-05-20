using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class StructureHelper : MonoBehaviour
{
    public GameObject maison;//the prefab we wanna spawn
    public GameObject apartementRouge;
    public GameObject apartementNoir;
    public GameObject bigBuilding;
    public GameObject gasStation; 
    public GameObject garage;

    [Range(0.2f, 1)]
    public float chanceToSpawnBigBuilding = 0.467f;

    private List<Vector3Int> smallBuildingsDespawn = new List<Vector3Int>();

    public void PlaceStructureAroundRoad(List<Vector3Int> roadPositions)//this function allows us to place structures around the road of the map
    {
        int nombreRepetion = 0;

        Dictionary<Vector3Int, Direction> freeEstateSpots = FindFreeSpacesAroundRoad(roadPositions);//dictionary containing the position and the rotation of the spots where a small building can be instantiated.
        //To get these spots, we call FindFreeSpacesAroundRoad() which returns a dictionary containing the positions and the direction of all the free estate spots located around the road.  
        Dictionary<Vector3, Direction> freeBigBuildingsSpots = BigBuildingPositionGetter(freeEstateSpots);
        Dictionary<Vector3, Direction> freeGasStationEtGarageSpots = GasStationEtGaragePositionGetter(freeBigBuildingsSpots);
        
        foreach (var smallBuilding in smallBuildingsDespawn)
        {
            freeEstateSpots.Remove(smallBuilding);//we remove all the small buildings we want to despawn from the city
        }
        
        foreach (var freeSpot in freeEstateSpots)//for each position in freeEstateSpots, we will Instantiate a prefab
        {
            var rotation = Quaternion.identity;
            //ensures that the building is well rotated
            if(freeSpot.Value==Direction.Up)
                rotation=Quaternion.Euler(0,90,0);
            else if(freeSpot.Value==Direction.Down)
                rotation=Quaternion.Euler(0,-90,0);
            else if(freeSpot.Value==Direction.Right)
                rotation=Quaternion.Euler(0,180,0);
            
            //this allows diversification of the type of small building that will be spawned
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
            //ensures that the big building is well rotated
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
            
            if (nombreRepetion == 0)
            {//ensures that the gasStation is well rotated
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
            {//ensures that the garage is well rotated
                if(gasStationEtGarageSpot.Value==Direction.Left)
                    rotation=Quaternion.Euler(0,180,0);
                else if(gasStationEtGarageSpot.Value==Direction.Down)
                    rotation=Quaternion.Euler(0,90,0);
                else if(gasStationEtGarageSpot.Value==Direction.Right)
                    rotation=Quaternion.Euler(0,0,0);
                else if(gasStationEtGarageSpot.Value==Direction.Up)
                    rotation=Quaternion.Euler(0,-90,0);
                Instantiate(garage, gasStationEtGarageSpot.Key+ new Vector3(0, 0.072f, 0), rotation, transform);
            }

            nombreRepetion++;
        }
    }
    
    private Dictionary<Vector3Int, Direction> FindFreeSpacesAroundRoad(List<Vector3Int> roadPositions)//I did not code this. I took it from
        //the internet source. This method to allow use to find those free spaces. https://www.youtube.com/watch?v=c9OTDfKJrAM&list=PLcRSafycjWFcbaI8Dzab9sTy5cAQzLHoy&index=11 
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
    
    public Direction FindOrientation(Vector3Int newPosition, List<Vector3Int> roadPositions) //this function allows us to get the orientation we need to give to the building we want  
    {//to instantiate so that it faces the road
        Direction rotationBatiment = Direction.Up;
        foreach (var position in roadPositions)
        {
            if (position + Vector3Int.right == newPosition)//if the road is on the right of the building, the building will be rotated to the left so that it faces it and etc...
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

    public Dictionary<Vector3, Direction> BigBuildingPositionGetter(Dictionary<Vector3Int, Direction> freeSpaces)//allows us to get the positions of where we can potentially spawn a big building
    {//, occupying the space of 2 smaller buildings
        Dictionary<Vector3, Direction> bigBuildingPositions = new Dictionary<Vector3, Direction>();
        foreach (var position in freeSpaces)
        {
            if(UnityEngine.Random.value < chanceToSpawnBigBuilding)
            {
                var currentPositionRight = (position.Key +position.Key+Vector3.right)/2;//allows us to obtain the position between the two next buildings to the right of our current position
                var currentPositionUp = (position.Key + position.Key + new Vector3(0, 0, 1)) / 2;//allows us to obtain the position between the two next buildings above our current position
                
                if (freeSpaces.ContainsKey(position.Key+Vector3Int.right) && freeSpaces.ContainsKey(position.Key-Vector3Int.right) &&!bigBuildingPositions.ContainsKey
                        (currentPositionRight+Vector3.right) && 
                    !bigBuildingPositions.ContainsKey(currentPositionRight-Vector3.right))//verifies if a big building can be instantiated to the right of our current position and if it is not a duplicate
                {
                    bigBuildingPositions.Add(currentPositionRight, position.Value);
                    smallBuildingsDespawn.Add(position.Key);//we add the two next buildings to the right of our current position to the small buildings we want to despawn so that the big building can fit
                    smallBuildingsDespawn.Add(position.Key+Vector3Int.right);
                } 
                else if (freeSpaces.ContainsKey(position.Key+ new Vector3Int(0,0,1)) &&freeSpaces.ContainsKey(position.Key- new Vector3Int(0,0,1))
                         &&!bigBuildingPositions.ContainsKey(currentPositionUp+new Vector3(0,0,1)) && 
                         !bigBuildingPositions.ContainsKey(currentPositionUp+new Vector3(0,0,-1)))//verifies if a big building can be instantiated above our current position and if it is not a duplicate
                {
                    bigBuildingPositions.Add(currentPositionUp, position.Value);
                    smallBuildingsDespawn.Add(position.Key);//we add the two next buildings above of our current position to the small buildings we want to despawn so that the big building can fit
                    smallBuildingsDespawn.Add(position.Key+new Vector3Int(0,0,1));
                }
            }
        }
        return bigBuildingPositions;
    }
    
    private Dictionary<Vector3, Direction> GasStationEtGaragePositionGetter(
        Dictionary<Vector3, Direction> freeBigBuildingSpots)//this function allows us to instantiate a garage and a gasStation at the place of a big building.
    {
        var random = new Random();

        Dictionary<Vector3, Direction> GasStationEtGaragePositions = new Dictionary<Vector3, Direction>();

        int nmbreBigBuilding = freeBigBuildingSpots.Count;

        List<int> listeNombrePossible = new List<int>();

        for (int i = 0; i < nmbreBigBuilding; i++)
        {
            listeNombrePossible.Add(i);
        }
        
        var nmbreHasard = random.Next(listeNombrePossible.Count);//we randomly chose the big building we will replace by a gasStation
        GasStationEtGaragePositions.Add(freeBigBuildingSpots.ElementAt(nmbreHasard).Key,
            freeBigBuildingSpots.ElementAt(nmbreHasard).Value);//we add its position and rotation to the dictionary which contains these information for the gasStation and for the garage.
        freeBigBuildingSpots.Remove(freeBigBuildingSpots.ElementAt(nmbreHasard).Key);//we remove the randomly chosen big building from the dictionary containing the information of the big buildings we
        //will want to instantiate
        listeNombrePossible.Remove(nmbreHasard);//avoids duplication
        
        nmbreHasard = random.Next(listeNombrePossible.Count);//we randomly chose the big building we will replace by a garage
        GasStationEtGaragePositions.Add(freeBigBuildingSpots.ElementAt(nmbreHasard).Key, 
                freeBigBuildingSpots.ElementAt(nmbreHasard).Value);//we add its position and rotation to the dictionary which contains these information for the gasStation and for the garage.
            freeBigBuildingSpots.Remove(freeBigBuildingSpots.ElementAt(nmbreHasard).Key);//we remove the randomly chosen big building from the dictionary containing the information of the big buildings we
            //will want to instantiate
            return GasStationEtGaragePositions;
    }
}

