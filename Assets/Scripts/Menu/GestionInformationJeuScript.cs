using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionInformationJeuScript : MonoBehaviour
{
    public static GestionInformationJeuScript instance;
    [HideInInspector]
    public int NbPoints;
    
    
    int argentDisponible;
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
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            instance.ArgentDisponible = 0;
        }
    }



}
