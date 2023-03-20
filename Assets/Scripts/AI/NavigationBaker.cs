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
       
    }
    IEnumerator TrouverSurface()
    {
         yield return new WaitForSeconds(1);
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }

    }
    void Update()
    {
       
    }


}