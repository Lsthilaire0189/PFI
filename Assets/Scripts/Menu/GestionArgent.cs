using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionArgent : MonoBehaviour
{
    GestionJoueur gestionJoueur;
    private void Awake()
    {
        gestionJoueur = GetComponent<GestionJoueur>();
    }

    public void ModifierArgent(int NbArgent)
    {
        gestionJoueur.JoueurArgent += NbArgent;
    }
}
