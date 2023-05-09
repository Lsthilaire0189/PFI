using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationJeu : MonoBehaviour
{
    public static InformationJeu inf;
    [HideInInspector]
    public int NbPoints;


    int argentDisponible;
    [HideInInspector]
    public float upgradeAccélération;
    [HideInInspector]
    public int upgradeVitesseMaximale,
     upgradeForceFreinage,
     upgradeCapacitéEssence,
     upgradeVieMaximale,
     upgradeWrench,
     upgradePompe;
    public int ArgentDisponible
    {
        get
        {
            return argentDisponible;
        }
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
