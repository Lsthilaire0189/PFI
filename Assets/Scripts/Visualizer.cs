
using System.Collections.Generic;
using UnityEngine;


public class Visualizer : MonoBehaviour
{
  
    public GameObject Arbre;
    public LSystemGenerator lsystem;
    List<Vector3> positions = new List<Vector3>();
    CircuitCarte circuit;

    public RoadHelper roadHelper;
        public StructureHelper structureHelper;

        public ArgentSpawner argentInitiateur;
        
        private int length = 6;
        private float angle = 90;

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
                            throw new System.Exception("Don't have saved point in our stack");
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
        }
            environnementScript.InstancierEnvironnement();
            argentInitiateur.InstatierMonnaies();
            
            var spline = new Circuit(); 

        roadHelper.FixRoad();
        circuit = new CircuitCarte(Arbre,roadHelper);

        structureHelper.PlaceStructureAroundRoad(roadHelper.GetRoadPositions());
        


    }
   
}

