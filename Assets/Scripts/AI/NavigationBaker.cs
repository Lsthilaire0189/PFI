using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;


public class NavigationBaker : MonoBehaviour
{

    public NavMeshSurface[] surfaces;
    public Transform[] objectsToRotate;
    [SerializeField] GameObject RoadHelper;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(TrouverSurface());
    }
    IEnumerator TrouverSurface()
    {
        yield return new WaitForSeconds(1);
        int nbRues = RoadHelper.transform.childCount;
        surfaces = new NavMeshSurface[nbRues];
        for (int i = 0; i < nbRues; i++)
        {
            surfaces[i] = RoadHelper.transform.GetChild(i).GetComponent<NavMeshSurface>();
        }

    }
    void Update()
    {
        for (int j = 0; j < objectsToRotate.Length; j++)
        {
            objectsToRotate[j].localRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
        }

        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }


}