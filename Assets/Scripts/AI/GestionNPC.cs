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

    public Transform explosion;
    
    private void Awake()
    {
        sceneManagerScript = GetComponent<SceneManagerScript>();
    }
    
    public void CréerNpc(int NbAutos)
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == BatimentLayer)
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity, transform);
            Destroy(gameObject);
        }
    }
}
