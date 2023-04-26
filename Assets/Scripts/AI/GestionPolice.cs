using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPolice : MonoBehaviour
{
    Ray ray;
    float maxDistance = 2;
    public LayerMask layer = 14;
    Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
    }
    void CheckForColliders()
    {
        if (Physics.Raycast(ray, out RaycastHit hit,maxDistance))
        {
            if (hit.collider.gameObject.layer == 14)
            {
                print(hit.collider.gameObject.name);
                position = hit.collider.transform.position;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (position== null)
        {
            ray = new Ray(transform.position, transform.forward);
        CheckForColliders();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, position, 0.005f);
        }

    }
}
