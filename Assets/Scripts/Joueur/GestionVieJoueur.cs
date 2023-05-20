using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionVieJoueur : MonoBehaviour
{
    GestionJoueur gestionJoueur;
    private void Awake()
    {
        gestionJoueur = GetComponent<GestionJoueur>();  
    }
    public void ModifierVie(int pointsVie) //fonction qui permet d'ajouter pointsVie à la quantité de vie totale au joueur 
    {
        gestionJoueur.JoueurHP += pointsVie;
    }
    public bool VérifierVieJoueur()
    {
        if (gestionJoueur.JoueurHP > gestionJoueur.VieMaximaleJoueur)
        {
            gestionJoueur.JoueurHP = gestionJoueur.VieMaximaleJoueur; //permet de s'assurer que la quantité de vie du joueur ne dépasse pas sa quantité de vie maximale
        }
        return gestionJoueur.JoueurHP > 0;
    }
}
