using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class GestionEssence: MonoBehaviour
{
    [SerializeField] public int qtEssence = 10;

    DéplacementScript voitureAvancer;
    GestionJoueur gestionJoueur;

    private float totalTime;

    private void Awake()
    {
        gestionJoueur = GetComponent<GestionJoueur>();  
        voitureAvancer = GetComponent<DéplacementScript>();
    }
    public void ModifierEssence(int EssenceAjoutée)
    {
        gestionJoueur.JoueurEssence += EssenceAjoutée;
    }

    public bool VérifierEssence()
    {
        if (gestionJoueur.JoueurEssence > gestionJoueur.CapacitéEssenceMaximale)
        {
            gestionJoueur.JoueurEssence = gestionJoueur.CapacitéEssenceMaximale;
        }
        return gestionJoueur.JoueurEssence > 0;
    }
    private void Update()
    {
        totalTime += Time.deltaTime;
        if (gestionJoueur.JoueurEssence > 0)
        {
            if (!(totalTime >= 1)) return;
            gestionJoueur.JoueurEssence -= 1;
            totalTime = 0.0f;
        }
    }
}
