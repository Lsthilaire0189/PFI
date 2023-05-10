using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationJeu : MonoBehaviour
{
    public static InformationJeu inf;


    int argentDisponible;
    
    [HideInInspector] public int upgradeVitesseMaximale,
        upgradeAcceleration,
        upgradeForceFreinage,
        upgradeCapaciteEssence,
        upgradeVieMaximale,
        upgradeWrench,
        upgradePompe;

    public int ArgentDisponible
    {
        get { return argentDisponible; }
        set
        {
            if (value < 0)
            {
                argentDisponible = 0;
            }
            else
            {
                argentDisponible = value;
            }
        }
    }


    private void Awake()
    {
        if (inf != null && inf != this)
        {
            Destroy(this);
        }
        else
        {
            inf = this;
            inf.ArgentDisponible = 15;
        }
    }
}