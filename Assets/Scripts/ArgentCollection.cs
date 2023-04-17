using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArgentCollection : MonoBehaviour
{
    [System.NonSerialized] 
    public int qtArgent;

    private int argentLayer = 13;
    
    public AudioSource sonCollection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == argentLayer)
        {
            qtArgent += 1;
            Destroy(other.gameObject);
            sonCollection.Play();
        }
    }
}
