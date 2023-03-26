using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointageScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int RetournerPointage(float tempsSurvie)
    {
        return (int)tempsSurvie * 5;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
