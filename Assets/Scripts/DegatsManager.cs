using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DegatsManager : MonoBehaviour
{

    public AudioSource sonCollisionPolice;
    public AudioSource sonCollisionObjet;

    private int nombreDegatsReg = 5;
    private int nombreDegatsMax;

    private HealthManager voitureHP;

    private int BatimentLayer = 9;
    
    private DéplacementScript voitureAvancer;


    private void Awake()
    {
        voitureHP = GetComponent<HealthManager>();
        voitureAvancer = GetComponent<DéplacementScript>();
        nombreDegatsMax = nombreDegatsReg + 3;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint c = collision.GetContact(0);

        if (c.otherCollider.gameObject.layer == BatimentLayer && voitureHP.pointsVie > 0)
        {
            if (c.thisCollider.gameObject == voitureHP.PointFaible)
            {
                voitureHP.pointsVie -= nombreDegatsMax;
            }
            else
            {
                voitureHP.pointsVie -= nombreDegatsReg;
            }
            print(voitureHP.pointsVie);
            
            
        }
    }
    
}
