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
    public void ModifierVie(int pointsVie)
    {
        gestionJoueur.JoueurHP += pointsVie;
        print($"{gestionJoueur.JoueurHP} vie joueur");
    }
    public bool VérifierVieJoueur()
    {
        if (gestionJoueur.JoueurHP > gestionJoueur.VieMaximaleJoueur)
        {
            gestionJoueur.JoueurHP = gestionJoueur.VieMaximaleJoueur;
        }
        return gestionJoueur.JoueurHP > 0;
    }
}
