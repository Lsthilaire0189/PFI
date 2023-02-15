
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

	public class RoadHelper : MonoBehaviour
	{
		public GameObject roadStraight, roadCorner, road3way, road4way, roadEnd;
		Dictionary<Vector3Int, GameObject> roadDictionary = new Dictionary<Vector3Int, GameObject>();
		HashSet<Vector3Int> fixRoadCandidates = new HashSet<Vector3Int>();

		public List<Vector3Int> GetRoadPositions()
		{
			return roadDictionary.Keys.ToList();
		}

		public void PlaceStreetPositions(Vector3 startPosition, Vector3Int direction, int length)
		{
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

		public void FixRoad()
		{
			foreach (var position in fixRoadCandidates)//fixRaodCandidates is a hashset, insuring that we do not have duplicates
			{
				List<Direction> neighborsDirections = PlacementHelper.findNeighbor(position, roadDictionary.Keys);
				Quaternion rotation = Quaternion.identity;//Quaternion.identity represents the right of RoadStraight, basically the X axis
				if (neighborsDirections.Count == 1)
				{
					Destroy(roadDictionary[position]);
					if (neighborsDirections.Contains(Direction.Down))
					{
						rotation = Quaternion.Euler(0, 90, 0);//if the road is facing down, we need to rotate the new roadEnd by 90 degrees
						//to make it face the same way as the roadStraight
					}
					else if (neighborsDirections.Contains(Direction.Left))
					{
						rotation = Quaternion.Euler(0, 180, 0);
					}
					else if (neighborsDirections.Contains(Direction.Up))
					{
						rotation = Quaternion.Euler(0, -90, 0);
					}

					roadDictionary[position] = Instantiate(roadEnd, position, rotation, transform);
				}
				else if (neighborsDirections.Count == 2)
				{
					if (neighborsDirections.Contains(Direction.Up) && neighborsDirections.Contains(Direction.Down) ||
					    neighborsDirections.Contains(Direction.Right) && neighborsDirections.Contains(Direction.Left))//checks if the road is straight, meaning that
					//we do not need to implement a corner
					{
						continue;
					}
					
					Destroy(roadDictionary[position]);
					if (neighborsDirections.Contains(Direction.Up) &&
					    neighborsDirections.Contains(Direction.Right))
					{
						rotation = Quaternion.Euler(0, 90, 0);
					}
					else if (neighborsDirections.Contains(Direction.Down) &&
					         neighborsDirections.Contains(Direction.Right))
					{
						rotation = Quaternion.Euler(0, 180, 0);
					}
					else if (neighborsDirections.Contains(Direction.Down) &&
					         neighborsDirections.Contains(Direction.Left))
					{
						rotation = Quaternion.Euler(0, -90, 0);
					}
					
					roadDictionary[position] = Instantiate(roadCorner, position, rotation, transform);

				}
				else if (neighborsDirections.Count == 3)
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
				else
				{
					Destroy(roadDictionary[position]);//destroys the current straight road at the position
					roadDictionary[position] = Instantiate(road4way, position, rotation, transform);//instantiates the 4 way at the position of the previous road
				}
				
			}
		}
		
	}


