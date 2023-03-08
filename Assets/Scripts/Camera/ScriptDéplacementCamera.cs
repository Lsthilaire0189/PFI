using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDéplacementCamera : MonoBehaviour
{
    [SerializeField] GameObject Voiture;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Voiture.transform.position;
        transform.rotation = Voiture.transform.rotation;
    }
}
