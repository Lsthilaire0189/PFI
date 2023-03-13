using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDéplacementCamera : MonoBehaviour
{
    [SerializeField] GameObject Voiture;

    void Update()
    {
        transform.position = Voiture.transform.position;
        transform.rotation = Voiture.transform.rotation;
    }
}
