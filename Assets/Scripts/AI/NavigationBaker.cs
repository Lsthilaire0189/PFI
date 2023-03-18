using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;


public class NavigationBaker : MonoBehaviour
{

    public NavMeshSurface[] surfaces; 
    // Use this for initialization
    void Start()
    {
        StartCoroutine(TrouverSurface());
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
    IEnumerator TrouverSurface()
    {
        yield return new WaitForSeconds(1);
        

    }
    void Update()
    {
       
    }


}