using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeutAvancerManager : MonoBehaviour
{
    private EssenceManager EssenceRestante;
    private HealthManager voitureHP;
    private DéplacementScript VoitureAvancer;
    
    // Start is called before the first frame update
    void Awake()
    {
        EssenceRestante = GetComponent<EssenceManager>();
        voitureHP = GetComponent<HealthManager>();
        VoitureAvancer = GetComponent<DéplacementScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (EssenceRestante.qtEssence <= 0 || voitureHP.pointsVie <= 0)
        {
            VoitureAvancer.peutAvancer = false;
        }
        else
        {
            VoitureAvancer.peutAvancer = true;
        }
    }
}
