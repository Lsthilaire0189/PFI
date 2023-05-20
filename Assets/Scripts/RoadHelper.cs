using System.Collections.Generic;
using System.Linq;
using UnityEngine;

	public class RoadHelper : MonoBehaviour
	{
		public GameObject roadStraight, roadCorner, road3way, road4way, roadEnd;
		public Dictionary<Vector3Int, GameObject> roadDictionary;
		private HashSet<Vector3Int> fixRoadCandidates;
		
		public List<Vector3Int> GetRoadPositions()//takes the Keys of roadDictionary (which are positions) and returns them as a list
		{
			return roadDictionary.Keys.ToList();
		}
		
		public void PlaceStreetPositions(Vector3 startPosition, Vector3Int direction, int length)//I did not code this function. It was taken from the internet source
		{//https://www.youtube.com/watch?v=-sr_RFdMaz4&list=PLcRSafycjWFcbaI8Dzab9sTy5cAQzLHoy&index=9
			var rotation = Quaternion.identity;
			if(direction.x == 0)
			{
				rotation = Quaternion.Euler(0, 90, 0);
			}
			for (int i = 0; i < length; i++)
			{
				var position = Vector3Int.RoundToInt(startPosition + direction *i);
				if (roadDictionary.ContainsKey(position))
				{
					continue;
				}
				var road = Instantiate(roadStraight, position, rotation, transform);
				roadDictionary.Add(position, road);
				if(i==0 || i == length - 1)
				{
					fixRoadCandidates.Add(position);
				}
			}
		}

		public void FixRoad() //I created this function by using the theory of the source from the internet
		{
			foreach (var position in fixRoadCandidates)//fixRoadCandidates is a hashset, insuring that we do not have duplicates
			{
				List<Direction> neighborsDirections = PlacementHelper.findNeighbor(position, roadDictionary.Keys);//this function returns a list containing the positions of the roads neighbouring the position
				//passed in the parameter
				Quaternion rotation = Quaternion.identity;
				if (neighborsDirections.Count == 1) //verifies if the road has only one other road prefab near it, meaning that it is the end of the road
				{
					Destroy(roadDictionary[position]);
					if (neighborsDirections.Contains(Direction.Down))
					{
						rotation = Quaternion.Euler(0, 90, 0);//if the neighbouring road's position is beneath, we need to rotate the new roadEnd by 90 degrees
						//to make it face the same way as the neighbouring road
					}
					else if (neighborsDirections.Contains(Direction.Left))
					{
						rotation = Quaternion.Euler(0, 180, 0);//if the neighbouring road's position is on the left, we need to rotate the new roadEnd by 180 degrees
						//to make it face the same way as the neighbouring road
					}
					else if (neighborsDirections.Contains(Direction.Up))//if the neighbouring road's position is above, we need to rotate the new roadEnd by -90 degrees
						//to make it face the same way as the neighbouring road
					{
						rotation = Quaternion.Euler(0, -90, 0);
					}

					roadDictionary[position] = Instantiate(roadEnd, position, rotation, transform);//since it is the end of the road, we place a roadEnd.
				}
				else if (neighborsDirections.Count == 2)//verifies if the current roadStraight has 2 neighbouring roads.
				{
					Destroy(roadDictionary[position]);
					
					if (neighborsDirections.Contains(Direction.Right) && neighborsDirections.Contains(Direction.Left))//checks if the roadStraight is already well rotated, meaning that
						//we do not need to change anything
					{
						roadDictionary[position] = Instantiate(roadStraight, position, Quaternion.Euler(0, 0, 0), transform);
						continue;
					}
					if (neighborsDirections.Contains(Direction.Up) && neighborsDirections.Contains(Direction.Down))//if there is a road above and below the current roadStraight, it means
					//that we need to create a new roadStraight facing down to connect the road above and below it together.
					{
						roadDictionary[position] = Instantiate(roadStraight, position, Quaternion.Euler(0, 90, 0), transform);
						continue;
					}
					//we will now be verifying if we need to place a roadCorner instead of a roadStraight
					if (neighborsDirections.Contains(Direction.Up) &&
					    neighborsDirections.Contains(Direction.Right))
					{
						rotation = Quaternion.Euler(0, 90, 0);//if the neighbouring roads' position is above and on the right, we will need to place
						//place a roadCorner with a 90 degree rotation
					}
					else if (neighborsDirections.Contains(Direction.Down) &&
					         neighborsDirections.Contains(Direction.Right))
					{
						rotation = Quaternion.Euler(0, 180, 0);//if the neighbouring roads' position is below and on the right, we will need to place
						//place a roadCorner with a 180 degree rotation
					}
					else if (neighborsDirections.Contains(Direction.Down) &&
					         neighborsDirections.Contains(Direction.Left))
					{
						rotation = Quaternion.Euler(0, -90, 0);//if the neighbouring roads' position is below and on the left, we will need to place
						//place a roadCorner with a -90 degree rotation
					}
					roadDictionary[position] = Instantiate(roadCorner, position, rotation, transform);
				}
				else if (neighborsDirections.Count == 3) //verifies if the currentRaodStraight has 3 neighbouring roads. If it's the case, we need to place a road3way instead.
				{
					Destroy(roadDictionary[position]);
					if (neighborsDirections.Contains(Direction.Up) && neighborsDirections.Contains(Direction.Left) && neighborsDirections.Contains(Direction.Down))
					{
						rotation = Quaternion.Euler(0, -180, 0);
					}
					if (neighborsDirections.Contains(Direction.Left) && neighborsDirections.Contains(Direction.Down) && neighborsDirections.Contains(Direction.Right))
					{
						rotation = Quaternion.Euler(0, 90, 0);
					}
					if (neighborsDirections.Contains(Direction.Up) && neighborsDirections.Contains(Direction.Left) && neighborsDirections.Contains(Direction.Right))
					{
						rotation = Quaternion.Euler(0, -90, 0);
					}
					roadDictionary[position] = Instantiate(road3way, position, rotation, transform);
					
				}
				else //if the current roadStraight has 4 neighbours, it immediately means that we need to place a 4 way intersection instead
				{
					Destroy(roadDictionary[position]);//destroys the current straight road at the position
					roadDictionary[position] = Instantiate(road4way, position, rotation, transform);//instantiates the 4 way at the position of the previous road
				}
				
			}
		}
	}


