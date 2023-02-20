using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamera : MonoBehaviour
{
    [SerializeField] GameObject Auto;
    GameObject auto;
    private void Start()
    {
        auto = Auto;
    }

    void Update()
    {
        if (auto!= null)
        {
            transform.position = new Vector3(auto.transform.position.x, transform.position.y, auto.transform.position.z);
        }
    }

}
