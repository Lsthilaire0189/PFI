using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionOutilsManager : MonoBehaviour
{
    private int wrenchLayer = 7;
    private int gasLayer = 8;

    public int maxVieRegen = 2;
    public int essenceRegen = 10;
    
    private EssenceManager EssenceRestante;
    private HealthManager voitureHP;
    private static int maxHP;

    void Awake()
    {
        EssenceRestante = GetComponent<EssenceManager>();
        voitureHP = GetComponent<HealthManager>();
        maxHP = voitureHP.pointsVie;
    }
    //fix les colliders aussi

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == wrenchLayer)
        {
            if (voitureHP.pointsVie + maxVieRegen <= maxHP)
            {
                voitureHP.pointsVie += maxVieRegen;
            }
            else
            {
                voitureHP.pointsVie += maxHP - voitureHP.pointsVie;
            }

            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == gasLayer)
        {
            
            EssenceRestante.qtEssence += essenceRegen;
            
            
            
            Destroy(other.gameObject);
        }
    }
}
