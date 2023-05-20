using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class GestionEssence: MonoBehaviour
{
    DéplacementScript voitureAvancer;
    GestionJoueur gestionJoueur;

    private float totalTime;

    private void Awake()
    {
        gestionJoueur = GetComponent<GestionJoueur>();  
        voitureAvancer = GetComponent<DéplacementScript>();
    }
    public void ModifierEssence(int EssenceAjoutee) //fonction qui permet d'ajouter points à la quantité d'essence totale au joueur 
    {
        gestionJoueur.JoueurEssence += EssenceAjoutee;
    }

    public bool VérifierEssence()
    {
        if (gestionJoueur.JoueurEssence > gestionJoueur.CapacitéEssenceMaximale)
        {
            gestionJoueur.JoueurEssence = gestionJoueur.CapacitéEssenceMaximale; //permet de s'assurer que la quantité d'essence du joueur ne dépasse pas sa quantité d'essence maximale
        }
        return gestionJoueur.JoueurEssence > 0;
    }
    private void Update()
    {
        totalTime += Time.deltaTime; //on diminue la quantité d'essence du joueur de 1 chaque seconde
        if (gestionJoueur.JoueurEssence > 0)
        {
            if (!(totalTime >= 1)) return;
            gestionJoueur.JoueurEssence -= 1;
            totalTime = 0.0f;
        }
    }
}
