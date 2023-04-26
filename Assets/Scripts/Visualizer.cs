
using System.Collections.Generic;
using UnityEngine;


public class Visualizer : MonoBehaviour
{
    Circuit circuit;
    public GameObject Tree;
    public LSystemGenerator lsystem;
    List<Vector3> positions = new List<Vector3>();

    public RoadHelper roadHelper;

    public StructureHelper structureHelper;

    public EnvironnementScript instancierEnvironnement;

    public ArgentSpawner instancierMonnaies;

    private int length = 6;
    private float angle = 90;

    public int Length
    {
        get
        {
            if (length > 0)
            {
                return length;
            }
            else
            {
                return 1;
            }
        }
        set => length = value;
    }

    private void Start()
    {
        var sequence = lsystem.GenerateSentence();
        VisualizeSequence(sequence);
        circuit = gameObject.AddComponent<Circuit>();
    }

    private void VisualizeSequence(string sequence)
    {
        Stack<AgentParameters> savePoints = new Stack<AgentParameters>();
        var currentPosition = Vector3.zero;

        Vector3 direction = Vector3.forward;
        Vector3 tempPosition = Vector3.zero;

        positions.Add(currentPosition);

        foreach (var letter in sequence)
        {
            SimpleVisualizer.EncodingLetters encoding = (SimpleVisualizer.EncodingLetters)letter;
            switch (encoding)
            {
                case SimpleVisualizer.EncodingLetters.save:
                    savePoints.Push(new AgentParameters
                    {
                        position = currentPosition,
                        direction = direction,
                        length = Length
                    });
                    break;
                case SimpleVisualizer.EncodingLetters.load:
                    if (savePoints.Count > 0)
                    {
                        var agentParameter = savePoints.Pop();
                        currentPosition = agentParameter.position;
                        direction = agentParameter.direction;
                        Length = agentParameter.length;
                    }
                    else
                    {
                        throw new System.Exception("Dont have saved point in our stack");
                    }
                    break;
                case SimpleVisualizer.EncodingLetters.draw:
                    tempPosition = currentPosition;
                    currentPosition += direction * length;
                    roadHelper.PlaceStreetPositions(tempPosition, Vector3Int.RoundToInt(direction), length);
                    Length -= 2;
                    positions.Add(currentPosition);
                    break;
                case SimpleVisualizer.EncodingLetters.turnRight:
                    direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;
                    break;
                case SimpleVisualizer.EncodingLetters.turnLeft:
                    direction = Quaternion.AngleAxis(-angle, Vector3.up) * direction;
                    break;
                default:
                    break;
            }
        }

        roadHelper.FixRoad();
        //var listv = new List<Vector3>() { new Vector3(0, 0, 21), new Vector3(1, 0, 24), new Vector3(2, 0, 24), new Vector3(3, 0, 18), new Vector3(4, 0, 16) };
        //var listv = ci
        // circuit = new Circuit(listv,Tree);
        //circuit.InstancierObjets();
        instancierEnvironnement.InstancierEnvironnement();
        structureHelper.PlaceStructureAroundRoad(roadHelper.GetRoadPositions());
        instancierMonnaies.InstatierMonnaies();


    }
}

