using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FonctionRotation : MonoBehaviour
{
    
    [SerializeField]
    private Vector3 rotationParSeconde;
    
    [SerializeField]
    private Space space;

    void Update()
    {
        transform.Rotate(rotationParSeconde*Time.deltaTime, space);
    }
}
