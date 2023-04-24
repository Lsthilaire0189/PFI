using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPolice : MonoBehaviour
{
    Ray ray;
    float maxDistance = 100;
    public LayerMask layer;
    // Start is called before the first frame update
    void Start()
    {
        ray=new Ray(transform.position, transform.forward);

        
    }
    void RegarderColliders()
    {
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layer))
        {
            Vector3.MoveTowards(transform.position, hit.transform.position,20f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
