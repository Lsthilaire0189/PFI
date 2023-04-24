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
    
    public void CréerNpc(int NbAutos)
    {
        for (int i = 0; i < NbAutos; i++)
        {
            int NoVoiture = Random.Range(0, NPCVoitures.Count);
            GameObject voiture = Instantiate(NPCVoitures[NoVoiture], Vector3.zero, Quaternion.identity, gameObject.transform);
        }
    }
}
