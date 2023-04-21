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
    
    public int maxHP;

    public int maxEssence;

    public AudioSource sonEssence;
    public AudioSource sonGainHP;

    void Awake()
    {
        EssenceRestante = GetComponent<EssenceManager>();
        voitureHP = GetComponent<HealthManager>();
        maxHP = voitureHP.pointsVie;
        maxEssence = EssenceRestante.qtEssence;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == wrenchLayer)
        {
            Destroy(other.gameObject);
            if (voitureHP.pointsVie + maxVieRegen <= maxHP)
            {
                voitureHP.pointsVie += maxVieRegen;
            }
            else
            {
                voitureHP.pointsVie += maxHP - voitureHP.pointsVie;
            }
            sonGainHP.Play();

            
        }

        if (other.gameObject.layer == gasLayer)
        {
            Destroy(other.gameObject);
            if (EssenceRestante.qtEssence + essenceRegen <= maxEssence)
            {
                EssenceRestante.qtEssence += essenceRegen;
            }
            else
            {
                EssenceRestante.qtEssence += maxEssence - essenceRegen;
            }

            sonEssence.Play();
            
            
        }
    }
}
