using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FonctionCos : MonoBehaviour
{
    

    [SerializeField] 
    private float amplitude = 1;
    [SerializeField]
    private float stepPerSecond = 1;
    
    private static readonly Vector3 Up = Vector3.up;
    private float currentAngle = 0;
    private float previousUnitDelta = 0;

    private void Update()
    {
        currentAngle += stepPerSecond * Time.deltaTime;
        float currentUnitDelta = Mathf.Cos(currentAngle);
        transform.Translate(amplitude * (currentUnitDelta - previousUnitDelta) * Up);
        previousUnitDelta = currentUnitDelta;
    }
}
