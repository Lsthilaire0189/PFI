using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionPolice : MonoBehaviour
{
    public GameObject explosion;
    int BatimentLayer = 9;
    int JoueurLayer = 14;
    int NPCLayer = 16;
    Ray ray;
    float maxDistance = 2;
    public LayerMask layer = 14;
    Vector3 p;
    // Start is called before the first frame update
    void Start()
    {
    }
    void CheckForColliders()
    {
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            if (hit.collider.gameObject.layer == 14)
            {
                print(hit.collider.gameObject.name);
                p = hit.collider.transform.position;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (p == Vector3.zero)
        {
            ray = new Ray(transform.position, transform.forward);
            CheckForColliders();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, p, 0.005f);
        }

    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.layer == BatimentLayer)
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer == JoueurLayer)
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer == NPCLayer)
        {
            Destroy(gameObject);
        }

    }
}
