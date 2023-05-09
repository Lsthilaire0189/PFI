using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionNPC : MonoBehaviour
{
    [SerializeField] List<GameObject> NPCVoitures;

    SceneManagerScript sceneManagerScript;
    
    int BatimentLayer = 9;
    int JoueurLayer = 14;
    int NPCLayer = 16;

    public GameObject explosion;
    
    private void Awake()
    {
        sceneManagerScript = GetComponentInParent<SceneManagerScript>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == BatimentLayer)
        {
            sceneManagerScript.NbAutosActuel--;
            Destroy(gameObject);
        }
        if (other.gameObject.layer == JoueurLayer)
        {
            sceneManagerScript.NbAutosActuel--;
            Destroy(gameObject);
        }
        if (other.gameObject.layer == NPCLayer)
        {
            sceneManagerScript.NbAutosActuel--;
            Destroy(gameObject);
        }
    }
}
