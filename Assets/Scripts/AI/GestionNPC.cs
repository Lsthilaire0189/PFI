using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionNPC : MonoBehaviour
{
    [SerializeField] List<GameObject> NPCVoitures;

    SceneManagerScript sceneManagerScript;
    private void Awake()
    {
        sceneManagerScript = GetComponent<SceneManagerScript>();
    }
    
    public void Cr�erNpc(int NbAutos)
    {

    }
}
