using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class EssenceManager : MonoBehaviour
{
    [SerializeField] public int qtEssence = 10;

    private DéplacementScript voitureAvancer;

    private float totalTime;

    private void Awake()
    {
        voitureAvancer = GetComponent<DéplacementScript>();
    }

    private void Update()
    {
        totalTime += Time.deltaTime;
        if (qtEssence > 0)
        {
            if (!(totalTime >= 1)) return;
            qtEssence -= 1;
            totalTime = 0.0f;
        }
    }
}
