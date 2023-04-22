using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionOutilsManager : MonoBehaviour
{
    public int maxVieRegen = 2;
    public int essenceRegen = 10;
    
    GestionJoueur gestionJoueur;
    public int maxHP;
    public int maxEssence;
    
    public AudioSource sonEssence;
    public AudioSource sonGainHP;

    void Awake()
    {
        gestionJoueur = GetComponent<GestionJoueur>();
        maxEssence = gestionJoueur.CapacitéEssenceMaximale;
        maxHP = gestionJoueur.VieMaximaleJoueur;
    }


    public void AugmenterEssence()
    {
        if (gestionJoueur.JoueurEssence + essenceRegen <= maxEssence)
        {
            gestionJoueur.JoueurEssence += essenceRegen;
        }
        else
        {
            gestionJoueur.JoueurEssence += maxEssence - essenceRegen;
        }
        sonEssence.Play();
    }
}
